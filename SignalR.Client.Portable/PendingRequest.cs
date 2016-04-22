using System;
using System.Reflection;

namespace SignalR.Client.Portable
{
    public class PendingRequest
    {
        public object Source { get; private set; }
        public Type ResultType { get; private set; }
        public MethodInfo SetResultMethod { get; private set; }

        public PendingRequest(object source, Type resultType, MethodInfo setResultMethod)
        {
            Source = source;
            ResultType = resultType;
            SetResultMethod = setResultMethod;
        }
    }
}
