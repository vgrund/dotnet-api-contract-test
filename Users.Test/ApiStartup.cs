

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using Users.Repository;
using Users.Test.Middleware;

namespace Users.Test
{
    public class ApiStartup { 
    public ApiStartup(IConfiguration configuration)
    {
        Configuration = configuration;
        Startup = new Startup(Configuration);
    }

    public IConfiguration Configuration { get; }
    public Startup Startup { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IUsersRepository, UsersRepository>();
        Startup.ConfigureServices(services);
        //services.AddSingleton<IUsersRepository, UsersRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ProviderStateMiddleware>();

        Startup.Configure(app, env);
    }
}

        //public class ApiStartup
        //{
        //    public ApiStartup(IConfiguration configuration)
        //    {
        //        Configuration = configuration;
        //    }

        //    public IConfiguration Configuration { get; }

        //    public void ConfigureServices(IServiceCollection services)
        //    {
        //        services.AddTransient<IUsersRepository, UsersRepository>();
        //        services.AddControllers();
        //    }

        //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //    {
        //        app.UseMiddleware<ProviderStateMiddleware>();
        //        app.UseRouting();
        //        app.UseEndpoints(e => e.MapControllers());
        //    }
        //}

        //public class ApiStartup
        //{
        //    public ApiStartup(IConfiguration configuration)
        //    {
        //        Configuration2 = configuration;
        //        Startup = new Startup(configuration);
        //    }

        //    public IConfiguration Configuration2 { get; }
        //    //public Startup Startup { get; }

        //    //// This method gets called by the runtime. Use this method to add services to the container.
        //    //public void ConfigureServices(IServiceCollection services)
        //    //{
        //    //    Startup.ConfigureServices(services);
        //    //}

        //    //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //    //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //    //{
        //    //    app.UseMiddleware<ProviderStateMiddleware>();
        //    //    Startup.Configure(app, env);
        //    //}

        //    public void Configuration(IAppBuilder app)
        //    {
        //        var apiStartup = new Startup(Configuration2);
        //        app.Use<ProviderStateMiddleware>();

        //        apiStartup.Configuration(app);
        //    }
        //}

        //public class ApiStartup
        //{
        //    public ApiStartup(IConfiguration configuration)
        //    {
        //        Configuration = configuration;
        //    }

        //    public IConfiguration Configuration { get; }

        //    // This method gets called by the runtime. Use this method to add services to the container.
        //    public void ConfigureServices(IServiceCollection services)
        //    {
        //        services.AddControllers();
        //        services.AddSwaggerGen(c =>
        //        {
        //            c.SwaggerDoc("v1",
        //                new OpenApiInfo
        //                {
        //                    Title = "API Users v1",
        //                    Version = "v1.0",
        //                    Description = "API REST Users - Feita em ASP.NET Core 5 para gerenciamento de usuários",
        //                    Contact = new OpenApiContact
        //                    {
        //                        Name = "Squad X",
        //                        Url = new Uri("https://gitlab.luizalabs.com/luizalabs/")
        //                    }
        //                }
        //            );
        //            c.EnableAnnotations();
        //            //c.OperationFilter<ExamplesOperationFilter>();
        //            c.ExampleFilters();
        //            // c.OperationFilter<ExamplesOperationFilter>();
        //            // c.OperationFilter<DescriptionOperationFilter>();

        //            var filePath = Path.Combine(System.AppContext.BaseDirectory, "Users.xml");
        //            c.IncludeXmlComments(filePath);
        //        });
        //        // services.AddSwaggerExamples();
        //        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        //    }

        //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //    {
        //        //if (env.IsDevelopment())
        //        //{
        //        //    app.UseDeveloperExceptionPage();
        //        //}

        //        app.UseMiddleware<ProviderStateMiddleware>();
        //        app.UseStaticFiles();

        //        app.UseSwagger();
        //        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users v1"));

        //        app.UseRouting();

        //        app.UseEndpoints(endpoints =>
        //        {
        //            endpoints.MapControllers();
        //        });
        //    }
        //}
    }
