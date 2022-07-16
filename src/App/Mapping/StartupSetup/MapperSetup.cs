using Mapping.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Mapping.StartupSetup
{
    public static class MapperSetup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {

            services.AddAutoMapper(cofig => { }, AssemblyHelper.GetApplicationLayerAssembly());
            return services;
        }
    }
}
