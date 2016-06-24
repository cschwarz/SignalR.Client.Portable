using System;
using System.Threading.Tasks;
using Websockets;

namespace SignalR.Client.Portable
{
    public class WebSocket : IDisposable
    {
        public event Action<string> MessageReceived;
        public event Action Opened;
        public event Action Closed;        

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

        public void Dispose()
        {
            connection.OnOpened -= OnOpened;
            connection.OnClosed -= OnClosed;
            connection.OnError -= OnError;
            connection.OnMessage -= OnMessage;

            connection.Dispose();
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
            if (webSocketOpened != null)
                webSocketOpened.SetResult(true);

            Opened?.Invoke();
        }

        private void OnClosed()
        {
            if (webSocketClosed != null)
                webSocketClosed.SetResult(true);

            Closed?.Invoke();
        }

        private void OnError(string error)
        {
            Exception exception = new Exception(error);

            if (webSocketOpened != null)
                webSocketOpened.SetException(exception);
            else if (webSocketClosed != null)
                webSocketClosed.SetException(exception);
            else
                throw exception;
        }

        private void OnMessage(string message)
        {
            MessageReceived?.Invoke(message);
        }
    }
}
