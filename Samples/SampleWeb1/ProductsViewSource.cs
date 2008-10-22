using System;
using System.Data;
using Northwind.Data;
using System.ComponentModel;
using MyOrm.Common;
using System.Collections.Generic;

namespace Northwind
{
    [DataObject]
    public class ProductsViewSource
    {
        public List<ProductsView> Select(Condition condition, int startRowIndex, int maximumRows, string orderBy)
        {
            bool desc = false;
            if (!String.IsNullOrEmpty(orderBy))
            {
                string[] args = orderBy.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                orderBy = args[0];
                desc = args.Length > 1 && String.Compare(args[1], "desc", true) == 0;
            }
            return NorthwindFactory.DAOFactory.ProductsViewDAO.SearchSection(condition, startRowIndex, maximumRows, orderBy, desc);
        }

        public int Count(Condition condition)
        {
            return NorthwindFactory.DAOFactory.ProductsViewDAO.Count(condition);
        }
    }
}
