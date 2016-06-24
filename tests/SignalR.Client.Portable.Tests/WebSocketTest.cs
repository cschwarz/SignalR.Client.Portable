using System;
using System.Threading;
using Xunit;

namespace SignalR.Client.Portable.Tests
{
    public class WebSocketTest : TestBase
    {
        public const string TestServerUrl = "http://echo.websocket.org";

        [Fact]
        public async void ConnectToInvalidUri_ShouldThrowException()
        {
            using (WebSocket webSocket = new WebSocket())
                await Assert.ThrowsAsync<Exception>(() => webSocket.Open("InvalidUrl"));
        }

        [Fact]
        public async void Open_ShouldCallOpened()
        {
            using (WebSocket webSocket = new WebSocket())
            {
                ManualResetEvent resetEvent = new ManualResetEvent(false);
                webSocket.Opened += () => { resetEvent.Set(); };

                await webSocket.Open(TestServerUrl);

                Assert.True(resetEvent.WaitOne(3000));
            }
        }

        [Fact]
        public async void Close_ShouldCallClosed()
        {
            using (WebSocket webSocket = new WebSocket())
            {
                ManualResetEvent resetEvent = new ManualResetEvent(false);
                webSocket.Closed += () => { resetEvent.Set(); };

                await webSocket.Open(TestServerUrl);

                await webSocket.Close();

                Assert.True(resetEvent.WaitOne(3000));
            }
        }
    }
}