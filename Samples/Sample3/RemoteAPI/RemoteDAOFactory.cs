using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Reflection;
using Northwind.Data;
using MyOrm.Common;

namespace Northwind.RemoteAPI
{
    public class RemoteDAOFactory
    {
        public static IDAOFactory GenerateRemoteHttpHandleFactory()
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<IDAOFactory>(new FactoryInteceptor(new HttpDispatcher()));
        }

        public static IDAOFactory GenerateRemoteWebServiceFactory()
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<IDAOFactory>(new FactoryInteceptor(new WebServiceDispatcher()));
        }
    }

    public class FactoryInteceptor : IInterceptor
    {
        private IRemoteDispatcher dispatcher;
        private Dictionary<MethodInfo, object> daoCache = new Dictionary<MethodInfo, object>();

        public FactoryInteceptor(IRemoteDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            if (!daoCache.ContainsKey(invocation.Method))
            {
                lock (daoCache)
                {
                    if (!daoCache.ContainsKey(invocation.Method))
                        daoCache[invocation.Method] = new ProxyGenerator().CreateInterfaceProxyWithoutTarget(invocation.Method.ReturnType, new Type[] { typeof(IObjectDAO), typeof(IObjectViewDAO) }, new RemoteInteceptor(invocation.Method.Name.Substring(4), dispatcher));
                }
            }
            invocation.ReturnValue = daoCache[invocation.Method];
        }

        #endregion
    }
}
