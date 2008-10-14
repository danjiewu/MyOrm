using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Products
	/// <summary>
	/// Products.
	/// </summary>
	[Table("Products")]
	[Serializable]
	public class Products 
	{
		#region Constant		
		public const string	_ProductID = "ProductID";
		public const string	_ProductName = "ProductName";
		public const string	_SupplierID = "SupplierID";
		public const string	_CategoryID = "CategoryID";
		public const string	_QuantityPerUnit = "QuantityPerUnit";
		public const string	_UnitPrice = "UnitPrice";
		public const string	_UnitsInStock = "UnitsInStock";
		public const string	_UnitsOnOrder = "UnitsOnOrder";
		public const string	_ReorderLevel = "ReorderLevel";
		public const string	_Discontinued = "Discontinued";
		#endregion
		
		#region Member Variables		
		private int productID;
		private string productName;
		private int? supplierID;
		private int? categoryID;
		private string quantityPerUnit;
		private decimal? unitPrice;
		private short? unitsInStock;
		private short? unitsOnOrder;
		private short? reorderLevel;
		private bool discontinued;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public int ProductID
		{
			get { return productID; }			
			set { productID = value; }
		}
		
		[Column]
		public string ProductName
		{
			get { return productName; }			
			set { productName = value; }
		}
		
		[Column]
		public int? SupplierID
		{
			get { return supplierID; }			
			set { supplierID = value; }
		}
		
		[Column]
		public int? CategoryID
		{
			get { return categoryID; }			
			set { categoryID = value; }
		}
		
		[Column]
		public string QuantityPerUnit
		{
			get { return quantityPerUnit; }			
			set { quantityPerUnit = value; }
		}
		
		[Column]
		public decimal? UnitPrice
		{
			get { return unitPrice; }			
			set { unitPrice = value; }
		}
		
		[Column]
		public short? UnitsInStock
		{
			get { return unitsInStock; }			
			set { unitsInStock = value; }
		}
		
		[Column]
		public short? UnitsOnOrder
		{
			get { return unitsOnOrder; }			
			set { unitsOnOrder = value; }
		}
		
		[Column]
		public short? ReorderLevel
		{
			get { return reorderLevel; }			
			set { reorderLevel = value; }
		}
		
		[Column]
		public bool Discontinued
		{
			get { return discontinued; }			
			set { discontinued = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region ProductsView
	/// <summary>
	/// ProductsView.
	/// </summary>	
	[TableJoin(typeof(Categories), "CategoryID", AliasName = ProductsView.Categories)]
	[TableJoin(typeof(Suppliers), "SupplierID", AliasName = ProductsView.Suppliers)]
	public class ProductsView : Products
	{
		#region Constant		
		public const string	_Categories_CategoryName = "Categories_CategoryName";			
		public const string	_Categories_Description = "Categories_Description";			
		public const string	_Categories_Picture = "Categories_Picture";			
		public const string	_Suppliers_CompanyName = "Suppliers_CompanyName";			
		public const string	_Suppliers_ContactName = "Suppliers_ContactName";			
		public const string	_Suppliers_ContactTitle = "Suppliers_ContactTitle";			
		public const string	_Suppliers_Address = "Suppliers_Address";			
		public const string	_Suppliers_City = "Suppliers_City";			
		public const string	_Suppliers_Region = "Suppliers_Region";			
		public const string	_Suppliers_PostalCode = "Suppliers_PostalCode";			
		public const string	_Suppliers_Country = "Suppliers_Country";			
		public const string	_Suppliers_Phone = "Suppliers_Phone";			
		public const string	_Suppliers_Fax = "Suppliers_Fax";			
		public const string	_Suppliers_HomePage = "Suppliers_HomePage";			
			
		public const string Categories = "Categories";
		public const string Suppliers = "Suppliers";
		#endregion
		
		#region Member Variables		
		private string categories_CategoryName;			
		private string categories_Description;			
		private byte[] categories_Picture;			
		private string suppliers_CompanyName;			
		private string suppliers_ContactName;			
		private string suppliers_ContactTitle;			
		private string suppliers_Address;			
		private string suppliers_City;			
		private string suppliers_Region;			
		private string suppliers_PostalCode;			
		private string suppliers_Country;			
		private string suppliers_Phone;			
		private string suppliers_Fax;			
		private string suppliers_HomePage;			
		#endregion

		#region Public Properties
		[Column("CategoryName", Foreign = ProductsView.Categories, ColumnMode = ColumnMode.Read)]
		public string Categories_CategoryName
		{
			get { return categories_CategoryName; }			
			set { categories_CategoryName = value; }
		}
		
		[Column("Description", Foreign = ProductsView.Categories, ColumnMode = ColumnMode.Read)]
		public string Categories_Description
		{
			get { return categories_Description; }			
			set { categories_Description = value; }
		}
		
		[Column("Picture", Foreign = ProductsView.Categories, ColumnMode = ColumnMode.Read)]
		public byte[] Categories_Picture
		{
			get { return categories_Picture; }			
			set { categories_Picture = value; }
		}
		
		[Column("CompanyName", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_CompanyName
		{
			get { return suppliers_CompanyName; }			
			set { suppliers_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_ContactName
		{
			get { return suppliers_ContactName; }			
			set { suppliers_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_ContactTitle
		{
			get { return suppliers_ContactTitle; }			
			set { suppliers_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_Address
		{
			get { return suppliers_Address; }			
			set { suppliers_Address = value; }
		}
		
		[Column("City", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_City
		{
			get { return suppliers_City; }			
			set { suppliers_City = value; }
		}
		
		[Column("Region", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_Region
		{
			get { return suppliers_Region; }			
			set { suppliers_Region = value; }
		}
		
		[Column("PostalCode", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_PostalCode
		{
			get { return suppliers_PostalCode; }			
			set { suppliers_PostalCode = value; }
		}
		
		[Column("Country", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_Country
		{
			get { return suppliers_Country; }			
			set { suppliers_Country = value; }
		}
		
		[Column("Phone", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_Phone
		{
			get { return suppliers_Phone; }			
			set { suppliers_Phone = value; }
		}
		
		[Column("Fax", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_Fax
		{
			get { return suppliers_Fax; }			
			set { suppliers_Fax = value; }
		}
		
		[Column("HomePage", Foreign = ProductsView.Suppliers, ColumnMode = ColumnMode.Read)]
		public string Suppliers_HomePage
		{
			get { return suppliers_HomePage; }			
			set { suppliers_HomePage = value; }
		}
		
		#endregion
	}
	#endregion	
}
