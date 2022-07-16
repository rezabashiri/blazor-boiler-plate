using System.Reflection;
using Application.Data.Http;
using Application.Helpers;
using Application.Interfaces;
using Application.Jobs;
using Core.Jobs;
using Core.Jobs.Interfaces;
using Infrastructure.StartupSetup;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.StartupSetup
{
    public static class ApplicationSetup
    {
        /// <summary>
        /// Add application tier services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="dataBaseConnectionStringName">To add database tier, connection string name is mandatory</param>
        /// <param name="cronJobSeviceConnectionString">If cronjob is needed, fill connection string of hangfire database</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration,
            string dataBaseConnectionStringName,
            string cronJobSeviceConnectionString = "")
        {

            services.AddMediatR(new List<Assembly>() { AssemblyHelper.GetApplicationLayerAssembly() },
                configuration => { });

            services.AddSingleton(typeof(IDataReader<>), typeof(HttpReader<>));

            if (!string.IsNullOrEmpty(dataBaseConnectionStringName)) {
                services.AddPostgreSqlInfrastructure(configuration, dataBaseConnectionStringName);
            }

            if (!string.IsNullOrEmpty(cronJobSeviceConnectionString)) {
                services.AddCronJobProcessor(cronJobSeviceConnectionString);
            }

            return services;
        }

        public static void UseApplication(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseHangfireDashboard();

            DoJobs.DoAll(serviceProvider);
        }

    }
}
