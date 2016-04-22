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

        public void InvokeClient(int value)
        {
            Clients.All.ClientCallback(value);
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
