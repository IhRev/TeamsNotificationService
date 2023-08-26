namespace TeamsNotificationService.System
{
	public interface IHttpWrapper
	{
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);
    }
}