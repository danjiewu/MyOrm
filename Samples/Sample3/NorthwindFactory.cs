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
using log4net;
using System.Threading;

namespace Northwind
{
    public class NorthwindFactory
    {
        private static readonly object syncLock = new object();
        private static IDAOFactory _daoFactory;

        public static IDAOFactory DAOFactory
        {
            get
            {
                if (_daoFactory == null)
                {
                    GenerateFactoryProxy();
                }
                return NorthwindFactory._daoFactory;
            }
        }

        public static void GenerateFactoryProxy()
        {
            lock (syncLock)
            {
                if (_daoFactory == null)
                {
                    ProxyGenerator generator = new ProxyGenerator();
                    _daoFactory = new DAOFactory();
                    foreach (FieldInfo field in _daoFactory.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        object dao = field.GetValue(_daoFactory);
                        field.SetValue(_daoFactory, generator.CreateInterfaceProxyWithoutTarget(field.FieldType, dao.GetType().GetInterfaces(), new DAOInterceptor(field.FieldType.Name.Substring(1))));
                    }
                }
            }
        }

        public static void AsynInit()
        {
            new Thread(GenerateFactoryProxy).Start();
        }
    }

    public class DAOInterceptor : IInterceptor
    {
        private static ILog logger = LogManager.GetLogger("Northwind");
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
                    string args = String.Join(",", Array.ConvertAll<object, string>(invocation.Arguments, delegate(object o) { return o == null ? "null" : Convert.ToString(o); }));
                    logger.DebugFormat("DAO:{{{0}}} Method:{{{1}}} invoked, Args:{{{2}}}. {3} bytes to send.", daoName, invocation.Method.Name, args, ms.Length);
                    MemoryStream ret = new MemoryStream(remoteService.SerializedRemoteInvoke(ms.ToArray()));
                    invocation.ReturnValue = serializer.Deserialize(ret);
                    logger.DebugFormat("DAO:{{{0}}} Method:{{{1}}} finished. {2} bytes recieved.", daoName, invocation.Method.Name, ret.Length);
                }
            }
        }

        #endregion
    }
}
