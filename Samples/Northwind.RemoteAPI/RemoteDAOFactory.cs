using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Castle.Core.Interceptor;
using System.Reflection;
using Northwind.Protocal;

namespace Northwind.Business
{
    public class RemoteServiceFactory
    {
        public static IServiceFactory GenerateRemoteHttpHandleFactory(string serviceUrl)
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<IServiceFactory>(new FactoryInteceptor(new HttpDispatcher(serviceUrl)));
        }

        public static IServiceFactory GenerateRemoteWebServiceFactory()
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<IServiceFactory>(new FactoryInteceptor(new WebServiceDispatcher()));
        }
    }

    public class FactoryInteceptor : IInterceptor
    {
        private IRemoteDispatcher dispatcher;
        private Dictionary<MethodInfo, object> serviceCache = new Dictionary<MethodInfo, object>();

        public FactoryInteceptor(IRemoteDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            if (!serviceCache.ContainsKey(invocation.Method))
            {
                lock (serviceCache)
                {
                    if (!serviceCache.ContainsKey(invocation.Method))
                        serviceCache[invocation.Method] = new ProxyGenerator().CreateInterfaceProxyWithoutTarget(invocation.Method.ReturnType, invocation.Method.ReturnType.GetInterfaces(), new RemoteInteceptor(invocation.Method.Name.Substring(4), dispatcher));
                }
            }
            invocation.ReturnValue = serviceCache[invocation.Method];
        }

        #endregion
    }
}
