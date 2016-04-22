using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SignalR.Client.Portable
{
    public class Message
    {
        [JsonProperty("H")]
        public string HubName { get; set; }
        [JsonProperty("M")]
        public string MethodName { get; set; }
        [JsonProperty("A")]
        public JToken[] Arguments { get; set; }
    }
}
