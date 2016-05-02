using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class TestPersistentConnection : PersistentConnection
    {
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return Task.Delay(10).ContinueWith(t => Connection.Broadcast(string.Format("{0}: {1}", connectionId, data)));            
        }
    }
}
