using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nanny
{
    public class ServerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(List<Server>);

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var response = new List<Server>();
            JObject servers = JObject.Load(reader);
            foreach (var server in servers)
            {
                var s = JsonConvert.DeserializeObject<Server>(server.Value.ToString());
                response.Add(s);
            }
            return response;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (Server server in (List<Server>)value)
            {
                writer.WriteRawValue(JsonConvert.SerializeObject(server));
            }
            writer.WriteEndArray();
        }
    }
}
