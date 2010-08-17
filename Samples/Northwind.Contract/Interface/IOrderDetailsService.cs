using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IOrderDetailsService
	/// <summary>
	/// Service for entity 'OrderDetails'.
	/// </summary>	
	public partial interface IOrderDetailsService : IEntityService<OrderDetails>, IEntityViewService<OrderDetailsView>
	{
        /// <summary>
        /// Get all the OrderDetailses by OrderID.
        /// </summary>
        /// <param name="orderID">OrderID of the OrderDetails</param>
        /// <returns></returns>
        List<OrderDetailsView> GetAllByOrderID(int orderID);
        /// <summary>
        /// Get all the OrderDetailses by ProductID.
        /// </summary>
        /// <param name="productID">ProductID of the OrderDetails</param>
        /// <returns></returns>
        List<OrderDetailsView> GetAllByProductID(int productID);
        /// <summary>
        /// Get all the OrderDetailses of the Order.
        /// </summary>
        /// <param name="orderID">ID of Order</param>
        /// <returns></returns>
		List<OrderDetailsView> GetAllWithOrder(int orderID);
        /// <summary>
        /// Get all the OrderDetailses of the Product.
        /// </summary>
        /// <param name="productID">ID of Product</param>
        /// <returns></returns>
		List<OrderDetailsView> GetAllWithProduct(int productID);
	}
	#endregion	
}
