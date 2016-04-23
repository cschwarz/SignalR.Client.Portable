using System.Threading;
using Xunit;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class HubProxyExtensionsTest : TestBase
    {
        [Fact]
        public async void On()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On("ClientCallback", () => { resetEvent.Set(); });

                int temp = await hubProxy.Invoke<int>("InvokeClientCallback");

                Assert.True(resetEvent.WaitOne(3000));
            }
        }

        [Fact]
        public async void On1()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;

                hubProxy.On<int>("ClientCallback", (v1) => { value1 = v1; resetEvent.Set(); });

                int temp = await hubProxy.Invoke<int>("InvokeClientCallback1", 1);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
            }
        }

        [Fact]
        public async void On2()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;

                hubProxy.On<int, int>("ClientCallback", (v1, v2) => { value1 = v1; value2 = v2; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback2", 1, 2);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
            }
        }

        [Fact]
        public async void On3()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;
                int value3 = 0;

                hubProxy.On<int, int, int>("ClientCallback", (v1, v2, v3) => { value1 = v1; value2 = v2; value3 = v3; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback3", 1, 2, 3);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
                Assert.Equal(3, value3);
            }
        }

        [Fact]
        public async void On4()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;
                int value3 = 0;
                int value4 = 0;

                hubProxy.On<int, int, int, int>("ClientCallback", (v1, v2, v3, v4) => { value1 = v1; value2 = v2; value3 = v3; value4 = v4; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback4", 1, 2, 3, 4);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
                Assert.Equal(3, value3);
                Assert.Equal(4, value4);
            }
        }

        [Fact]
        public async void On5()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;
                int value3 = 0;
                int value4 = 0;
                int value5 = 0;

                hubProxy.On<int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5) => { value1 = v1; value2 = v2; value3 = v3; value4 = v4; value5 = v5; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback5", 1, 2, 3, 4, 5);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
                Assert.Equal(3, value3);
                Assert.Equal(4, value4);
                Assert.Equal(5, value5);
            }
        }

        [Fact]
        public async void On6()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;
                int value3 = 0;
                int value4 = 0;
                int value5 = 0;
                int value6 = 0;

                hubProxy.On<int, int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5, v6) => { value1 = v1; value2 = v2; value3 = v3; value4 = v4; value5 = v5; value6 = v6; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback6", 1, 2, 3, 4, 5, 6);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
                Assert.Equal(3, value3);
                Assert.Equal(4, value4);
                Assert.Equal(5, value5);
                Assert.Equal(6, value6);
            }
        }

        [Fact]
        public async void On7()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                int value1 = 0;
                int value2 = 0;
                int value3 = 0;
                int value4 = 0;
                int value5 = 0;
                int value6 = 0;
                int value7 = 0;

                hubProxy.On<int, int, int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5, v6, v7) => { value1 = v1; value2 = v2; value3 = v3; value4 = v4; value5 = v5; value6 = v6; value7 = v7; resetEvent.Set(); });

                await hubProxy.Invoke("InvokeClientCallback7", 1, 2, 3, 4, 5, 6, 7);

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(1, value1);
                Assert.Equal(2, value2);
                Assert.Equal(3, value3);
                Assert.Equal(4, value4);
                Assert.Equal(5, value5);
                Assert.Equal(6, value6);
                Assert.Equal(7, value7);
            }
        }

        [Fact]
        public async void On_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On("ClientCallback", () => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback");

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On1_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int>("ClientCallback", (v1) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback1", 1);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On2_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int>("ClientCallback", (v1, v2) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback2", 1, 2);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On3_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int, int>("ClientCallback", (v1, v2, v3) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback3", 1, 2, 3);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On4_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int, int, int>("ClientCallback", (v1, v2, v3, v4) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback4", 1, 2, 3, 4);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On5_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback5", 1, 2, 3, 4, 5);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On6_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5, v6) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback6", 1, 2, 3, 4, 5, 6);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }

        [Fact]
        public async void On7_Disposed()
        {
            using (HubConnection connection = new HubConnection(BaseUrl))
            {
                IHubProxy hubProxy = connection.CreateHubProxy("TestHub");
                await connection.Start();

                ManualResetEvent resetEvent = new ManualResetEvent(false);

                hubProxy.On<int, int, int, int, int, int, int>("ClientCallback", (v1, v2, v3, v4, v5, v6, v7) => { resetEvent.Set(); }).Dispose();

                await hubProxy.Invoke("InvokeClientCallback7", 1, 2, 3, 4, 5, 6, 7);

                Assert.False(resetEvent.WaitOne(1000));
            }
        }
    }
}