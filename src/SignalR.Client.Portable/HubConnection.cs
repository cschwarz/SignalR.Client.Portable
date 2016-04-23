using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SignalR.Client.Portable
{
    public class HubConnection : IDisposable
    {
        public string ConnectionToken { get; private set; }
        public string ConnectionId { get; private set; }
        public ConnectionState State { get; private set; }
        public string QueryString { get; private set; }
        public string Url { get; private set; }

        private Dictionary<string, HubProxy> hubs;

        private WebSocket webSocket;

        public HubConnection(string url)
            : this(url, (string)null)
        {
        }

        public HubConnection(string url, IDictionary<string, string> queryString)
            : this(url, new FormUrlEncodedContent(queryString).ReadAsStringAsync().Result)
        {
        }

        public HubConnection(string url, string queryString)
        {
            Url = url;

            if (!Url.EndsWith("/"))
                Url += "/";

            Url += "signalr/";

            webSocket = new WebSocket();
            webSocket.MessageReceived += MessageReceived;

            State = ConnectionState.Disconnected;
            QueryString = queryString;

            hubs = new Dictionary<string, HubProxy>();
        }

        public IHubProxy CreateHubProxy(string hubName)
        {
            HubProxy hubProxy = new HubProxy(this, hubName);

            hubs.Add(hubName, hubProxy);

            return hubProxy;
        }

        public async Task Start()
        {
            State = ConnectionState.Connecting;

            await Negotiate();

            await Connect();

            await StartConnection();

            State = ConnectionState.Connected;
        }

        public async void Stop()
        {
            await webSocket.Close();

            await Abort();

            State = ConnectionState.Disconnected;
        }

        public Task Send(string message)
        {
            webSocket.Send(message);

            return Task.FromResult<object>(null);
        }

        public void Dispose()
        {
            if (State != ConnectionState.Disconnected)
                Stop();
        }

        private void MessageReceived(string message)
        {
            MessageResponse response = JsonConvert.DeserializeObject<MessageResponse>(message);

            foreach (var hubProxy in hubs)
                hubProxy.Value.MessageReceived(response);
        }

        private async Task Negotiate()
        {
            using (HttpClient client = new HttpClient())
            {
                string negotiateUrl = string.Format("{0}negotiate?{1}", Url, await NegotiateQueryString("1.4", GetConnectionData()));

                NegotiateResponse response = JsonConvert.DeserializeObject<NegotiateResponse>(await client.GetStringAsync(negotiateUrl));

                ConnectionToken = response.ConnectionToken;
                ConnectionId = response.ConnectionId;
            }
        }

        private async Task Connect()
        {
            string connectQueryString = await ConnectQueryString("1.4", GetConnectionData(), ConnectionToken);

            if (!string.IsNullOrEmpty(QueryString))
                connectQueryString += "&" + QueryString;

            await webSocket.Open(string.Format("{0}connect?{1}", Url, connectQueryString));
        }

        private async Task StartConnection()
        {
            using (HttpClient client = new HttpClient())
            {
                string startQueryString = await ConnectQueryString("1.4", GetConnectionData(), ConnectionToken);
                string startUrl = string.Format("{0}start?{1}", Url, startQueryString);

                await client.GetStringAsync(startUrl);
            }
        }

        private async Task Abort()
        {
            using (HttpClient client = new HttpClient())
            {
                string abortQueryString = await ConnectQueryString("1.4", GetConnectionData(), ConnectionToken);
                string abortUrl = string.Format("{0}abort?{1}", Url, abortQueryString);

                await client.GetStringAsync(abortUrl);
            }
        }

        private async Task<string> NegotiateQueryString(string clientProtocol, string connectionData)
        {
            return await new FormUrlEncodedContent(new Dictionary<string, string> {
                { "clientProtocol", clientProtocol },
                { "connectionData", connectionData }
            }).ReadAsStringAsync();
        }

        private async Task<string> ConnectQueryString(string clientProtocol, string connectionData, string connectionToken)
        {
            return await new FormUrlEncodedContent(new Dictionary<string, string> {
                { "clientProtocol", clientProtocol },
                { "transport", "webSockets" },
                { "connectionData", connectionData },
                { "connectionToken", connectionToken }
            }).ReadAsStringAsync();
        }

        private string GetConnectionData()
        {
            return JsonConvert.SerializeObject(hubs.Select(h => new ConnectionData() { Name = h.Key }));
        }
    }
}