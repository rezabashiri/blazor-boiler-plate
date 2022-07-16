using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using Service.Share.StartupSetup.Authentication;

namespace Identity.Api.StartupSetup;

public static class OpenIddictSetup
{
    public static IServiceCollection AddOpenIddict(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {


        // To not map claims name to default JWT ones
       JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default entities.
                options.UseEntityFrameworkCore()
                    .UseDbContext<AuthenticationDbContext>();


            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the token endpoint.
                options.SetTokenEndpointUris("/token")
                    .SetLogoutEndpointUris("/logout");
                // Enable the password flow.
                options.AllowPasswordFlow()
                    .AllowRefreshTokenFlow() ;


                //// Encryption and signing of tokens

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
                // Register the ASP.NET Core host and configure the ASP.NET Core options.
                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .DisableTransportSecurityRequirement();
                options.SetAuthorizationEndpointUris("/connect/authorize");

                //Disable encryption to Debug purposes 
                if (env.IsDevelopment())
                    options.DisableAccessTokenEncryption();

                options.SetRefreshTokenLifetime(
                    TimeSpan.FromDays(int.Parse(configuration.GetSection("OpenIDConnectSettings:RefreshTokenExpireFromDay").Value)));
                options.SetAccessTokenLifetime(
                    TimeSpan.FromHours(int.Parse(configuration.GetSection("OpenIDConnectSettings:AccessTokenExpireFromHour").Value)));

                options.DisableRollingRefreshTokens();
                options.UseReferenceRefreshTokens();

               

            })
            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();

            });
        
        // Just need for authorize users in [authorized] controller like Logout
      
        services
            .ConfigureIdentityOptions()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options=> JwtBearerSetup.DefaultOptions(options,env, (env.IsProduction() ? "https://" : "http://") + configuration.GetSection("OpenIDConnectSettings:Authority").Value));

        return services;
    }
}

