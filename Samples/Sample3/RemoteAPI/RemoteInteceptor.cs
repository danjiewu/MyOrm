using System;
using System.Collections.Generic;
using System.Text;
using Castle.Core.Interceptor;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;

namespace Northwind.RemoteAPI
{
    public class RemoteInteceptor : IInterceptor
    {
        private string daoName;
        private IRemoteDispatcher dispatcher;

        public RemoteInteceptor(string daoName, IRemoteDispatcher dispatcher)
        {
            this.daoName = daoName;
            this.dispatcher = dispatcher;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = dispatcher.RemoteInvoke(daoName, invocation.Method, invocation.Arguments);
        }

        #endregion
    }    
}
