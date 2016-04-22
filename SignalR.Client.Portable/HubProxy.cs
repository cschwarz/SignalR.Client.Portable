using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SignalR.Client.Portable
{
    public class HubProxy : IHubProxy
    {
        private HubConnection connection;
        private string hubName;

        private ConcurrentDictionary<string, PendingRequest> pendingRequests;
        private ConcurrentDictionary<string, Subscription> subscriptions;

        public HubProxy(HubConnection connection, string hubName)
        {
            this.connection = connection;
            this.hubName = hubName;

            pendingRequests = new ConcurrentDictionary<string, PendingRequest>();
            subscriptions = new ConcurrentDictionary<string, Subscription>();
        }

        public Task Invoke(string method, params object[] args)
        {
            MessageRequest request = new MessageRequest(hubName, method, args.Select(a => JToken.FromObject(a)).ToArray());
            connection.Send(JsonConvert.SerializeObject(request));
            return Task.FromResult<object>(null);
        }

        public Task<T> Invoke<T>(string method, params object[] args)
        {
            MessageRequest request = new MessageRequest(hubName, method, args.Select(a => JToken.FromObject(a)).ToArray());

            TaskCompletionSource<T> taskSource = new TaskCompletionSource<T>();
            MethodInfo setResultMethod = typeof(TaskCompletionSource<T>).GetRuntimeMethod("SetResult", new Type[] { typeof(T) });

            if (!pendingRequests.TryAdd(request.InvocationIdentifier, new PendingRequest(taskSource, typeof(T), setResultMethod)))
                throw new Exception();

            connection.Send(JsonConvert.SerializeObject(request));

            return taskSource.Task;
        }

        public void MessageReceived(MessageResponse response)
        {
            PendingRequest pendingRequest = null;

            if (!string.IsNullOrEmpty(response.InvocationIdentifier) && pendingRequests.TryGetValue(response.InvocationIdentifier, out pendingRequest))
            {
                pendingRequest.SetResultMethod.Invoke(pendingRequest.Source, new object[] { response.Result.ToObject(pendingRequest.ResultType) });
            }
            else if (!string.IsNullOrEmpty(response.MessageId))
            {
                foreach (Message message in response.Messages)
                {
                    Subscription subscription = null;
                    if (message.HubName == hubName && subscriptions.TryGetValue(message.MethodName, out subscription))
                        subscription.OnReceived(message.Arguments);
                }
            }
        }

        public Subscription Subscribe(string eventName)
        {
            return subscriptions.GetOrAdd(eventName, e => new Subscription());
        }
    }
}
