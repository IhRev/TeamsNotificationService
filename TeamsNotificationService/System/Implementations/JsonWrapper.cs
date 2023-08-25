using Newtonsoft.Json;

namespace TeamsNotificationService.System.Implementations
{
	public class JsonWrapper : IJsonWrapper
	{
		public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

		public T Deserialize<T>(string value) => JsonConvert.DeserializeObject<T>(value); 
	}
}