using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region ProductsDAO
	/// <summary>
	/// DAO for object 'Products'.
	/// </summary>	
	public partial class ProductsDAO : ObjectDAO<Products>, IProductsDAO
	{
		public Products GetProductOfOrderDetail(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.ProductID);
		}
		
		public List<Products> GetAllWithCategory(Categories category)
		{
			return Search(new SimpleCondition(Products._CategoryID, category.CategoryID));
		}
		
		public List<Products> GetAllWithSupplier(Suppliers supplier)
		{
			return Search(new SimpleCondition(Products._SupplierID, supplier.SupplierID));
		}
		
	}
	#endregion
	
	#region ProductsViewDAO
	/// <summary>
	/// DAO for object's view 'ProductsView'.
	/// </summary>	
	public partial class ProductsViewDAO : ObjectViewDAO<ProductsView>, IProductsViewDAO
	{
		public ProductsView GetProductOfOrderDetail(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.ProductID);
		}
		
		public List<ProductsView> GetAllWithCategory(Categories category)
		{
			return Search(new SimpleCondition(ProductsView._CategoryID, category.CategoryID));
		}
		
		public List<ProductsView> GetAllWithSupplier(Suppliers supplier)
		{
			return Search(new SimpleCondition(ProductsView._SupplierID, supplier.SupplierID));
		}
		
	}
	#endregion	
}
