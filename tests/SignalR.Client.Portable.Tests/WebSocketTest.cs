using System;
using Xunit;

namespace SignalR.Client.Portable.Tests
{
    public class WebSocketTest : TestBase
    {
        [Fact]
        public async void ConnectToInvalidUri_ShouldThrowException()
        {
            WebSocket webSocket = new WebSocket();
            await Assert.ThrowsAsync<Exception>(() => webSocket.Open("InvalidUrl"));
        }
    }
}
