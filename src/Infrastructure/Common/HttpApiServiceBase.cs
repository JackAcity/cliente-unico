using System.Net.Http.Json;

namespace PlayerRegistration.Infraestructure.Common
{
    public abstract class HttpApiServiceBase
    {
        protected readonly HttpClient _httpClient;

        protected HttpApiServiceBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        protected async Task<TResponse> GetAsync<TResponse>(string url, CancellationToken token)
        {
            var response = await _httpClient.GetAsync(url, token);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"GET {url} failed with status code {response.StatusCode}");

            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);

            if (result == null)
                throw new InvalidOperationException($"Empty or invalid response from {url}");

            return result;
        }
        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken token)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data, token);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"POST {url} failed with status code {response.StatusCode}");

            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);

            if (result == null)
                throw new InvalidOperationException($"Empty or invalid response from {url}");

            return result;
        }

        protected async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken token)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data, token);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"PUT {url} failed with status code {response.StatusCode}");
            var result = await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: token);

            if (result == null)
                throw new InvalidOperationException($"Empty or invalid response from {url}");
            return result;

        }
    }
}
