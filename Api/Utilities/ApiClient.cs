namespace Api.Utilities
{
    public class ApiClient : IApiClient
    {
        private readonly ApiSettings apiSettings;
        private HttpClient httpClient;

        public ApiClient(ApiSettings apiSettings)
        {
            this.apiSettings = apiSettings;

            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(apiSettings.Url)
            };
            httpClient.DefaultRequestHeaders.Add("X-Functions-Key", this.apiSettings.Key);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            var response = await httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"GET request failed");
            }

            return response;
        }
    }
}

