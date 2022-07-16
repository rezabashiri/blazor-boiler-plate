using Identity.Managers;
using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.StartupSetup;

public static class IdentitySetup
{
    /// <summary>
    /// Add Identity required services and setup identity database
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <param name="postgreDBConnectionStringName">PostgreSql appsettings connection name</param>
    /// <returns></returns>
    public static IServiceCollection AddEfIdentity(this IServiceCollection services,
        IConfiguration config,
        string postgreDBConnectionStringName)
    {
        services.AddDbContext<AuthenticationDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString(postgreDBConnectionStringName));
            options.UseOpenIddict();
        });

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddSignInManager<CustomSignInManager>()
            .AddDefaultTokenProviders();

        return services;
    }
}

