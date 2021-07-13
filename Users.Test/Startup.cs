using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Repository;

namespace Users.Test
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {

        }

        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            services.AddSingleton<IUsersRepository, FakeUsersRepository>();
        }
    }

}
