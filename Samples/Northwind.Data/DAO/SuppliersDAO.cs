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
	public partial class SuppliersDAO : ObjectDAO<Suppliers>, ISuppliersDAO
	{
		public Suppliers GetSupplierOfProduct(Products products)
		{
			return GetObject(products.SupplierID);
		}
		
	}
	#endregion
}
