using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region OrderDetails
	/// <summary>
	/// OrderDetails.
	/// </summary>
	[Table("Order Details")]	
	[Serializable]
	public partial class OrderDetails : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	OrderID = "OrderID";
		    public const string	ProductID = "ProductID";
		    public const string	UnitPrice = "UnitPrice";
		    public const string	Quantity = "Quantity";
		    public const string	Discount = "Discount";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// OrderID
		/// </summary>
        [ForeignType(typeof(Orders))]
		[Column(IsPrimaryKey = true)]
		public int OrderID { get; set; }	
        
		/// <summary>
		/// ProductID
		/// </summary>
        [ForeignType(typeof(Products))]
		[Column(IsPrimaryKey = true)]
		public int ProductID { get; set; }	
        
		/// <summary>
		/// UnitPrice
		/// </summary>
		[Column]
		public decimal UnitPrice { get; set; }	
        
		/// <summary>
		/// Quantity
		/// </summary>
		[Column]
		public short Quantity { get; set; }	
        
		/// <summary>
		/// Discount
		/// </summary>
		[Column]
		public float Discount { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.OrderID: return OrderID;
					case Properties.ProductID: return ProductID;
					case Properties.UnitPrice: return UnitPrice;
					case Properties.Quantity: return Quantity;
					case Properties.Discount: return Discount;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.OrderID: OrderID = (int)value; break;
					case Properties.ProductID: ProductID = (int)value; break;
					case Properties.UnitPrice: UnitPrice = (decimal)value; break;
					case Properties.Quantity: Quantity = (short)value; break;
					case Properties.Discount: Discount = (float)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
	
	#region OrderDetailsView
	/// <summary>
	/// OrderDetailsView.
	/// </summary>
	[TableJoin(typeof(Orders), OrderDetails.Properties.OrderID, AliasName = "Order")]
	[TableJoin(typeof(Products), OrderDetails.Properties.ProductID, AliasName = "Product")]
	[Serializable]
	public partial class OrderDetailsView : OrderDetails
	{
		#region Constant
        public new static class Properties
        {
		    public const string	OrderID = "OrderID";
		    public const string	ProductID = "ProductID";
		    public const string	UnitPrice = "UnitPrice";
		    public const string	Quantity = "Quantity";
		    public const string	Discount = "Discount";
		    public const string	Order_OrderDate = "Order_OrderDate";			
		    public const string	Order_RequiredDate = "Order_RequiredDate";			
		    public const string	Order_ShippedDate = "Order_ShippedDate";			
		    public const string	Order_Freight = "Order_Freight";			
		    public const string	Order_ShipName = "Order_ShipName";			
		    public const string	Order_ShipAddress = "Order_ShipAddress";			
		    public const string	Order_ShipCity = "Order_ShipCity";			
		    public const string	Order_ShipRegion = "Order_ShipRegion";			
		    public const string	Order_ShipPostalCode = "Order_ShipPostalCode";			
		    public const string	Order_ShipCountry = "Order_ShipCountry";			
		    public const string	Product_ProductName = "Product_ProductName";			
		    public const string	Product_QuantityPerUnit = "Product_QuantityPerUnit";			
		    public const string	Product_UnitPrice = "Product_UnitPrice";			
		    public const string	Product_UnitsInStock = "Product_UnitsInStock";			
		    public const string	Product_UnitsOnOrder = "Product_UnitsOnOrder";			
		    public const string	Product_ReorderLevel = "Product_ReorderLevel";			
		    public const string	Product_Discontinued = "Product_Discontinued";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// OrderDate of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.OrderDate)]
		public DateTime? Order_OrderDate { get; set; }	
        
		/// <summary>
		/// RequiredDate of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.RequiredDate)]
		public DateTime? Order_RequiredDate { get; set; }	
        
		/// <summary>
		/// ShippedDate of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShippedDate)]
		public DateTime? Order_ShippedDate { get; set; }	
        
		/// <summary>
		/// Freight of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.Freight)]
		public decimal? Order_Freight { get; set; }	
        
		/// <summary>
		/// ShipName of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipName)]
		public string Order_ShipName { get; set; }	
        
		/// <summary>
		/// ShipAddress of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipAddress)]
		public string Order_ShipAddress { get; set; }	
        
		/// <summary>
		/// ShipCity of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipCity)]
		public string Order_ShipCity { get; set; }	
        
		/// <summary>
		/// ShipRegion of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipRegion)]
		public string Order_ShipRegion { get; set; }	
        
		/// <summary>
		/// ShipPostalCode of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipPostalCode)]
		public string Order_ShipPostalCode { get; set; }	
        
		/// <summary>
		/// ShipCountry of Order
		/// </summary>
		[ForeignColumn("Order", Property = Orders.Properties.ShipCountry)]
		public string Order_ShipCountry { get; set; }	
        
		/// <summary>
		/// ProductName of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.ProductName)]
		public string Product_ProductName { get; set; }	
        
		/// <summary>
		/// QuantityPerUnit of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.QuantityPerUnit)]
		public string Product_QuantityPerUnit { get; set; }	
        
		/// <summary>
		/// UnitPrice of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.UnitPrice)]
		public decimal? Product_UnitPrice { get; set; }	
        
		/// <summary>
		/// UnitsInStock of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.UnitsInStock)]
		public short? Product_UnitsInStock { get; set; }	
        
		/// <summary>
		/// UnitsOnOrder of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.UnitsOnOrder)]
		public short? Product_UnitsOnOrder { get; set; }	
        
		/// <summary>
		/// ReorderLevel of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.ReorderLevel)]
		public short? Product_ReorderLevel { get; set; }	
        
		/// <summary>
		/// Discontinued of Product
		/// </summary>
		[ForeignColumn("Product", Property = Products.Properties.Discontinued)]
		public bool? Product_Discontinued { get; set; }	
        
		#endregion
	}
	#endregion	
}
