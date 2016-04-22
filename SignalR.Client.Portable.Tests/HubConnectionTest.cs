using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Websockets.Net;
using Xunit;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class HubConnectionTest
    {
#if REFERENCE_IMPLEMENTATION
        private const string BaseUrl = "http://localhost:8080";
#else
        private const string BaseUrl = "http://localhost:8081";
#endif

        private static IDisposable webApp;

        static HubConnectionTest()
        {
            WebsocketConnection.Link();
            webApp = WebApp.Start<Startup>(BaseUrl);
        }

        [Fact]
        public async void InvokeWithParameter()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                Assert.Equal("Echo: Test", await hubProxy.Invoke<string>("Echo", "Test"));
            }
        }

        [Fact]
        public async void InvokeWithoutParameter()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                TestHub.NoParametersCalled = () => resetEvent.Set();

                await hubProxy.Invoke("NoParameters");

                Assert.True(resetEvent.WaitOne(3000));
            }
        }

        [Fact]
        public void QueryStringProperty()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
                Assert.Null(connection.QueryString);

            using (HubConnection connection = new HubConnection(BaseUrl, "a=b"))
                Assert.Equal("a=b", connection.QueryString);

            using (HubConnection connection = new HubConnection(BaseUrl, new Dictionary<string, string>() { { "a", "b" } }))
                Assert.Equal("a=b", connection.QueryString);
        }

        [Fact]
        public void UrlProperty()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
                Assert.Equal(string.Concat(BaseUrl, "/signalr/"), connection.Url);

            using (HubConnection connection = new HubConnection(BaseUrl, "a=b"))
                Assert.Equal(string.Concat(BaseUrl, "/signalr/"), connection.Url);

            using (HubConnection connection = new HubConnection(BaseUrl, new Dictionary<string, string>() { { "a", "b" } }))
                Assert.Equal(string.Concat(BaseUrl, "/signalr/"), connection.Url);
        }

        [Fact]
        public async void TestQueryString()
        {
            using (HubConnection connection = new HubConnection(BaseUrl, new Dictionary<string, string>() { { "a", "b" } }))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                TestHub.TestQueryStringCalled = () => resetEvent.Set();

                await hubProxy.Invoke("TestQueryString");

                Assert.True(resetEvent.WaitOne(3000));
                Assert.True(TestHub.QueryString.Contains(new KeyValuePair<string, string>("a", "b")));
            }
        }

        [Fact]
        public async void InvokeClientCallback()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value = 0;

                hubProxy.On<int>("ClientCallback", v => { value = v; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClient", 5);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(5, value);
            }
        }

        [Fact]
        public async void InvokeDisposedClientCallback()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value = 0;

                IDisposable callback = hubProxy.On<int>("ClientCallback", v => { value = v; resetEvent.Set(); });
                callback.Dispose();

                await hubProxy.Invoke("InvokeClient", 5);

                Assert.False(resetEvent.WaitOne(1000));
                Assert.Equal(0, value);
            }
        }

        [Fact]
        public async void InvokeJsonMessage()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);
                TestMessage callbackMessage = null;

                hubProxy.On<TestMessage>("MessageCallback", m => { callbackMessage = m; resetEvent.Set(); });

                TestMessage message = await hubProxy.Invoke<TestMessage>("EchoMessage", new TestMessage() { Value1 = "Value1", Value2 = 2 });

                Assert.Equal("Echo: Value1", message.Value1);
                Assert.Equal(2, message.Value2);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal("Echo: Value1", callbackMessage.Value1);
                Assert.Equal(2, callbackMessage.Value2);
            }
        }

        [Fact]
        public async void OnConnectedCalled()
        {
            ManualResetEvent resetEvent = new ManualResetEvent(false);

            TestHub.OnConnectedCalled = () => resetEvent.Set();

            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                Assert.True(resetEvent.WaitOne(3000));
            }
        }

        [Fact(Skip = "Flaky")]
        public async void OnDisconnectedCalled()
        {
            ManualResetEvent resetEvent = new ManualResetEvent(false);

            TestHub.OnDisconnectedCalled = () => resetEvent.Set();

            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                connection.Stop();

                Assert.True(resetEvent.WaitOne(3000));
            }
        }
    }
}
