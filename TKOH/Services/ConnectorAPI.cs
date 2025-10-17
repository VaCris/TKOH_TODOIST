using Microsoft.Extensions.Options;
using TKOH.Config;
using System.Net.Http.Headers;
using System.Text.Json;

namespace TKOH.Services
{
    public class ConnectorAPI
    {
        private readonly HttpClient _http;
        private const string JwtCookieName = "JWT_TOKEN";

        public ConnectorAPI(HttpClient http, IOptions<ApiSettings> apiSettings)
        {
            _http = http;

            var baseUrl = apiSettings.Value.BaseUrl;
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "BaseUrl cannot be null or empty");

            if (!baseUrl.StartsWith("http"))
                baseUrl = $"https://{baseUrl}";

            _http.BaseAddress = new Uri(baseUrl.TrimEnd('/'));
        }

        private void AddJwtHeader(HttpRequestMessage request, string? token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("Advertencia");
            }
        }

        private async Task<T?> SendAsync<T>(HttpRequestMessage request, string? token)
        {
            try
            {
                AddJwtHeader(request, token);
                var response = await _http.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }

                if (string.IsNullOrWhiteSpace(content))
                    return default;

                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public Task<T?> GetAsync<T>(string endpoint, string? token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint.TrimStart('/'));
            return SendAsync<T>(request, token);
        }

        public Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, string? token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint.TrimStart('/'))
            {
                Content = JsonContent.Create(data)
            };
            return SendAsync<TResponse>(request, token);
        }

        public Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data, string? token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, endpoint.TrimStart('/'))
            {
                Content = JsonContent.Create(data)
            };

            return SendAsync<TResponse>(request, token);
        }

        public Task<TResponse?> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data, string? token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, endpoint.TrimStart('/'))
            {
                Content = JsonContent.Create(data)
            };
            return SendAsync<TResponse>(request, token);
        }

        public async Task<bool> DeleteAsync(string endpoint, string? token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint.TrimStart('/'));
            AddJwtHeader(request, token);
            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}