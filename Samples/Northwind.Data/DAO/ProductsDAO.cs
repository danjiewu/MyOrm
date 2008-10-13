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
	public class ProductsDAO : ObjectDAO<Products>, IProductsDAO
	{
		public Products GetProducts(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.ProductID);
		}
		
		public List<Products> GetAllWithCategories(Categories categories)
		{
			return Search(new SimpleCondition(Products._CategoryID, categories.CategoryID));
		}
		
		public List<Products> GetAllWithSuppliers(Suppliers suppliers)
		{
			return Search(new SimpleCondition(Products._SupplierID, suppliers.SupplierID));
		}
		
	}
	#endregion
	
	#region ProductsViewDAO
	/// <summary>
	/// DAO for object's view 'ProductsView'.
	/// </summary>	
	public class ProductsViewDAO : ObjectViewDAO<ProductsView>, IProductsViewDAO
	{
		public ProductsView GetProducts(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.ProductID);
		}
		
		public List<ProductsView> GetAllWithCategories(Categories categories)
		{
			return Search(new SimpleCondition(ProductsView._CategoryID, categories.CategoryID));
		}
		
		public List<ProductsView> GetAllWithSuppliers(Suppliers suppliers)
		{
			return Search(new SimpleCondition(ProductsView._SupplierID, suppliers.SupplierID));
		}
		
	}
	#endregion	
}
