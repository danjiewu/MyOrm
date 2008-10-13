using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICustomersDAO
	/// <summary>
	/// Interface of DAO for object 'Customers'.
	/// </summary>	
	public interface ICustomersDAO : IObjectDAO<Customers>, IObjectDAO
	{		
	}
	#endregion
}