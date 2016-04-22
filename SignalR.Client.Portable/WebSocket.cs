using System;
using System.Threading.Tasks;
using Websockets;

namespace SignalR.Client.Portable
{
    public class WebSocket
    {
        public event Action<string> MessageReceived;

        private IWebSocketConnection connection;

        private TaskCompletionSource<bool> webSocketOpened;
        private TaskCompletionSource<bool> webSocketClosed;

        public WebSocket()
        {
            connection = WebSocketFactory.Create();
            connection.OnOpened += OnOpened;
            connection.OnClosed += OnClosed;
            connection.OnError += OnError;
            connection.OnMessage += OnMessage;
        }

        public async Task Open(string url)
        {
            webSocketOpened = new TaskCompletionSource<bool>();

            connection.Open(url);

            await webSocketOpened.Task;
        }

        public async Task Close()
        {
            webSocketClosed = new TaskCompletionSource<bool>();

            connection.Close();

            await webSocketClosed.Task;
        }

        public void Send(string message)
        {
            connection.Send(message);
        }

        private void OnOpened()
        {
            webSocketOpened.SetResult(true);
        }

        private void OnClosed()
        {
            webSocketClosed.SetResult(true);
        }

        private void OnError(string error)
        {
            throw new Exception(error);
        }

        private void OnMessage(string message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}
