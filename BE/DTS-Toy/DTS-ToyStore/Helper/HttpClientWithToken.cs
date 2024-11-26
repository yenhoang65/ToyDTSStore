using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTS_ToyStore.Helper
{
    public class HttpClientWithToken
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientWithToken(System.Net.Http.HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;


            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }

    public class SomeService
    {
        private readonly HttpClientWithToken _httpClientWithToken;

        public SomeService(HttpClientWithToken httpClientWithToken)
        {
            _httpClientWithToken = httpClientWithToken;
        }

        public async Task<string> CheckToken()
        {
            var response = await _httpClientWithToken.GetAsync("http://localhost:5000.com/api/User/check-token");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
