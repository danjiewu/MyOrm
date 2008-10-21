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
	[TableJoin(typeof(Categories), Products._CategoryID, AliasName = ProductsView.Category)]
	[TableJoin(typeof(Suppliers), Products._SupplierID, AliasName = ProductsView.Supplier)]
    [Serializable]
	public class ProductsView : Products
	{
		#region Constant		
		public const string	_Category_CategoryName = "Category_CategoryName";			
		public const string	_Category_Description = "Category_Description";			
		public const string	_Category_Picture = "Category_Picture";			
		public const string	_Supplier_CompanyName = "Supplier_CompanyName";			
		public const string	_Supplier_ContactName = "Supplier_ContactName";			
		public const string	_Supplier_ContactTitle = "Supplier_ContactTitle";			
		public const string	_Supplier_Address = "Supplier_Address";			
		public const string	_Supplier_City = "Supplier_City";			
		public const string	_Supplier_Region = "Supplier_Region";			
		public const string	_Supplier_PostalCode = "Supplier_PostalCode";			
		public const string	_Supplier_Country = "Supplier_Country";			
		public const string	_Supplier_Phone = "Supplier_Phone";			
		public const string	_Supplier_Fax = "Supplier_Fax";			
		public const string	_Supplier_HomePage = "Supplier_HomePage";			
			
		public const string Category = "Category";
		public const string Supplier = "Supplier";
		#endregion
		
		#region Member Variables		
		private string category_CategoryName;			
		private string category_Description;			
		private byte[] category_Picture;			
		private string supplier_CompanyName;			
		private string supplier_ContactName;			
		private string supplier_ContactTitle;			
		private string supplier_Address;			
		private string supplier_City;			
		private string supplier_Region;			
		private string supplier_PostalCode;			
		private string supplier_Country;			
		private string supplier_Phone;			
		private string supplier_Fax;			
		private string supplier_HomePage;			
		#endregion

		#region Public Properties
		[Column("CategoryName", Foreign = ProductsView.Category, ColumnMode = ColumnMode.Read)]
		public string Category_CategoryName
		{
			get { return category_CategoryName; }			
			set { category_CategoryName = value; }
		}
		
		[Column("Description", Foreign = ProductsView.Category, ColumnMode = ColumnMode.Read)]
		public string Category_Description
		{
			get { return category_Description; }			
			set { category_Description = value; }
		}

        [Column("Picture", Foreign = ProductsView.Category, ColumnMode = ColumnMode.Read)]
        public byte[] Category_Picture
        {
            get { return category_Picture; }
            set { category_Picture = value; }
        }
		
		[Column("CompanyName", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_CompanyName
		{
			get { return supplier_CompanyName; }			
			set { supplier_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_ContactName
		{
			get { return supplier_ContactName; }			
			set { supplier_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_ContactTitle
		{
			get { return supplier_ContactTitle; }			
			set { supplier_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_Address
		{
			get { return supplier_Address; }			
			set { supplier_Address = value; }
		}
		
		[Column("City", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_City
		{
			get { return supplier_City; }			
			set { supplier_City = value; }
		}
		
		[Column("Region", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_Region
		{
			get { return supplier_Region; }			
			set { supplier_Region = value; }
		}
		
		[Column("PostalCode", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_PostalCode
		{
			get { return supplier_PostalCode; }			
			set { supplier_PostalCode = value; }
		}
		
		[Column("Country", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_Country
		{
			get { return supplier_Country; }			
			set { supplier_Country = value; }
		}
		
		[Column("Phone", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_Phone
		{
			get { return supplier_Phone; }			
			set { supplier_Phone = value; }
		}
		
		[Column("Fax", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_Fax
		{
			get { return supplier_Fax; }			
			set { supplier_Fax = value; }
		}
		
		[Column("HomePage", Foreign = ProductsView.Supplier, ColumnMode = ColumnMode.Read)]
		public string Supplier_HomePage
		{
			get { return supplier_HomePage; }			
			set { supplier_HomePage = value; }
		}
		
		#endregion
	}
	#endregion	
}
