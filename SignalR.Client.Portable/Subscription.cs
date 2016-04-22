using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SignalR.Client.Portable
{
    public class Subscription
    {
        public event Action<IList<JToken>> Received;

        internal void OnReceived(IList<JToken> data)
        {
            Received?.Invoke(data);
        }
    }
}
