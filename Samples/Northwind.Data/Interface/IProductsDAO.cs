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
	public interface IProductsDAO : IObjectDAO<Products>, IObjectViewDAO<Products>, IObjectDAO, IObjectViewDAO
	{
		Products GetProductOfOrderDetail(OrderDetails orderDetails);
		List<Products> GetAllWithCategory(Categories category);
		List<Products> GetAllWithSupplier(Suppliers supplier);
	}
	#endregion
	
	#region IProductsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'ProductsView'.
	/// </summary>	
	public interface IProductsViewDAO : IObjectViewDAO<ProductsView>, IObjectViewDAO
	{
		ProductsView GetProductOfOrderDetail(OrderDetails orderDetails);
		List<ProductsView> GetAllWithCategory(Categories category);
		List<ProductsView> GetAllWithSupplier(Suppliers supplier);
	}
	#endregion	
}
