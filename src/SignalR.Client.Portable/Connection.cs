using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SignalR.Client.Portable
{
    public class Connection : IDisposable
    {
        public string ConnectionToken { get; private set; }
        public string ConnectionId { get; private set; }
        public ConnectionState State { get; private set; }
        public string QueryString { get; private set; }
        public string Url { get; private set; }

        public event Action<string> Received;
        public event Action Closed;
        public event Action<Exception> Error;

        private WebSocket webSocket;

        public Connection(string url)
            : this(url, (string)null)
        {
        }

        public Connection(string url, IDictionary<string, string> queryString)
            : this(url, new FormUrlEncodedContent(queryString).ReadAsStringAsync().Result)
        {
        }

        public Connection(string url, string queryString)
        {
            Url = url;

            if (!Url.EndsWith("/"))
                Url += "/";

            Url += "signalr/";

            webSocket = new WebSocket();
            webSocket.Opened += WebSocketOpened;
            webSocket.Closed += WebSocketClosed;
            webSocket.MessageReceived += MessageReceived;

            State = ConnectionState.Disconnected;
            QueryString = queryString;
        }

        public async Task Start()
        {
            State = ConnectionState.Connecting;

            await Negotiate();

            await Connect();

            await StartConnection();
        }

        public async void Stop()
        {
            await webSocket.Close();

            await Abort();
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

            webSocket.Opened -= WebSocketOpened;
            webSocket.Closed -= WebSocketClosed;
            webSocket.MessageReceived -= MessageReceived;
            webSocket.Dispose();
        }

        private void WebSocketOpened()
        {
            State = ConnectionState.Connected;
        }

        private void WebSocketClosed()
        {
            State = ConnectionState.Disconnected;

            OnClosed();
        }
        
        protected virtual void OnClosed()
        {
            Closed?.Invoke();
        }

        private void MessageReceived(string message)
        {
            MessageResponse response = JsonConvert.DeserializeObject<MessageResponse>(message);

            MessageReceived(response);
        }

        internal virtual void MessageReceived(MessageResponse response)
        {
            foreach (JToken message in response.Messages)
                Received?.Invoke(message.ToObject<string>());
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

        protected virtual string GetConnectionData()
        {
            return string.Empty;
        }
    }
}
