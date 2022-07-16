 
using Identity.Seeders;

namespace Identity.Api.Backgrounds
{
    public class SeedDatabase : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDatabase(IServiceProvider serviceProvider )
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scope =   _serviceProvider.CreateAsyncScope();
            var seeder =  scope.ServiceProvider.GetService<IDatabaseSeeder>();

            await seeder.Seed(1);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        =>Task.CompletedTask;
    }
}
