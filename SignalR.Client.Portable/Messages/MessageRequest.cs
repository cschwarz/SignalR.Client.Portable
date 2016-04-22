using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace SignalR.Client.Portable
{
    public class MessageRequest : Message
    {
        [JsonProperty("I")]
        public string InvocationIdentifier { get; set; }

        public MessageRequest(string hubName, string methodName, params string[] arguments)
        {
            HubName = hubName;
            MethodName = methodName;
            Arguments = arguments.Select(a => JToken.FromObject(a)).ToList();
            InvocationIdentifier = Guid.NewGuid().ToString();
        }
    }
}
