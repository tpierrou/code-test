namespace Api.Utilities
{
	public interface IApiClient
	{
        public Task<HttpResponseMessage> GetAsync(string endpoint);
    }
}

