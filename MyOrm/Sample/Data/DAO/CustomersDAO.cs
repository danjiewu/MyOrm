using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomersDAO
	/// <summary>
	/// DAO for object 'Customers'.
	/// </summary>	
	public class CustomersDAO : ObjectDAO<Customers>, ICustomersDAO
	{
	}
	#endregion
}
