using System.Net.Http.Headers;

namespace TeamsNotificationService.System.Implementations
{
	public class HttpWrapper : IHttpWrapper
    {
        private readonly HttpClient httpClient;

        public HttpWrapper(HttpClient httpClient)
		{
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content) =>
            await httpClient.PostAsync(url, content).ConfigureAwait(false); 
	}
}