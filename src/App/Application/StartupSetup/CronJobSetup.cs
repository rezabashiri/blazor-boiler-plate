using Application.Hangfire;
using Hangfire;
using Hangfire.MediatR;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.StartupSetup
{
    internal static class CronJobSetup
    {
        public static IServiceCollection AddCronJobProcessor(this IServiceCollection services, string connectionString)
        {

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                })
                .UseMediatR(new KnownTypesBinder()
                {
                    KnownTypes = Helpers.AssemblyHelper.GetApplicationLayerAssembly().GetTypes().ToList()
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            return services;
        }

        public static void UseHangfireDashboard(this IApplicationBuilder app)
        {

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {

                Authorization = new[] { new HangFireAuthorizationFilter() }
            });
        }
    }
}