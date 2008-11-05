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
	public partial class OrderDetailsDAO : ObjectDAO<OrderDetails>, IOrderDetailsDAO
	{
		public List<OrderDetails> GetAllWithOrder(Orders order)
		{
			return Search(new SimpleCondition(OrderDetails._OrderID, order.OrderID));
		}
		
		public List<OrderDetails> GetAllWithProduct(Products product)
		{
			return Search(new SimpleCondition(OrderDetails._ProductID, product.ProductID));
		}
		
	}
	#endregion
	
	#region OrderDetailsViewDAO
	/// <summary>
	/// DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public partial class OrderDetailsViewDAO : ObjectViewDAO<OrderDetailsView>, IOrderDetailsViewDAO
	{
		public List<OrderDetailsView> GetAllWithOrder(Orders order)
		{
			return Search(new SimpleCondition(OrderDetailsView._OrderID, order.OrderID));
		}
		
		public List<OrderDetailsView> GetAllWithProduct(Products product)
		{
			return Search(new SimpleCondition(OrderDetailsView._ProductID, product.ProductID));
		}
		
	}
	#endregion	
}
