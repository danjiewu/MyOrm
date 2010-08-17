using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IProductsService
	/// <summary>
	/// Service for entity 'Products'.
	/// </summary>	
	public partial interface IProductsService : IEntityService<Products>, IEntityViewService<ProductsView>
	{
        /// <summary>
        /// Get all the Productses by ProductName.
        /// </summary>
        /// <param name="productName">ProductName of the Products</param>
        /// <returns></returns>
        List<ProductsView> GetAllByProductName(string productName);
        /// <summary>
        /// Get all the Productses by SupplierID.
        /// </summary>
        /// <param name="supplierID">SupplierID of the Products</param>
        /// <returns></returns>
        List<ProductsView> GetAllBySupplierID(int supplierID);
        /// <summary>
        /// Get all the Productses by CategoryID.
        /// </summary>
        /// <param name="categoryID">CategoryID of the Products</param>
        /// <returns></returns>
        List<ProductsView> GetAllByCategoryID(int categoryID);
        /// <summary>
        /// Get all the Productses of the Category.
        /// </summary>
        /// <param name="categoryID">ID of Category</param>
        /// <returns></returns>
		List<ProductsView> GetAllWithCategory(int categoryID);
        /// <summary>
        /// Get all the Productses of the Supplier.
        /// </summary>
        /// <param name="supplierID">ID of Supplier</param>
        /// <returns></returns>
		List<ProductsView> GetAllWithSupplier(int supplierID);
	}
	#endregion	
}
