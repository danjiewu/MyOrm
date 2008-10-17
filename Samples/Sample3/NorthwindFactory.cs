using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;
using Castle.DynamicProxy;
using System.Reflection;
using MyOrm;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Northwind.Northwind;

namespace Northwind
{
    public class NorthwindFactory
    {
        public static IDAOFactory DAOFactory = GenerateFactoryProxy();

        private static IDAOFactory GenerateFactoryProxy()
        {
            ProxyGenerator generator = new ProxyGenerator();
            DAOInterceptor interceptor = new DAOInterceptor();
            IDAOFactory factory = new DAOFactory();
            foreach (FieldInfo field in factory.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                object dao = field.GetValue(factory);
                field.SetValue(factory, generator.CreateProxy(dao.GetType().GetInterfaces(), interceptor, dao));
            }
            return factory;
        }
    }

    public class DAOInterceptor : IInterceptor
    {
        private static readonly object syncLock = new object();
        private static RemoteDAOService remoteService = new RemoteDAOService();
        #region IInterceptor 成员

        public object Intercept(IInvocation invocation, params object[] args)
        {
            lock (syncLock)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(ms, invocation.Method);
                    serializer.Serialize(ms, args);
                    return serializer.Deserialize(new MemoryStream(remoteService.SerializedRemoteInvoke(ms.ToArray())));
                }
            }
        }

        #endregion
    }
}
