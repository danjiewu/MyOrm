using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IOrderDetailsDAO
	/// <summary>
	/// Interface of DAO for object 'OrderDetails'.
	/// </summary>	
	public interface IOrderDetailsDAO : IObjectDAO<OrderDetails>, IObjectViewDAO<OrderDetails>
	{
		List<OrderDetails> GetAllWithOrder(Orders order);
		List<OrderDetails> GetAllWithProduct(Products product);
	}
	#endregion
	
	#region IOrderDetailsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public interface IOrderDetailsViewDAO : IObjectViewDAO<OrderDetailsView>
	{
		List<OrderDetailsView> GetAllWithOrder(Orders order);
		List<OrderDetailsView> GetAllWithProduct(Products product);
	}
	#endregion	
}
