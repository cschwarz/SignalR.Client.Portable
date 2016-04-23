using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class TestHub : Hub
    {
        public static Action NoParametersCalled;
        public static Action TestQueryStringCalled;
        public static IEnumerable<KeyValuePair<string, string>> QueryString;

        public static Action OnConnectedCalled;
        public static Action OnDisconnectedCalled;

        public void NoParameters()
        {
            NoParametersCalled?.Invoke();
        }

        public void TestQueryString()
        {
            QueryString = Context.QueryString;
            TestQueryStringCalled?.Invoke();
        }

        public string Echo(string value)
        {
            return string.Concat("Echo: ", value);
        }

        public TestMessage EchoMessage(TestMessage message)
        {
            message.Value1 = string.Concat("Echo: ", message.Value1);
            Clients.All.MessageCallback(message);
            return message;
        }

        public int InvokeClientCallback()
        {
            Clients.All.ClientCallback();
            return 0;
        }

        public int InvokeClientCallback1(int value1)
        {
            Clients.All.ClientCallback(value1);
            return 1;
        }

        public void InvokeClientCallback2(int value1, int value2)
        {
            Clients.All.ClientCallback(value1, value2);
        }

        public void InvokeClientCallback3(int value1, int value2, int value3)
        {
            Clients.All.ClientCallback(value1, value2, value3);
        }

        public void InvokeClientCallback4(int value1, int value2, int value3, int value4)
        {
            Clients.All.ClientCallback(value1, value2, value3, value4);
        }

        public void InvokeClientCallback5(int value1, int value2, int value3, int value4, int value5)
        {
            Clients.All.ClientCallback(value1, value2, value3, value4, value5);
        }

        public void InvokeClientCallback6(int value1, int value2, int value3, int value4, int value5, int value6)
        {
            Clients.All.ClientCallback(value1, value2, value3, value4, value5, value6);
        }

        public void InvokeClientCallback7(int value1, int value2, int value3, int value4, int value5, int value6, int value7)
        {
            Clients.All.ClientCallback(value1, value2, value3, value4, value5, value6, value7);
        }

        public override Task OnConnected()
        {
            OnConnectedCalled?.Invoke();
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            OnDisconnectedCalled?.Invoke();
            return base.OnDisconnected(stopCalled);
        }
    }
}