using App.Share.Startup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Backgrounds
{
    public class SeedDatabase : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDatabase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = _serviceProvider.CreateAsyncScope();
            var seeder = scope.ServiceProvider.GetService<IDatabaseSeeder>();

            await seeder.Seed(5);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
    }
}
