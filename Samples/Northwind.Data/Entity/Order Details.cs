using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region OrderDetails
	/// <summary>
	/// OrderDetails object for table 'Order Details'.
	/// </summary>
	[Table("Order Details")]
	[Serializable]
	public class OrderDetails 
	{
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

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public int OrderID
		{
			get { return orderID; }			
			set { orderID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public int ProductID
		{
			get { return productID; }			
			set { productID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public decimal UnitPrice
		{
			get { return unitPrice; }			
			set { unitPrice = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public short Quantity
		{
			get { return quantity; }			
			set { quantity = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
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
	[TableJoin(typeof(Orders), "OrderID", AliasName = OrderDetailsView.Orders)]
	[TableJoin(typeof(Products), "ProductID", AliasName = OrderDetailsView.Products)]
	public class OrderDetailsView : OrderDetails
	{
		#region Constant
		
		public const string	_Orders_OrderDate = "Orders_OrderDate";			
		public const string	_Orders_RequiredDate = "Orders_RequiredDate";			
		public const string	_Orders_ShippedDate = "Orders_ShippedDate";			
		public const string	_Orders_Freight = "Orders_Freight";			
		public const string	_Orders_ShipName = "Orders_ShipName";			
		public const string	_Orders_ShipAddress = "Orders_ShipAddress";			
		public const string	_Orders_ShipCity = "Orders_ShipCity";			
		public const string	_Orders_ShipRegion = "Orders_ShipRegion";			
		public const string	_Orders_ShipPostalCode = "Orders_ShipPostalCode";			
		public const string	_Orders_ShipCountry = "Orders_ShipCountry";			
		public const string	_Products_ProductName = "Products_ProductName";			
		public const string	_Products_QuantityPerUnit = "Products_QuantityPerUnit";			
		public const string	_Products_UnitPrice = "Products_UnitPrice";			
		public const string	_Products_UnitsInStock = "Products_UnitsInStock";			
		public const string	_Products_UnitsOnOrder = "Products_UnitsOnOrder";			
		public const string	_Products_ReorderLevel = "Products_ReorderLevel";			
		public const string	_Products_Discontinued = "Products_Discontinued";			
			
		private const string Orders = "Orders";
		private const string Products = "Products";
		
		#endregion
		
		#region Member Variables
		
		private DateTime? orders_OrderDate;			
		private DateTime? orders_RequiredDate;			
		private DateTime? orders_ShippedDate;			
		private decimal? orders_Freight;			
		private string orders_ShipName;			
		private string orders_ShipAddress;			
		private string orders_ShipCity;			
		private string orders_ShipRegion;			
		private string orders_ShipPostalCode;			
		private string orders_ShipCountry;			
		private string products_ProductName;			
		private string products_QuantityPerUnit;			
		private decimal? products_UnitPrice;			
		private short? products_UnitsInStock;			
		private short? products_UnitsOnOrder;			
		private short? products_ReorderLevel;			
		private bool products_Discontinued;			
		
		#endregion

		#region Public Properties

		[Column("OrderDate", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public DateTime? Orders_OrderDate
		{
			get { return orders_OrderDate; }			
			set { orders_OrderDate = value; }
		}
		
		[Column("RequiredDate", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public DateTime? Orders_RequiredDate
		{
			get { return orders_RequiredDate; }			
			set { orders_RequiredDate = value; }
		}
		
		[Column("ShippedDate", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public DateTime? Orders_ShippedDate
		{
			get { return orders_ShippedDate; }			
			set { orders_ShippedDate = value; }
		}
		
		[Column("Freight", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public decimal? Orders_Freight
		{
			get { return orders_Freight; }			
			set { orders_Freight = value; }
		}
		
		[Column("ShipName", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipName
		{
			get { return orders_ShipName; }			
			set { orders_ShipName = value; }
		}
		
		[Column("ShipAddress", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipAddress
		{
			get { return orders_ShipAddress; }			
			set { orders_ShipAddress = value; }
		}
		
		[Column("ShipCity", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipCity
		{
			get { return orders_ShipCity; }			
			set { orders_ShipCity = value; }
		}
		
		[Column("ShipRegion", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipRegion
		{
			get { return orders_ShipRegion; }			
			set { orders_ShipRegion = value; }
		}
		
		[Column("ShipPostalCode", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipPostalCode
		{
			get { return orders_ShipPostalCode; }			
			set { orders_ShipPostalCode = value; }
		}
		
		[Column("ShipCountry", Foreign = OrderDetailsView.Orders, ColumnMode = ColumnMode.Read)]
		public string Orders_ShipCountry
		{
			get { return orders_ShipCountry; }			
			set { orders_ShipCountry = value; }
		}
		
		[Column("ProductName", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public string Products_ProductName
		{
			get { return products_ProductName; }			
			set { products_ProductName = value; }
		}
		
		[Column("QuantityPerUnit", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public string Products_QuantityPerUnit
		{
			get { return products_QuantityPerUnit; }			
			set { products_QuantityPerUnit = value; }
		}
		
		[Column("UnitPrice", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public decimal? Products_UnitPrice
		{
			get { return products_UnitPrice; }			
			set { products_UnitPrice = value; }
		}
		
		[Column("UnitsInStock", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public short? Products_UnitsInStock
		{
			get { return products_UnitsInStock; }			
			set { products_UnitsInStock = value; }
		}
		
		[Column("UnitsOnOrder", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public short? Products_UnitsOnOrder
		{
			get { return products_UnitsOnOrder; }			
			set { products_UnitsOnOrder = value; }
		}
		
		[Column("ReorderLevel", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public short? Products_ReorderLevel
		{
			get { return products_ReorderLevel; }			
			set { products_ReorderLevel = value; }
		}
		
		[Column("Discontinued", Foreign = OrderDetailsView.Products, ColumnMode = ColumnMode.Read)]
		public bool Products_Discontinued
		{
			get { return products_Discontinued; }			
			set { products_Discontinued = value; }
		}
		
		#endregion
	}
	#endregion	
}
