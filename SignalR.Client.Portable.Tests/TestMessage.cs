#if REFERENCE_IMPLEMENTATION
namespace Microsoft.AspNet.SignalR.Client.Tests
#else
namespace SignalR.Client.Portable.Tests
#endif
{
    public class TestMessage
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
    }
}
