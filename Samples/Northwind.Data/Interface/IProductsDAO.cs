using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IProductsDAO
	/// <summary>
	/// Interface of DAO for object 'Products'.
	/// </summary>	
	public interface IProductsDAO : IObjectDAO<Products>, IObjectDAO
	{
		Products GetProducts(OrderDetails orderDetails);
		List<Products> GetAllWithCategories(Categories categories);
		List<Products> GetAllWithSuppliers(Suppliers suppliers);
	}
	#endregion
	
	#region IProductsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'ProductsView'.
	/// </summary>	
	public interface IProductsViewDAO : IObjectViewDAO<ProductsView>, IObjectViewDAO
	{
		ProductsView GetProducts(OrderDetails orderDetails);
		List<ProductsView> GetAllWithCategories(Categories categories);
		List<ProductsView> GetAllWithSuppliers(Suppliers suppliers);
	}
	#endregion	
}
