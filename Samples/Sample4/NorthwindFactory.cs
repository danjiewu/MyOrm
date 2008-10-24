using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;
using Castle.DynamicProxy;
using System.Reflection;
using MyOrm;
using System.Data;
using MyOrm.Common;

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

        public static IObjectDAO GetObjectDAO(Type objectType)
        {
            return DAOFactoryUtil.GetObjectDAO(DAOFactory, objectType);
        }

        public static IObjectViewDAO GetObjectViewDAO(Type objectType)
        {
            return DAOFactoryUtil.GetObjectViewDAO(DAOFactory, objectType);
        }
    }

    public class DAOInterceptor : IInterceptor
    {
        private static readonly object syncLock = new object();
        #region IInterceptor 成员

        public object Intercept(IInvocation invocation, params object[] args)
        {
            lock (syncLock)
            {
                ObjectDAOBase dao = (ObjectDAOBase)invocation.InvocationTarget;
                if (dao.Connection.State == ConnectionState.Closed) dao.Connection.Open();
                object ret = invocation.Proceed(args);
                if (dao.Connection.State == ConnectionState.Open) dao.Connection.Close();
                return ret;
            }
        }

        #endregion
    }
}
