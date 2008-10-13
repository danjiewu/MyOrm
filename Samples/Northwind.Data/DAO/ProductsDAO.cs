using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region ProductsDAO
	/// <summary>
	/// DAO for object 'Products'.
	/// </summary>	
	public class ProductsDAO : ObjectDAO<Products>, IProductsDAO
	{
	}
	#endregion
	
	#region ProductsViewDAO
	/// <summary>
	/// DAO for object's view 'ProductsView'.
	/// </summary>	
	public class ProductsViewDAO : ObjectViewDAO<ProductsView>, IProductsViewDAO
	{
	}
	#endregion	
}
