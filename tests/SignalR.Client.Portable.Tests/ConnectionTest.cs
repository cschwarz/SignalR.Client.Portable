using System.Threading;
using Xunit;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class ConnectionTest : TestBase
    {
        [Fact(Skip = "Flaky")]
        public async void Send()
        {
            using (Connection connection = new Connection(BaseUrl + "/test"))
            {
                ManualResetEvent resetEvent = new ManualResetEvent(false);

                string data = string.Empty;

                connection.Received += d => { data = d; resetEvent.Set(); };

                await connection.Start();

                await connection.Send("Test");

                Assert.True(resetEvent.WaitOne(3000));
                Assert.Equal(string.Format("{0}: Test", connection.ConnectionId), data);
            }
        }
    }
}