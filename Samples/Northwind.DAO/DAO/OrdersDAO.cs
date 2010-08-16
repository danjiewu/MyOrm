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
	public partial class OrdersDAO : ObjectDAO<Orders> { }
	#endregion

	#region OrdersViewDAO
	/// <summary>
	/// DAO for object's view 'OrdersView'.
	/// </summary>	
	public partial class OrdersViewDAO : ObjectViewDAO<OrdersView>
	{
        /// <summary>
        /// Get all the Orderses by CustomerID.
        /// </summary>
        /// <param name="customerID">CustomerID of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByCustomerID(string customerID)
        {
            return Search(new SimpleCondition(OrdersView.Properties.CustomerID, customerID));
        }
        
        /// <summary>
        /// Get all the Orderses by EmployeeID.
        /// </summary>
        /// <param name="employeeID">EmployeeID of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByEmployeeID(int employeeID)
        {
            return Search(new SimpleCondition(OrdersView.Properties.EmployeeID, employeeID));
        }
        
        /// <summary>
        /// Get all the Orderses by OrderDate.
        /// </summary>
        /// <param name="orderDate">OrderDate of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByOrderDate(DateTime orderDate)
        {
            return Search(new SimpleCondition(OrdersView.Properties.OrderDate, orderDate));
        }
        
        /// <summary>
        /// Get all the Orderses by ShippedDate.
        /// </summary>
        /// <param name="shippedDate">ShippedDate of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByShippedDate(DateTime shippedDate)
        {
            return Search(new SimpleCondition(OrdersView.Properties.ShippedDate, shippedDate));
        }
        
        /// <summary>
        /// Get all the Orderses by ShipVia.
        /// </summary>
        /// <param name="shipVia">ShipVia of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByShipVia(int shipVia)
        {
            return Search(new SimpleCondition(OrdersView.Properties.ShipVia, shipVia));
        }
        
        /// <summary>
        /// Get all the Orderses by ShipPostalCode.
        /// </summary>
        /// <param name="shipPostalCode">ShipPostalCode of the Orders</param>
        /// <returns></returns>
        public List<OrdersView> GetAllByShipPostalCode(string shipPostalCode)
        {
            return Search(new SimpleCondition(OrdersView.Properties.ShipPostalCode, shipPostalCode));
        }
        
        /// <summary>
        /// Get all the Orderses of the Customer.
        /// </summary>
        /// <param name="customerID">ID of Customer</param>
        /// <returns></returns>
		public List<OrdersView> GetAllWithCustomer(string customerID)
		{			
			return Search(new SimpleCondition(OrdersView.Properties.CustomerID, customerID));
		}
		
        /// <summary>
        /// Get all the Orderses of the Employee.
        /// </summary>
        /// <param name="employeeID">ID of Employee</param>
        /// <returns></returns>
		public List<OrdersView> GetAllWithEmployee(int employeeID)
		{			
			return Search(new SimpleCondition(OrdersView.Properties.EmployeeID, employeeID));
		}
		
        /// <summary>
        /// Get all the Orderses of the Shipper.
        /// </summary>
        /// <param name="shipperID">ID of Shipper</param>
        /// <returns></returns>
		public List<OrdersView> GetAllWithShipper(int shipperID)
		{			
			return Search(new SimpleCondition(OrdersView.Properties.ShipVia, shipperID));
		}
		
	}
	#endregion	
}
