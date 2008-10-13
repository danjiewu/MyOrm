using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region SuppliersDAO
	/// <summary>
	/// DAO for object 'Suppliers'.
	/// </summary>	
	public class SuppliersDAO : ObjectDAO<Suppliers>, ISuppliersDAO
	{
	}
	#endregion
}
