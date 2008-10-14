using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;

namespace Northwind
{
    public class NorthwindFactory
    {
        public static IDAOFactory DAOFactory = new DAOFactory();
    }
}
