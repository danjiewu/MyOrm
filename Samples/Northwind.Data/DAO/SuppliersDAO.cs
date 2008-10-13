using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region SuppliersDAO
	/// <summary>
	/// DAO for object 'Suppliers'.
	/// </summary>	
	public class SuppliersDAO : ObjectDAO<Suppliers>, ISuppliersDAO
	{
		public Suppliers GetSuppliers(Products products)
		{
			return GetObject(products.SupplierID);
		}
		
	}
	#endregion
}
