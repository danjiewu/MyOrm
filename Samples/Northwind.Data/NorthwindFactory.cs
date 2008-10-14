using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Data
{
    public class NorthwindFactory
    {
        public static IDAOFactory DAOFactory = new DAOFactory();
    }
}
