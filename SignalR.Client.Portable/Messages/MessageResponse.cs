using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SignalR.Client.Portable
{
    public class MessageResponse
    {
        [JsonProperty("R")]
        public JToken Result { get; set; }
        [JsonProperty("I")]
        public string InvocationIdentifier { get; set; }
        [JsonProperty("C")]
        public string MessageId { get; set; }
        [JsonProperty("M")]
        public Message[] Messages { get; set; }
    }
}
