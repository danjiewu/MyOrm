using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region OrderDetailsDAO
	/// <summary>
	/// DAO for object 'OrderDetails'.
	/// </summary>	
	public partial class OrderDetailsDAO : ObjectDAO<OrderDetails> { }
	#endregion

	#region OrderDetailsViewDAO
	/// <summary>
	/// DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public partial class OrderDetailsViewDAO : ObjectViewDAO<OrderDetailsView>
	{
        /// <summary>
        /// Get all the OrderDetailses by OrderID.
        /// </summary>
        /// <param name="orderID">OrderID of the OrderDetails</param>
        /// <returns></returns>
        public List<OrderDetailsView> GetAllByOrderID(int orderID)
        {
            return Search(new SimpleCondition(OrderDetailsView.Properties.OrderID, orderID));
        }
        
        /// <summary>
        /// Get all the OrderDetailses by ProductID.
        /// </summary>
        /// <param name="productID">ProductID of the OrderDetails</param>
        /// <returns></returns>
        public List<OrderDetailsView> GetAllByProductID(int productID)
        {
            return Search(new SimpleCondition(OrderDetailsView.Properties.ProductID, productID));
        }
        
        /// <summary>
        /// Get all the OrderDetailses of the Order.
        /// </summary>
        /// <param name="orderID">ID of Order</param>
        /// <returns></returns>
		public List<OrderDetailsView> GetAllWithOrder(int orderID)
		{			
			return Search(new SimpleCondition(OrderDetailsView.Properties.OrderID, orderID));
		}
		
        /// <summary>
        /// Get all the OrderDetailses of the Product.
        /// </summary>
        /// <param name="productID">ID of Product</param>
        /// <returns></returns>
		public List<OrderDetailsView> GetAllWithProduct(int productID)
		{			
			return Search(new SimpleCondition(OrderDetailsView.Properties.ProductID, productID));
		}
		
	}
	#endregion	
}
