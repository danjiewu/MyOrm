using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ProductsService
	/// <summary>
	/// Service for entity 'Products'.
	/// </summary>	
	public partial class ProductsService : EntityServiceBase<Products, ProductsView>, IProductsService
	{
        /// <summary>
        /// Get all the Productses by ProductName.
        /// </summary>
        /// <param name="productName">ProductName of the Products</param>
        /// <returns></returns>
        public List<ProductsView> GetAllByProductName(string productName)
        {
            return ObjectViewDAO.Search(new SimpleCondition(ProductsView.Properties.ProductName, productName));
        }
        
        /// <summary>
        /// Get all the Productses by SupplierID.
        /// </summary>
        /// <param name="supplierID">SupplierID of the Products</param>
        /// <returns></returns>
        public List<ProductsView> GetAllBySupplierID(int supplierID)
        {
            return ObjectViewDAO.Search(new SimpleCondition(ProductsView.Properties.SupplierID, supplierID));
        }
        
        /// <summary>
        /// Get all the Productses by CategoryID.
        /// </summary>
        /// <param name="categoryID">CategoryID of the Products</param>
        /// <returns></returns>
        public List<ProductsView> GetAllByCategoryID(int categoryID)
        {
            return ObjectViewDAO.Search(new SimpleCondition(ProductsView.Properties.CategoryID, categoryID));
        }
        
        /// <summary>
        /// Get all the Productses of the Category.
        /// </summary>
        /// <param name="categoryID">ID of Category</param>
        /// <returns></returns>
		public List<ProductsView> GetAllWithCategory(int categoryID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(ProductsView.Properties.CategoryID, categoryID));
		}
		
        /// <summary>
        /// Get all the Productses of the Supplier.
        /// </summary>
        /// <param name="supplierID">ID of Supplier</param>
        /// <returns></returns>
		public List<ProductsView> GetAllWithSupplier(int supplierID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(ProductsView.Properties.SupplierID, supplierID));
		}
		
	}
	#endregion	
}
