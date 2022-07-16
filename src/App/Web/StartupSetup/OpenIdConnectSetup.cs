using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Service.Share.StartupSetup.Authentication;
using Web.Authentication.Helpers;

namespace Web.StartupSetup;

public static class OpenIdConnectSetup
{
    public static IServiceCollection AddOpenIdConnect(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {

        // To not map claims name to default JWT ones
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
 
        services
            .AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Identity/Account/Login");
                options.LogoutPath = new PathString("/Identity/Account/Logout");
            })
                // If any api added, we would have need jwt to authenticate apis based on their headers
            .AddJwtBearer(options => JwtBearerSetup.DefaultOptions(options, env, AuthencticationHelper.GetAuthority(env, config)));
        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                CookieAuthenticationDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder =
                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
             options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        });

        services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(sp => {
            // this is safe because 
            //     the `RevalidatingIdentityAuthenticationStateProvider` extends the `ServerAuthenticationStateProvider`
            var provider = (ServerAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>();
            return provider;
        });
        return services;
    }
}

