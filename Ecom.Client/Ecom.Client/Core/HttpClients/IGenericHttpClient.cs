namespace Ecom.Client.Core.HttpClients
{
    public interface IGenericHttpClient
    {
        Task<List<TResponse>> GetAsync<TResponse>(string UrlAddress);
        Task<List<TResponse>> GetAsync<TResponse>(string UrlAddress, dynamic dynamicRequest);
        Task<TResponse> GetIdAsync<TResponse>(string UrlAddress, int id);
        Task<TResponse> PostByAsync<TResponse>(string UrlAddress, dynamic dynamicRequest);
        Task<TResponse> PutByAsync<TResponse>(string UrlAddress, dynamic dynamicRequest);
        Task DeleteByAsync(string UrlAddress, int dynamicRequest);

        Task<TResponse> PostWithFileAsync<TResponse>(string UrlAddress, MultipartFormDataContent formData);
    }
}