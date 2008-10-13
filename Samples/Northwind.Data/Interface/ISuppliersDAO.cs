using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ISuppliersDAO
	/// <summary>
	/// Interface of DAO for object 'Suppliers'.
	/// </summary>	
	public interface ISuppliersDAO : IObjectDAO<Suppliers>, IObjectDAO
	{
		Suppliers GetSuppliers(Products products);
	}
	#endregion
}
