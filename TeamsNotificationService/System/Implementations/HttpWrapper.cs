namespace TeamsNotificationService.System.Implementations
{
	public class HttpWrapper : IHttpWrapper
    {
        private readonly HttpClient httpClient;

        public HttpWrapper(HttpClient httpClient)
		{
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content) =>
            await httpClient.PostAsync(url, content).ConfigureAwait(false); 
	}
}