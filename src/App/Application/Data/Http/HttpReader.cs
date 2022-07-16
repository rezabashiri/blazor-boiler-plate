using System.Net.Http.Json;
using Application.Interfaces;

namespace Application.Data.Http;
public class HttpReader<T> : IDataReader<T>
{
    private readonly HttpClient _httpClient;
    public HttpReader(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("");
    }
    public async Task<IDataReaderResponse<List<T>>> GetList(string path)
    {
        var eventListResponse = await _httpClient.GetAsync(path);
        if (!eventListResponse.IsSuccessStatusCode)
            new DataReaderResponse<List<T>>() { IsSuccess = false, Data = new List<T>() };

        var data = await eventListResponse.Content.ReadFromJsonAsync<List<T>>();

        return new DataReaderResponse<List<T>>() { IsSuccess = true, Data = data };
    }
}
