using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Core.Jobs.Interfaces;
using Core.Jobs;

namespace Message.Dispatcher.Core.UnitTests.Jobs
{
    public class DoJobsTest
    {
        private static int counter = 0;
        internal class ItestJob : IJob
        {
            public Task Todo()
            {
                counter++;
                return Task.CompletedTask;
            }
        }
        internal class ParameteredClass : IJob
        {
            public ParameteredClass(int id)
            {
                counter = +id;
            }
            public Task Todo()
            {
                counter++;
                return Task.CompletedTask;
            }
        }

        [Fact]
        public void DoAll_ShouldCallMethodOfAllImplementedIJobInterface()
        {
            var toAdd = 2;
            DoJobs.DoAll(toAdd);

            counter.Should().Be(3);
        }

    }



}