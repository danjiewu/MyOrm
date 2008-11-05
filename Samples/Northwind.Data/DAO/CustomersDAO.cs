using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomersDAO
	/// <summary>
	/// DAO for object 'Customers'.
	/// </summary>	
	public partial class CustomersDAO : ObjectDAO<Customers>, ICustomersDAO
	{
		public Customers GetCustomerOfOrder(Orders orders)
		{
			return GetObject(orders.CustomerID);
		}
		
		public Customers GetCustomerOfCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo)
		{
			return GetObject(customerCustomerDemo.CustomerID);
		}
		
	}
	#endregion
}
