using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace SignalR.Client.Portable
{
    public static class HubProxyExtensions
    {
        public static IDisposable On(this IHubProxy hubProxy, string eventName, Action onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData();
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T>(this IHubProxy hubProxy, string eventName, Action<T> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2>(this IHubProxy hubProxy, string eventName, Action<T1, T2> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2, T3>(this IHubProxy hubProxy, string eventName, Action<T1, T2, T3> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>(), data[2].ToObject<T3>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2, T3, T4>(this IHubProxy hubProxy, string eventName, Action<T1, T2, T3, T4> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>(), data[2].ToObject<T3>(), data[3].ToObject<T4>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2, T3, T4, T5>(this IHubProxy hubProxy, string eventName, Action<T1, T2, T3, T4, T5> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>(), data[2].ToObject<T3>(), data[3].ToObject<T4>(), data[4].ToObject<T5>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2, T3, T4, T5, T6>(this IHubProxy hubProxy, string eventName, Action<T1, T2, T3, T4, T5, T6> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>(), data[2].ToObject<T3>(), data[3].ToObject<T4>(), data[4].ToObject<T5>(), data[5].ToObject<T6>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }

        public static IDisposable On<T1, T2, T3, T4, T5, T6, T7>(this IHubProxy hubProxy, string eventName, Action<T1, T2, T3, T4, T5, T6, T7> onData)
        {
            Subscription subscription = hubProxy.Subscribe(eventName);
            Action<IList<JToken>> received = data => onData(data[0].ToObject<T1>(), data[1].ToObject<T2>(), data[2].ToObject<T3>(), data[3].ToObject<T4>(), data[4].ToObject<T5>(), data[5].ToObject<T6>(), data[6].ToObject<T7>());
            subscription.Received += received;
            return new DisposableAction(() => subscription.Received -= received);
        }
    }
}
