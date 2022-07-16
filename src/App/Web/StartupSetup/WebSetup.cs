using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Web.StartupSetup
{
    public static class WebSetup
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()

                .AddScoped(x =>
                {
                    ActionContext actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                    IUrlHelperFactory factory = x.GetRequiredService<IUrlHelperFactory>();

                    return factory.GetUrlHelper(actionContext);
                });

            return services;
        }
    }
}