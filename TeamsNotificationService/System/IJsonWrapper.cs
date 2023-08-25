namespace TeamsNotificationService.System
{
	public interface IJsonWrapper
	{
		string Serialize(object obj);

		T Deserialize<T>(string value);
    }
}