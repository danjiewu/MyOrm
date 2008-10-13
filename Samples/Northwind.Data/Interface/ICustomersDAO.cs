using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICustomersDAO
	/// <summary>
	/// Interface of DAO for object 'Customers'.
	/// </summary>	
	public interface ICustomersDAO : IObjectDAO<Customers>, IObjectDAO
	{
		Customers GetCustomers(Orders orders);
		Customers GetCustomers(CustomerCustomerDemo customerCustomerDemo);
	}
	#endregion
}
