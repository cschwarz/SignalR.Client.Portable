using System.Threading.Tasks;

namespace SignalR.Client.Portable
{
    public interface IHubProxy
    {
        Task Invoke(string method, params object[] args);
        Task<T> Invoke<T>(string method, params object[] args);
        Subscription Subscribe(string eventName);
    }
}
