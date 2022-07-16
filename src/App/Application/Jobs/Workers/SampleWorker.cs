using Core.Jobs.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Jobs.Workers
{
    public class SampleWorker : IJob
    {
        private readonly IRecurringJobManager _jobManager;

        public SampleWorker(IServiceProvider serviceProvider)
        {
            _jobManager = serviceProvider.GetService<IRecurringJobManager>();
        }

        public Task Todo()
        {
            _jobManager.AddOrUpdate("console date every 10 minute", () => Console.WriteLine(DateTime.UtcNow.ToShortTimeString()), "*/10 * * * *");

            return Task.CompletedTask;
        }
    }
}