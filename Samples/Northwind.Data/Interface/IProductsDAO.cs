using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IProductsDAO
	/// <summary>
	/// Interface of DAO for object 'Products'.
	/// </summary>	
	public interface IProductsDAO : IObjectDAO<Products>, IObjectDAO
	{		
	}
	#endregion
	
	#region IProductsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'ProductsView'.
	/// </summary>	
	public interface IProductsViewDAO : IObjectViewDAO<ProductsView>, IObjectViewDAO
	{
	}
	#endregion	
}
