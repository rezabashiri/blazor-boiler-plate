namespace Core.Jobs.Interfaces
{
    /// <summary>
    /// To run any type of jobs, just implement IJob interface
    /// </summary>
    public interface IJob
    {
        public Task Todo();

    }
}
