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
	public class OrderDetailsDAO : ObjectDAO<OrderDetails>, IOrderDetailsDAO
	{
		public List<OrderDetails> GetAllWithOrders(Orders orders)
		{
			return Search(new SimpleCondition(OrderDetails._OrderID, orders.OrderID));
		}
		
		public List<OrderDetails> GetAllWithProducts(Products products)
		{
			return Search(new SimpleCondition(OrderDetails._ProductID, products.ProductID));
		}
		
	}
	#endregion
	
	#region OrderDetailsViewDAO
	/// <summary>
	/// DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public class OrderDetailsViewDAO : ObjectViewDAO<OrderDetailsView>, IOrderDetailsViewDAO
	{
		public List<OrderDetailsView> GetAllWithOrders(Orders orders)
		{
			return Search(new SimpleCondition(OrderDetailsView._OrderID, orders.OrderID));
		}
		
		public List<OrderDetailsView> GetAllWithProducts(Products products)
		{
			return Search(new SimpleCondition(OrderDetailsView._ProductID, products.ProductID));
		}
		
	}
	#endregion	
}
