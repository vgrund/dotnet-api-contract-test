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

namespace Users.Test
{
    public class ProviderApiTests : IDisposable
    {
        private string _providerUri { get; }
        private string _pactServiceUri { get; }
        private IWebHost _webHost { get; }
        private ITestOutputHelper _outputHelper { get; }
        private TestServer _server { get; set; }

        public ProviderApiTests(ITestOutputHelper output)
        {
            _outputHelper = output;
            _providerUri = "http://localhost:9000";
            _pactServiceUri = "http://localhost:9001";

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(_providerUri)
                .UseStartup<ApiStartup>()
                .Build();

            _webHost.Start();

            //_server = new TestServer(new WebHostBuilder()
            //    .UseUrls(_providerUri)
            //    .UseStartup<ApiStartup>());
        }

        [Fact]
        public void EnsureProviderApiHonoursPactWithConsumer()
        {
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
                    _webHost.StopAsync().GetAwaiter().GetResult();
                    _webHost.Dispose();
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
