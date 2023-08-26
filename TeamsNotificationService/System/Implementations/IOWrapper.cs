using System.Text;

namespace TeamsNotificationService.System.Implementations
{
	public class IOWrapper : IIOWrapper
    {
        private readonly Encoding encoding = Encoding.UTF8;

        public async Task<string> ReadAllTextAsync(string path) => await File.ReadAllTextAsync(path, encoding);

        public string AppPath => AppDomain.CurrentDomain.BaseDirectory;
    }
}