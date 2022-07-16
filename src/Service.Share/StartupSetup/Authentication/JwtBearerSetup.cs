using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;

namespace Service.Share.StartupSetup.Authentication
{
    public static class JwtBearerSetup
    {
        /// <summary>
        /// Default options to set jwt bearer token
        /// </summary>
        public static Action<JwtBearerOptions, IWebHostEnvironment, string> DefaultOptions 
        {
            get{
                return 
                    (options, env, authority) =>
                {

                    var isDevelopmentOrStaging = env.IsDevelopment() || env.IsStaging();

                    options.SaveToken = true;
                    options.RequireHttpsMetadata = !isDevelopmentOrStaging;

                    options.Authority = authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = !isDevelopmentOrStaging,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        RoleClaimType = OpenIddictConstants.Claims.Role,
                        NameClaimType = OpenIddictConstants.Claims.Name,
                        ValidateLifetime = true,
                        ClockSkew = TokenValidationParameters.DefaultClockSkew,
                    };
                };
            }
        }

        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Username;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
                options.ClaimsIdentity.EmailClaimType = OpenIddictConstants.Claims.Email;
                options.ClaimsIdentity.SecurityStampClaimType = "secret_value";
            });
            return services;
        }
    }
}