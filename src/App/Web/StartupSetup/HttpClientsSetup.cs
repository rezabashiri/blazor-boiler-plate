using IdentityModel.Client;
using Web.Authentication.Helpers;
using Web.Authentication.Interfaces;

namespace Web.StartupSetup
{
    public static class HttpClientsSetup
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            var openIdSettings = configuration.GetSection("OpenIDConnectSettings");

            services.AddHttpClient("authenticationServer", client =>
             {
                 client.BaseAddress = new Uri(AuthencticationHelper.GetAuthority(environment, configuration));
                 client.DefaultRequestHeaders.Add("Accept", "application/json");

                 client.DefaultRequestHeaders.Add("client_id", openIdSettings.GetSection("ClientId").Value);
                 client.DefaultRequestHeaders.Add("client_secret", openIdSettings.GetSection("ClientSecret").Value);

             });
            services.AddSingleton(new PasswordTokenRequest
            {
                ClientId = openIdSettings.GetSection("ClientId").Value,
                ClientSecret = openIdSettings.GetSection("ClientSecret").Value,

            });
            services.AddSingleton<IAuthencticationHelper, AuthencticationHelper>();

            return services;
        }
    }
}
