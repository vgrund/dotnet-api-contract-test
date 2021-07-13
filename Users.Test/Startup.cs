using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Users.Repository;
using Users.Test.Middleware;
using Xunit.DependencyInjection;
using Api = Users;

namespace Users.Test
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            //var startup = new Api.Startup(configuration);
            //hostBuilder
            //    .ConfigureAppConfiguration(builder =>
            //    {
            //        builder
            //            .AddInMemoryCollection(new Dictionary<string, string>()
            //            {
            //                {"UserName", "Alice"}
            //            })
            //            .AddJsonFile("appsettings.json");
            //    })
            //    .ConfigureServices((context, services) =>
            //    {
            //        services.AddSingleton<IUsersRepository, UsersRepository>();
            //    })
            //.ConfigureWebHost(webHostBuilder => webHostBuilder
            //.UseUrls("http://*:9000;http://*:9001")
            //.UseStartup<Api.Startup>()
            //.UseTestServer()
            //.Configure(configure =>
            //{
            //    configure.UseMiddleware<ProviderStateMiddleware>();
            //})
            //.ConfigureServices(services =>
            //{
            //    services.AddRouting();
            //    services.AddSingleton<IUsersRepository, UsersRepository>();
            //}));
        }

        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {            
            var repo = new FakeUsersRepository();
            repo.Users = new List<User>(2){
                new User(){
                    Id = new Guid("ba8e6bc0-f02d-4f71-98cf-6f63b52434e0"),
                    FirstName = "Vinicius",
                    LastName = "Grund",
                    Email = "vsg@email.com",
                    Phone = "9999999999"
                },
                new User(){
                    Id = new Guid("ee4a8bd0-4792-472b-87a4-228ec2db84e0"),
                    FirstName = "Paul",
                    LastName = "McCartney",
                    Email = "pm@email.com",
                    Phone = "9999999999"
                }
            };

            //services.TryAddSingleton<UsersRepository>();
            services.AddSingleton<IUsersRepository>(s => repo);

        }

        //public void Configure(IApplicationBuilder app)
        //{
        //    app.UseMiddleware<ProviderStateMiddleware>();
        //}
    }

}
