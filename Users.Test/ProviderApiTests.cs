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
        private ITestOutputHelper _outputHelper { get; }
        private IConfiguration Configuration { get; }
        private readonly IUsersRepository _usersRepository;

        public ProviderApiTests(ITestOutputHelper output, IUsersRepository usersRepository, IConfiguration configuration)
        {
            Configuration = configuration;
            _usersRepository = usersRepository;
            _outputHelper = output;
            _providerUri = "http://localhost:9000";

            StartAsync();
        }

        private async void StartAsync(CancellationToken cancellationToken = default)
        {
            await CreateHostBuilder().Build().StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:9000");
                    webBuilder.UseStartup<ApiStartup>(s => new ApiStartup(Configuration, _usersRepository));
                });

        [Fact]
        public async void EnsureProviderApiHonoursPactWithConsumer()
        {
            var config = new PactVerifierConfig
            {

                // NOTE: We default to using a ConsoleOutput,
                // however xUnit 2 does not capture the console output,
                // so a custom outputter is required.
                Outputters = new List<IOutput>
                                {
                                   new XUnitOutput(_outputHelper)
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
