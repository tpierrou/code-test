namespace Api.Utilities
{
    public class ApiSettings
    {
        public string Url { get; init; }
        public string Key { get; init; }

        public ApiSettings(string url, string key)
        {
            Url = url;
            Key = key;
        }
    }
}

