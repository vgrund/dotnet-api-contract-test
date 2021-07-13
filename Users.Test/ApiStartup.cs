

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Users.Repository;
using Users.Test.Middleware;
using Api = Users;

namespace Users.Test
{
    public class ApiStartup { 
        public ApiStartup(IConfiguration configuration, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            Configuration = configuration;
            Startup = new Api.Startup(Configuration);
        }

        public IConfiguration Configuration { get; }
        public Api.Startup Startup { get; }
        private readonly IUsersRepository _usersRepository;

        public void ConfigureServices(IServiceCollection services)
        {
            Startup.ConfigureServices(services);
            services.AddSingleton<IUsersRepository>(s => _usersRepository);

            var assembly = Assembly.Load("Users");

            services.AddMvc().AddApplicationPart(assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Startup.Configure(app, env);
            app.UseMiddleware<ProviderStateMiddleware>();
        }
    }
}
