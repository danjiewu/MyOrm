using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Northwind.RemoteAPI;
using Northwind.Data;
using MyOrm.Common;

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
                    _daoFactory = RemoteDAOFactory.GenerateRemoteHttpHandleFactory();
                    //_daoFactory = RemoteDAOFactory.GenerateRemoteWebServiceFactory();
                }
            }
        }

        public static void AsynInit()
        {
            new Thread(GenerateFactoryProxy).Start();
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
}
