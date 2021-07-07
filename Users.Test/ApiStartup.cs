using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Users.Test.Middleware;

namespace Users.Test
{
    public class ApiStartup
    {
        public ApiStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "API Users v1",
                        Version = "v1.0",
                        Description = "API REST Users - Feita em ASP.NET Core 5 para gerenciamento de usuários",
                        Contact = new OpenApiContact
                        {
                            Name = "Squad X",
                            Url = new Uri("https://gitlab.luizalabs.com/luizalabs/")
                        }
                    }
                );
                //c.OperationFilter<ExamplesOperationFilter>();
                c.ExampleFilters();
                // c.OperationFilter<ExamplesOperationFilter>();
                // c.OperationFilter<DescriptionOperationFilter>();

                // var filePath = Path.Combine(System.AppContext.BaseDirectory, "Users.xml");
                // c.IncludeXmlComments(filePath);
            });
            // services.AddSwaggerExamples();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ProviderStateMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
