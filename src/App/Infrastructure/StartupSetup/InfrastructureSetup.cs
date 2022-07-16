using App.Share.Startup;
using Infrastructure.Backgrounds;
using Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.StartupSetup
{
    public static class InfrastructureSetup
    {
        /// <summary>
        /// Add all required services to setting up infrastructure tier
        /// </summary>
        /// <param name="servicies"></param>
        /// <param name="configuration">Microsoft configuration instance</param>
        /// <param name="dataBaseConnectionName">Connection string name in Appsettings</param>
        /// <returns></returns>
        public static IServiceCollection AddPostgreSqlInfrastructure(
            this IServiceCollection servicies,
            IConfiguration configuration,
            string dataBaseConnectionName)
        {

            servicies.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(dataBaseConnectionName));
            });
            servicies.AddScoped<IDatabaseSeeder, AppSeeder>()
                .AddScoped<IAccentDbContext,AppDbContext>()
                .AddHostedService<SeedDatabase>();
            return servicies;
        }
    }
}
