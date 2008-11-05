using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region OrdersDAO
	/// <summary>
	/// DAO for object 'Orders'.
	/// </summary>	
	public partial class OrdersDAO : ObjectDAO<Orders>, IOrdersDAO
	{
		public Orders GetOrderOfOrderDetail(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.OrderID);
		}
		
		public List<Orders> GetAllWithCustomer(Customers customer)
		{
			return Search(new SimpleCondition(Orders._CustomerID, customer.CustomerID));
		}
		
		public List<Orders> GetAllWithEmployee(Employees employee)
		{
			return Search(new SimpleCondition(Orders._EmployeeID, employee.EmployeeID));
		}
		
		public List<Orders> GetAllWithShipper(Shippers shipper)
		{
			return Search(new SimpleCondition(Orders._ShipVia, shipper.ShipperID));
		}
		
	}
	#endregion
	
	#region OrdersViewDAO
	/// <summary>
	/// DAO for object's view 'OrdersView'.
	/// </summary>	
	public partial class OrdersViewDAO : ObjectViewDAO<OrdersView>, IOrdersViewDAO
	{
		public OrdersView GetOrderOfOrderDetail(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.OrderID);
		}
		
		public List<OrdersView> GetAllWithCustomer(Customers customer)
		{
			return Search(new SimpleCondition(OrdersView._CustomerID, customer.CustomerID));
		}
		
		public List<OrdersView> GetAllWithEmployee(Employees employee)
		{
			return Search(new SimpleCondition(OrdersView._EmployeeID, employee.EmployeeID));
		}
		
		public List<OrdersView> GetAllWithShipper(Shippers shipper)
		{
			return Search(new SimpleCondition(OrdersView._ShipVia, shipper.ShipperID));
		}
		
	}
	#endregion	
}
