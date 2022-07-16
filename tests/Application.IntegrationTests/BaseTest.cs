using App.Share.Providers;
using Application.StartupSetup;
using Infrastructure;
using Mapping.StartupSetup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests;

public class BaseTest
{

    protected ServiceProvider ServiceProvider { get; private set; }

    public BaseTest()
    {
        BuildServices(new ServiceCollection());
    }

    protected virtual IServiceCollection BuildServices(IServiceCollection services)
    {

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {


            }).Build();

        services.AddSingleton<IConfiguration>(provider => configuration)
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddApplicationServices(configuration, "", "")
            .AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        })
            .AddScoped<IAccentDbContext, AppDbContext>()
            .AddMappers();

        ServiceProvider = services.BuildServiceProvider();
        return services;
    }


}

