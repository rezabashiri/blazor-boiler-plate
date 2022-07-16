using Application.Interfaces;

namespace Application.Data
{
    public class DataReaderResponse<T>: IDataReaderResponse<T>
    {
        public T Data { get; init; }
        public bool IsSuccess { get; init; }

        
    }
}