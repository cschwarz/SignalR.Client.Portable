using Microsoft.Owin.Hosting;
using System;
using Websockets.Net;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class TestBase
    {
#if REFERENCE_IMPLEMENTATION
        protected const string BaseUrl = "http://localhost:8080";
#else
        protected const string BaseUrl = "http://localhost:8081";
#endif

        private static IDisposable webApp;

        static TestBase()
        {
            WebsocketConnection.Link();
            webApp = WebApp.Start<Startup>(BaseUrl);
        }
    }
}
