using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IOrdersDAO
	/// <summary>
	/// Interface of DAO for object 'Orders'.
	/// </summary>	
	public partial interface IOrdersDAO : IObjectDAO<Orders>, IObjectViewDAO<Orders>
	{
		Orders GetOrderOfOrderDetail(OrderDetails orderDetails);
		List<Orders> GetAllWithCustomer(Customers customer);
		List<Orders> GetAllWithEmployee(Employees employee);
		List<Orders> GetAllWithShipper(Shippers shipper);
	}
	#endregion
	
	#region IOrdersViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrdersView'.
	/// </summary>	
	public partial interface IOrdersViewDAO : IObjectViewDAO<OrdersView>
	{
		OrdersView GetOrderOfOrderDetail(OrderDetails orderDetails);
		List<OrdersView> GetAllWithCustomer(Customers customer);
		List<OrdersView> GetAllWithEmployee(Employees employee);
		List<OrdersView> GetAllWithShipper(Shippers shipper);
	}
	#endregion	
}
