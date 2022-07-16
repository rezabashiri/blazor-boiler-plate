using App.Share.Providers;
using Infrastructure;
using Mapping.StartupSetup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTests
{
    public class BaseTest
    {
        protected ServiceProvider ServiceProvider { get; private set; }

        public BaseTest()
        {
            BuildServices(new ServiceCollection());
        }

        protected virtual IServiceCollection BuildServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath("../../../../../src/App/Web/appsettings.json"));

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddMappers()
                .AddSingleton<IConfiguration>(provider => configuration.Build());

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });
            ServiceProvider = services.BuildServiceProvider();

            return services;
        }
    }
}