using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region OrderDetails
	/// <summary>
	/// OrderDetails.
	/// </summary>
	[Table("Order Details")]
	[TableJoin(typeof(Orders), OrderDetails._OrderID, AliasName = OrderDetails.Order)]
	[TableJoin(typeof(Products), OrderDetails._ProductID, AliasName = OrderDetails.Product)]
	[Serializable]
	public partial class OrderDetails 
	{
		public const string Order = "Order";
		public const string Product = "Product";
		#region Constant		
		public const string	_OrderID = "OrderID";
		public const string	_ProductID = "ProductID";
		public const string	_UnitPrice = "UnitPrice";
		public const string	_Quantity = "Quantity";
		public const string	_Discount = "Discount";
		#endregion
		
		#region Member Variables		
		private int orderID;
		private int productID;
		private decimal unitPrice;
		private short quantity;
		private float discount;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public int OrderID
		{
			get { return orderID; }			
			set { orderID = value; }
		}
		
		[Column(IsPrimaryKey = true)]
		public int ProductID
		{
			get { return productID; }			
			set { productID = value; }
		}
		
		[Column]
		public decimal UnitPrice
		{
			get { return unitPrice; }			
			set { unitPrice = value; }
		}
		
		[Column]
		public short Quantity
		{
			get { return quantity; }			
			set { quantity = value; }
		}
		
		[Column]
		public float Discount
		{
			get { return discount; }			
			set { discount = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region OrderDetailsView
	/// <summary>
	/// OrderDetailsView.
	/// </summary>		
	[Serializable]
	public partial class OrderDetailsView : OrderDetails
	{
		#region Constant
		public const string	_Order_OrderDate = "Order_OrderDate";			
		public const string	_Order_RequiredDate = "Order_RequiredDate";			
		public const string	_Order_ShippedDate = "Order_ShippedDate";			
		public const string	_Order_Freight = "Order_Freight";			
		public const string	_Order_ShipName = "Order_ShipName";			
		public const string	_Order_ShipAddress = "Order_ShipAddress";			
		public const string	_Order_ShipCity = "Order_ShipCity";			
		public const string	_Order_ShipRegion = "Order_ShipRegion";			
		public const string	_Order_ShipPostalCode = "Order_ShipPostalCode";			
		public const string	_Order_ShipCountry = "Order_ShipCountry";			
		public const string	_Product_ProductName = "Product_ProductName";			
		public const string	_Product_QuantityPerUnit = "Product_QuantityPerUnit";			
		public const string	_Product_UnitPrice = "Product_UnitPrice";			
		public const string	_Product_UnitsInStock = "Product_UnitsInStock";			
		public const string	_Product_UnitsOnOrder = "Product_UnitsOnOrder";			
		public const string	_Product_ReorderLevel = "Product_ReorderLevel";			
		public const string	_Product_Discontinued = "Product_Discontinued";			
		#endregion
		
		#region Member Variables		
		private DateTime? order_OrderDate;			
		private DateTime? order_RequiredDate;			
		private DateTime? order_ShippedDate;			
		private decimal? order_Freight;			
		private string order_ShipName;			
		private string order_ShipAddress;			
		private string order_ShipCity;			
		private string order_ShipRegion;			
		private string order_ShipPostalCode;			
		private string order_ShipCountry;			
		private string product_ProductName;			
		private string product_QuantityPerUnit;			
		private decimal? product_UnitPrice;			
		private short? product_UnitsInStock;			
		private short? product_UnitsOnOrder;			
		private short? product_ReorderLevel;			
		private bool? product_Discontinued;			
		#endregion

		#region Public Properties
		[Column("OrderDate", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public DateTime? Order_OrderDate
		{
			get { return order_OrderDate; }			
			set { order_OrderDate = value; }
		}
		
		[Column("RequiredDate", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public DateTime? Order_RequiredDate
		{
			get { return order_RequiredDate; }			
			set { order_RequiredDate = value; }
		}
		
		[Column("ShippedDate", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public DateTime? Order_ShippedDate
		{
			get { return order_ShippedDate; }			
			set { order_ShippedDate = value; }
		}
		
		[Column("Freight", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public decimal? Order_Freight
		{
			get { return order_Freight; }			
			set { order_Freight = value; }
		}
		
		[Column("ShipName", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipName
		{
			get { return order_ShipName; }			
			set { order_ShipName = value; }
		}
		
		[Column("ShipAddress", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipAddress
		{
			get { return order_ShipAddress; }			
			set { order_ShipAddress = value; }
		}
		
		[Column("ShipCity", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipCity
		{
			get { return order_ShipCity; }			
			set { order_ShipCity = value; }
		}
		
		[Column("ShipRegion", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipRegion
		{
			get { return order_ShipRegion; }			
			set { order_ShipRegion = value; }
		}
		
		[Column("ShipPostalCode", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipPostalCode
		{
			get { return order_ShipPostalCode; }			
			set { order_ShipPostalCode = value; }
		}
		
		[Column("ShipCountry", Foreign = OrderDetailsView.Order, ColumnMode = ColumnMode.Read)]
		public string Order_ShipCountry
		{
			get { return order_ShipCountry; }			
			set { order_ShipCountry = value; }
		}
		
		[Column("ProductName", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public string Product_ProductName
		{
			get { return product_ProductName; }			
			set { product_ProductName = value; }
		}
		
		[Column("QuantityPerUnit", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public string Product_QuantityPerUnit
		{
			get { return product_QuantityPerUnit; }			
			set { product_QuantityPerUnit = value; }
		}
		
		[Column("UnitPrice", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public decimal? Product_UnitPrice
		{
			get { return product_UnitPrice; }			
			set { product_UnitPrice = value; }
		}
		
		[Column("UnitsInStock", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public short? Product_UnitsInStock
		{
			get { return product_UnitsInStock; }			
			set { product_UnitsInStock = value; }
		}
		
		[Column("UnitsOnOrder", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public short? Product_UnitsOnOrder
		{
			get { return product_UnitsOnOrder; }			
			set { product_UnitsOnOrder = value; }
		}
		
		[Column("ReorderLevel", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public short? Product_ReorderLevel
		{
			get { return product_ReorderLevel; }			
			set { product_ReorderLevel = value; }
		}
		
		[Column("Discontinued", Foreign = OrderDetailsView.Product, ColumnMode = ColumnMode.Read)]
		public bool? Product_Discontinued
		{
			get { return product_Discontinued; }			
			set { product_Discontinued = value; }
		}
		
		#endregion
	}
	#endregion	
}
