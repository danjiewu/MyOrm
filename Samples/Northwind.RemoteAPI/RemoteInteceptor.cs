using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Interceptor;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

namespace Northwind.Protocal
{
    public class RemoteInteceptor : IInterceptor
    {
        public RemoteInteceptor(string serviceName, IRemoteDispatcher dispatcher)
        {
            this.serviceName = serviceName;
            this.dispatcher = dispatcher;
        }

        private string serviceName;
        private IRemoteDispatcher dispatcher;

        public string ServiceName
        {
            get { return serviceName; }
        }       

        public IRemoteDispatcher Dispatcher
        {
            get { return dispatcher; }
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = Dispatcher.RemoteInvoke(ServiceName, invocation.Method, invocation.Arguments);
        }

        #endregion
    }

    public interface IRemoteDispatcher
    {
        object RemoteInvoke(string serviceName, MethodInfo method, object[] args);
    }
}
