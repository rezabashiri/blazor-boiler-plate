namespace Application.Interfaces
{
    public interface IDataReaderResponse<T>
    {
        public T Data { get; init; }
        public bool IsSuccess { get; init; }

    }
}