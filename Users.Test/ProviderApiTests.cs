using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;
using Users.Test.XUnitHelpers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Users.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Api = Users;
using System.Threading.Tasks;
using System.Threading;

namespace Users.Test
{
    public class ProviderApiTests : IDisposable
    {
        private string _providerUri { get; }
        //private string _pactServiceUri { get; }
        private IWebHost _webHost { get; }
        private ITestOutputHelper _outputHelper { get; }
        private TestServer _server { get; set; }
        private IConfiguration Configuration { get; }
        //private readonly WebApplicationFactory<Startup> _factory;
        private readonly IUsersRepository _usersRepository;

        public ProviderApiTests(ITestOutputHelper output, IUsersRepository usersRepository, IConfiguration configuration)
        {
            Configuration = configuration;
            _usersRepository = usersRepository;
            _outputHelper = output;
            _providerUri = "http://localhost:9000";
            //_pactServiceUri = "http://localhost:9001";



            //var webHost = WebHost.CreateDefaultBuilder()
            //    .UseUrls("http://*:9000;http://*:9001")
            //    .UseStartup<ApiStartup>(s => new ApiStartup(configuration, usersRepository));
            //    .Build();

            //_webHost.Start();

            //_server = new TestServer(webHost);

            //_factory = factory;

            StartAsync();
        }

        private async void StartAsync(CancellationToken cancellationToken = default)
        {
            await CreateHostBuilder().Build().StartAsync(cancellationToken).ConfigureAwait(false);
        }

        //public static void Run()
        //{
        //    string[] args = { "" };
        //    CreateHostBuilder(args).Build().RunAsync();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseUrls("http://localhost:9000");
        //            webBuilder.UseStartup<Startup>();
        //        });

        public IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:9000");
                    //webBuilder.UseStartup<Api.Startup>();
                    //webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    //{
                    //    config.AddJsonFile
                    //});
                    //webBuilder.UseStartup<ApiStartup>();
                    webBuilder.UseStartup<ApiStartup>(s => new ApiStartup(Configuration, _usersRepository));
                });

        [Fact]
        public async void EnsureProviderApiHonoursPactWithConsumer()
        {
           // var hostBuilder = new HostBuilder()
           //.ConfigureWebHost(webHost =>
           //{
           //     // Add TestServer
           //     webHost
           //         .UseUrls(_providerUri)
           //         .UseTestServer()
           //         .UseStartup<ApiStartup>(s => new ApiStartup(Configuration, _usersRepository));
                    
           //     //webHost.Configure(app => app.Run(async ctx =>
           //     //    await ctx.Response.WriteAsync("Hello World!")));
           // });

           // var host = hostBuilder.Build();
           // await host.StartAsync();

            //Host.CreateDefaultBuilder()
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseUrls(_providerUri);
            //        webBuilder.UseStartup<Startup>();
            //    });

            

            // Build and start the IHost
            //var host = await hostBuilder.StartAsync();

            // Arrange
            var config = new PactVerifierConfig
            {

                // NOTE: We default to using a ConsoleOutput,
                // however xUnit 2 does not capture the console output,
                // so a custom outputter is required.
                Outputters = new List<IOutput>
                                {
                                   new XUnitOutput(_outputHelper)
                                    //new ConsoleOutput()
                                },

                // Output verbose verification logs to the test output
                Verbose = true,
                ProviderVersion = "1.0.0", //NOTE: Setting a provider version is required for publishing verification results
                PublishVerificationResults = true
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState($"{_providerUri}/provider-states")
                .ServiceProvider("API Users v1", _providerUri)
                //.PactUri("http://pact-maestro.ipet.sh/pacts/provider/API%20Users%20v1/consumer/API%20Users%20v1%20-%20Release%20v1.0/latest");
                .PactBroker("http://pact-maestro.ipet.sh");

            pactVerifier.Verify();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //_webHost.StopAsync().GetAwaiter().GetResult();
                    //_webHost.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
