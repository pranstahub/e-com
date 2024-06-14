using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Ecom.Client.Core.HttpClients
{
    public class GenericHttpClient : IGenericHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public GenericHttpClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration["ApiClient:ClientUrl"]);

        }
        public async Task DeleteByAsync(string UrlAddress, int dynamicRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("deleted!");
                }
            }
            throw new NotImplementedException();
        }

        public async Task<List<TResponse>> GetAsync<TResponse>(string UrlAddress)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TResponse>>(content);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<List<TResponse>> GetAsync<TResponse>(string UrlAddress, dynamic dynamicRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TResponse>>(content);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<TResponse> GetIdAsync<TResponse>(string UrlAddress, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{UrlAddress}?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<TResponse> PostByAsync<TResponse>(string UrlAddress, dynamic dynamicRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<TResponse> PutByAsync<TResponse>(string UrlAddress, dynamic dynamicRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicRequest));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
            }
            throw new NotImplementedException();
        }

        public async Task<TResponse> PostWithFileAsync<TResponse>(string UrlAddress, MultipartFormDataContent formData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, UrlAddress);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Content = formData;



            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
                // Handle errors here as needed
                // You may want to throw a custom exception or return an error response
            }
            throw new NotImplementedException();
        }
    }
}