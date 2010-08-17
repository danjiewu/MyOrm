using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Products
	/// <summary>
	/// Products.
	/// </summary>
	[Table("Products")]	
	[Serializable]
	public partial class Products : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	ProductID = "ProductID";
		    public const string	ProductName = "ProductName";
		    public const string	SupplierID = "SupplierID";
		    public const string	CategoryID = "CategoryID";
		    public const string	QuantityPerUnit = "QuantityPerUnit";
		    public const string	UnitPrice = "UnitPrice";
		    public const string	UnitsInStock = "UnitsInStock";
		    public const string	UnitsOnOrder = "UnitsOnOrder";
		    public const string	ReorderLevel = "ReorderLevel";
		    public const string	Discontinued = "Discontinued";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// ProductID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int ProductID { get; set; }	
        
		/// <summary>
		/// ProductName
		/// </summary>
		[Column(IsIndex = true)]
		public string ProductName { get; set; }	
        
		/// <summary>
		/// SupplierID
		/// </summary>
        [ForeignType(typeof(Suppliers))]
		[Column(IsIndex = true)]
		public int? SupplierID { get; set; }	
        
		/// <summary>
		/// CategoryID
		/// </summary>
        [ForeignType(typeof(Categories))]
		[Column(IsIndex = true)]
		public int? CategoryID { get; set; }	
        
		/// <summary>
		/// QuantityPerUnit
		/// </summary>
		[Column]
		public string QuantityPerUnit { get; set; }	
        
		/// <summary>
		/// UnitPrice
		/// </summary>
		[Column]
		public decimal? UnitPrice { get; set; }	
        
		/// <summary>
		/// UnitsInStock
		/// </summary>
		[Column]
		public short? UnitsInStock { get; set; }	
        
		/// <summary>
		/// UnitsOnOrder
		/// </summary>
		[Column]
		public short? UnitsOnOrder { get; set; }	
        
		/// <summary>
		/// ReorderLevel
		/// </summary>
		[Column]
		public short? ReorderLevel { get; set; }	
        
		/// <summary>
		/// Discontinued
		/// </summary>
		[Column]
		public bool Discontinued { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.ProductID: return ProductID;
					case Properties.ProductName: return ProductName;
					case Properties.SupplierID: return SupplierID;
					case Properties.CategoryID: return CategoryID;
					case Properties.QuantityPerUnit: return QuantityPerUnit;
					case Properties.UnitPrice: return UnitPrice;
					case Properties.UnitsInStock: return UnitsInStock;
					case Properties.UnitsOnOrder: return UnitsOnOrder;
					case Properties.ReorderLevel: return ReorderLevel;
					case Properties.Discontinued: return Discontinued;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.ProductID: ProductID = (int)value; break;
					case Properties.ProductName: ProductName = (string)value; break;
					case Properties.SupplierID: SupplierID = (int?)value; break;
					case Properties.CategoryID: CategoryID = (int?)value; break;
					case Properties.QuantityPerUnit: QuantityPerUnit = (string)value; break;
					case Properties.UnitPrice: UnitPrice = (decimal?)value; break;
					case Properties.UnitsInStock: UnitsInStock = (short?)value; break;
					case Properties.UnitsOnOrder: UnitsOnOrder = (short?)value; break;
					case Properties.ReorderLevel: ReorderLevel = (short?)value; break;
					case Properties.Discontinued: Discontinued = (bool)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
	
	#region ProductsView
	/// <summary>
	/// ProductsView.
	/// </summary>
	[TableJoin(typeof(Categories), Products.Properties.CategoryID, AliasName = "Category")]
	[TableJoin(typeof(Suppliers), Products.Properties.SupplierID, AliasName = "Supplier")]
	[Serializable]
	public partial class ProductsView : Products
	{
		#region Constant
        public new static class Properties
        {
		    public const string	ProductID = "ProductID";
		    public const string	ProductName = "ProductName";
		    public const string	SupplierID = "SupplierID";
		    public const string	CategoryID = "CategoryID";
		    public const string	QuantityPerUnit = "QuantityPerUnit";
		    public const string	UnitPrice = "UnitPrice";
		    public const string	UnitsInStock = "UnitsInStock";
		    public const string	UnitsOnOrder = "UnitsOnOrder";
		    public const string	ReorderLevel = "ReorderLevel";
		    public const string	Discontinued = "Discontinued";
		    public const string	Category_CategoryName = "Category_CategoryName";			
		    public const string	Category_Description = "Category_Description";			
		    public const string	Category_Picture = "Category_Picture";			
		    public const string	Supplier_CompanyName = "Supplier_CompanyName";			
		    public const string	Supplier_ContactName = "Supplier_ContactName";			
		    public const string	Supplier_ContactTitle = "Supplier_ContactTitle";			
		    public const string	Supplier_Address = "Supplier_Address";			
		    public const string	Supplier_City = "Supplier_City";			
		    public const string	Supplier_Region = "Supplier_Region";			
		    public const string	Supplier_PostalCode = "Supplier_PostalCode";			
		    public const string	Supplier_Country = "Supplier_Country";			
		    public const string	Supplier_Phone = "Supplier_Phone";			
		    public const string	Supplier_Fax = "Supplier_Fax";			
		    public const string	Supplier_HomePage = "Supplier_HomePage";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CategoryName of Category
		/// </summary>
		[ForeignColumn("Category", Property = Categories.Properties.CategoryName)]
		public string Category_CategoryName { get; set; }	
        
		/// <summary>
		/// Description of Category
		/// </summary>
		[ForeignColumn("Category", Property = Categories.Properties.Description)]
		public string Category_Description { get; set; }	
        
		/// <summary>
		/// Picture of Category
		/// </summary>
		[ForeignColumn("Category", Property = Categories.Properties.Picture)]
		public byte[] Category_Picture { get; set; }	
        
		/// <summary>
		/// CompanyName of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.CompanyName)]
		public string Supplier_CompanyName { get; set; }	
        
		/// <summary>
		/// ContactName of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.ContactName)]
		public string Supplier_ContactName { get; set; }	
        
		/// <summary>
		/// ContactTitle of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.ContactTitle)]
		public string Supplier_ContactTitle { get; set; }	
        
		/// <summary>
		/// Address of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.Address)]
		public string Supplier_Address { get; set; }	
        
		/// <summary>
		/// City of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.City)]
		public string Supplier_City { get; set; }	
        
		/// <summary>
		/// Region of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.Region)]
		public string Supplier_Region { get; set; }	
        
		/// <summary>
		/// PostalCode of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.PostalCode)]
		public string Supplier_PostalCode { get; set; }	
        
		/// <summary>
		/// Country of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.Country)]
		public string Supplier_Country { get; set; }	
        
		/// <summary>
		/// Phone of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.Phone)]
		public string Supplier_Phone { get; set; }	
        
		/// <summary>
		/// Fax of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.Fax)]
		public string Supplier_Fax { get; set; }	
        
		/// <summary>
		/// HomePage of Supplier
		/// </summary>
		[ForeignColumn("Supplier", Property = Suppliers.Properties.HomePage)]
		public string Supplier_HomePage { get; set; }	
        
		#endregion
	}
	#endregion	
}
