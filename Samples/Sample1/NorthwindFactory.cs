using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;
using MyOrm.Common;

namespace Northwind
{
    public class NorthwindFactory
    {
        public static IDAOFactory DAOFactory = new DAOFactory();

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
