using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SignalR.Client.Portable
{
    public class HubConnection : Connection
    {
        private Dictionary<string, HubProxy> hubs;

        public HubConnection(string url)
            : this(url, (string)null)
        {
        }

        public HubConnection(string url, IDictionary<string, string> queryString)
            : this(url, new FormUrlEncodedContent(queryString).ReadAsStringAsync().Result)
        {
        }

        public HubConnection(string url, string queryString)
            : base(url, queryString)
        {
            hubs = new Dictionary<string, HubProxy>();
        }

        public IHubProxy CreateHubProxy(string hubName)
        {
            HubProxy hubProxy = new HubProxy(this, hubName);

            hubs.Add(hubName, hubProxy);

            return hubProxy;
        }

        internal override void MessageReceived(MessageResponse response)
        {
            foreach (var hubProxy in hubs)
                hubProxy.Value.MessageReceived(response);
        }

        protected override string GetConnectionData()
        {
            return JsonConvert.SerializeObject(hubs.Select(h => new ConnectionData() { Name = h.Key }));
        }
    }
}