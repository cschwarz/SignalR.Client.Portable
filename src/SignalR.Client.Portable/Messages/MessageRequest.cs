using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SignalR.Client.Portable
{
    internal class MessageRequest : Message
    {
        [JsonProperty("I")]
        public string InvocationIdentifier { get; set; }

        public MessageRequest(string hubName, string methodName, params JToken[] arguments)
        {
            HubName = hubName;
            MethodName = methodName;
            Arguments = arguments;
            InvocationIdentifier = Guid.NewGuid().ToString();
        }
    }
}
