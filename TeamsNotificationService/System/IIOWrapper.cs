namespace TeamsNotificationService.System
{
	public interface IIOWrapper
	{
        string AppPath { get; }

        Task<string> ReadAllTextAsync(string path);
    }
}