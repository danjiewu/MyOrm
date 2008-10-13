using System;
using System.Data;
using Data.Common;

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
