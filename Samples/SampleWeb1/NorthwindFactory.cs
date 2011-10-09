using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using System.Reflection;
using System.Data;

namespace Northwind.Business
{
    public class NorthwindFactory
    {
        public static IServiceFactory ServiceFactory = GenerateFactoryProxy();

        private static IServiceFactory GenerateFactoryProxy()
        {
            ProxyGenerator generator = new ProxyGenerator();
            ServiceInterceptor interceptor = new ServiceInterceptor();
            IServiceFactory factory = new ServiceFactory();
            foreach (FieldInfo field in factory.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                object service = field.GetValue(factory);
                field.SetValue(factory, generator.CreateProxy(field.FieldType, interceptor, service));
            }
            return factory;
        }
    }

    public class ServiceInterceptor : IInterceptor
    {
        private static readonly object syncLock = new object();
        #region IInterceptor 成员

        public object Intercept(IInvocation invocation, params object[] args)
        {
            lock (syncLock)
            {
                if (MyOrm.Configuration.DefaultConnection.State == ConnectionState.Closed) MyOrm.Configuration.DefaultConnection.Open();
                return invocation.Proceed(args);
            }
        }

        #endregion
    }
}
