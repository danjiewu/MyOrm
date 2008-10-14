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
	public interface ISuppliersDAO : IObjectDAO<Suppliers>, IObjectViewDAO<Suppliers>, IObjectDAO, IObjectViewDAO
	{
		Suppliers GetSupplierOfProduct(Products products);
	}
	#endregion
}
