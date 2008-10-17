using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;
using Castle.DynamicProxy;
using System.Reflection;
using MyOrm;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Castle.Core.Interceptor;
using Northwind.RemoteService;

namespace Northwind
{
    public class NorthwindFactory
    {
        public static IDAOFactory DAOFactory = GenerateFactoryProxy();

        private static IDAOFactory GenerateFactoryProxy()
        {
            ProxyGenerator generator = new ProxyGenerator();
            IDAOFactory factory = new DAOFactory();
            foreach (FieldInfo field in factory.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                object dao = field.GetValue(factory);
                field.SetValue(factory, generator.CreateInterfaceProxyWithoutTarget(field.FieldType, dao.GetType().GetInterfaces(), new DAOInterceptor(field.FieldType.Name.Substring(1))));
            }
            return factory;
        }
    }

    public class DAOInterceptor : IInterceptor
    {
        private static readonly object syncLock = new object();
        private static RemoteDAOService remoteService = new RemoteDAOService();

        private string daoName;
        public DAOInterceptor(string daoName)
        {
            this.daoName = daoName;
        }
        #region IInterceptor 成员

        public void Intercept(IInvocation invocation)
        {
            lock (syncLock)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(ms, daoName);
                    serializer.Serialize(ms, invocation.Method);
                    serializer.Serialize(ms, invocation.Arguments);
                    invocation.ReturnValue = serializer.Deserialize(new MemoryStream(remoteService.SerializedRemoteInvoke(ms.ToArray())));
                }
            }
        }

        #endregion
    }
}
