using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region Orders
	/// <summary>
	/// Orders object for table 'Orders'.
	/// </summary>
	[Table("Orders")]
	[Serializable]
	public class Orders 
	{
		#region Constant
		
		public const string	_OrderID = "OrderID";
		public const string	_CustomerID = "CustomerID";
		public const string	_EmployeeID = "EmployeeID";
		public const string	_OrderDate = "OrderDate";
		public const string	_RequiredDate = "RequiredDate";
		public const string	_ShippedDate = "ShippedDate";
		public const string	_ShipVia = "ShipVia";
		public const string	_Freight = "Freight";
		public const string	_ShipName = "ShipName";
		public const string	_ShipAddress = "ShipAddress";
		public const string	_ShipCity = "ShipCity";
		public const string	_ShipRegion = "ShipRegion";
		public const string	_ShipPostalCode = "ShipPostalCode";
		public const string	_ShipCountry = "ShipCountry";
		
		#endregion
		
		#region Member Variables
		
		private int orderID;
		private string customerID;
		private int? employeeID;
		private DateTime? orderDate;
		private DateTime? requiredDate;
		private DateTime? shippedDate;
		private int? shipVia;
		private decimal? freight;
		private string shipName;
		private string shipAddress;
		private string shipCity;
		private string shipRegion;
		private string shipPostalCode;
		private string shipCountry;

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
		[Column]
		public string CustomerID
		{
			get { return customerID; }			
			set { customerID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public int? EmployeeID
		{
			get { return employeeID; }			
			set { employeeID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public DateTime? OrderDate
		{
			get { return orderDate; }			
			set { orderDate = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public DateTime? RequiredDate
		{
			get { return requiredDate; }			
			set { requiredDate = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public DateTime? ShippedDate
		{
			get { return shippedDate; }			
			set { shippedDate = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public int? ShipVia
		{
			get { return shipVia; }			
			set { shipVia = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public decimal? Freight
		{
			get { return freight; }			
			set { freight = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipName
		{
			get { return shipName; }			
			set { shipName = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipAddress
		{
			get { return shipAddress; }			
			set { shipAddress = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipCity
		{
			get { return shipCity; }			
			set { shipCity = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipRegion
		{
			get { return shipRegion; }			
			set { shipRegion = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipPostalCode
		{
			get { return shipPostalCode; }			
			set { shipPostalCode = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ShipCountry
		{
			get { return shipCountry; }			
			set { shipCountry = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region OrdersView
	/// <summary>
	/// OrdersView.
	/// </summary>	
	[TableJoin(typeof(Customers), "CustomerID", AliasName = OrdersView.Customers)]
	[TableJoin(typeof(Employees), "EmployeeID", AliasName = OrdersView.Employees)]
	[TableJoin(typeof(Shippers), "ShipVia", AliasName = OrdersView.Shippers)]
	public class OrdersView : Orders
	{
		#region Constant
		
		public const string	_Customers_CompanyName = "Customers_CompanyName";			
		public const string	_Customers_ContactName = "Customers_ContactName";			
		public const string	_Customers_ContactTitle = "Customers_ContactTitle";			
		public const string	_Customers_Address = "Customers_Address";			
		public const string	_Customers_City = "Customers_City";			
		public const string	_Customers_Region = "Customers_Region";			
		public const string	_Customers_PostalCode = "Customers_PostalCode";			
		public const string	_Customers_Country = "Customers_Country";			
		public const string	_Customers_Phone = "Customers_Phone";			
		public const string	_Customers_Fax = "Customers_Fax";			
		public const string	_Employees_LastName = "Employees_LastName";			
		public const string	_Employees_FirstName = "Employees_FirstName";			
		public const string	_Employees_Title = "Employees_Title";			
		public const string	_Employees_TitleOfCourtesy = "Employees_TitleOfCourtesy";			
		public const string	_Employees_BirthDate = "Employees_BirthDate";			
		public const string	_Employees_HireDate = "Employees_HireDate";			
		public const string	_Employees_Address = "Employees_Address";			
		public const string	_Employees_City = "Employees_City";			
		public const string	_Employees_Region = "Employees_Region";			
		public const string	_Employees_PostalCode = "Employees_PostalCode";			
		public const string	_Employees_Country = "Employees_Country";			
		public const string	_Employees_HomePhone = "Employees_HomePhone";			
		public const string	_Employees_Extension = "Employees_Extension";			
		public const string	_Employees_Photo = "Employees_Photo";			
		public const string	_Employees_Notes = "Employees_Notes";			
		public const string	_Employees_PhotoPath = "Employees_PhotoPath";			
		public const string	_Shippers_CompanyName = "Shippers_CompanyName";			
		public const string	_Shippers_Phone = "Shippers_Phone";			
			
		private const string Customers = "Customers";
		private const string Employees = "Employees";
		private const string Shippers = "Shippers";
		
		#endregion
		
		#region Member Variables
		
		private string customers_CompanyName;			
		private string customers_ContactName;			
		private string customers_ContactTitle;			
		private string customers_Address;			
		private string customers_City;			
		private string customers_Region;			
		private string customers_PostalCode;			
		private string customers_Country;			
		private string customers_Phone;			
		private string customers_Fax;			
		private string employees_LastName;			
		private string employees_FirstName;			
		private string employees_Title;			
		private string employees_TitleOfCourtesy;			
		private DateTime? employees_BirthDate;			
		private DateTime? employees_HireDate;			
		private string employees_Address;			
		private string employees_City;			
		private string employees_Region;			
		private string employees_PostalCode;			
		private string employees_Country;			
		private string employees_HomePhone;			
		private string employees_Extension;			
		private byte[] employees_Photo;			
		private string employees_Notes;			
		private string employees_PhotoPath;			
		private string shippers_CompanyName;			
		private string shippers_Phone;			
		
		#endregion

		#region Public Properties

		[Column("CompanyName", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_CompanyName
		{
			get { return customers_CompanyName; }			
			set { customers_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_ContactName
		{
			get { return customers_ContactName; }			
			set { customers_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_ContactTitle
		{
			get { return customers_ContactTitle; }			
			set { customers_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Address
		{
			get { return customers_Address; }			
			set { customers_Address = value; }
		}
		
		[Column("City", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_City
		{
			get { return customers_City; }			
			set { customers_City = value; }
		}
		
		[Column("Region", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Region
		{
			get { return customers_Region; }			
			set { customers_Region = value; }
		}
		
		[Column("PostalCode", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_PostalCode
		{
			get { return customers_PostalCode; }			
			set { customers_PostalCode = value; }
		}
		
		[Column("Country", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Country
		{
			get { return customers_Country; }			
			set { customers_Country = value; }
		}
		
		[Column("Phone", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Phone
		{
			get { return customers_Phone; }			
			set { customers_Phone = value; }
		}
		
		[Column("Fax", Foreign = OrdersView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Fax
		{
			get { return customers_Fax; }			
			set { customers_Fax = value; }
		}
		
		[Column("LastName", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_LastName
		{
			get { return employees_LastName; }			
			set { employees_LastName = value; }
		}
		
		[Column("FirstName", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_FirstName
		{
			get { return employees_FirstName; }			
			set { employees_FirstName = value; }
		}
		
		[Column("Title", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Title
		{
			get { return employees_Title; }			
			set { employees_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_TitleOfCourtesy
		{
			get { return employees_TitleOfCourtesy; }			
			set { employees_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public DateTime? Employees_BirthDate
		{
			get { return employees_BirthDate; }			
			set { employees_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public DateTime? Employees_HireDate
		{
			get { return employees_HireDate; }			
			set { employees_HireDate = value; }
		}
		
		[Column("Address", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Address
		{
			get { return employees_Address; }			
			set { employees_Address = value; }
		}
		
		[Column("City", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_City
		{
			get { return employees_City; }			
			set { employees_City = value; }
		}
		
		[Column("Region", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Region
		{
			get { return employees_Region; }			
			set { employees_Region = value; }
		}
		
		[Column("PostalCode", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_PostalCode
		{
			get { return employees_PostalCode; }			
			set { employees_PostalCode = value; }
		}
		
		[Column("Country", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Country
		{
			get { return employees_Country; }			
			set { employees_Country = value; }
		}
		
		[Column("HomePhone", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_HomePhone
		{
			get { return employees_HomePhone; }			
			set { employees_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Extension
		{
			get { return employees_Extension; }			
			set { employees_Extension = value; }
		}
		
		[Column("Photo", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public byte[] Employees_Photo
		{
			get { return employees_Photo; }			
			set { employees_Photo = value; }
		}
		
		[Column("Notes", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Notes
		{
			get { return employees_Notes; }			
			set { employees_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = OrdersView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_PhotoPath
		{
			get { return employees_PhotoPath; }			
			set { employees_PhotoPath = value; }
		}
		
		[Column("CompanyName", Foreign = OrdersView.Shippers, ColumnMode = ColumnMode.Read)]
		public string Shippers_CompanyName
		{
			get { return shippers_CompanyName; }			
			set { shippers_CompanyName = value; }
		}
		
		[Column("Phone", Foreign = OrdersView.Shippers, ColumnMode = ColumnMode.Read)]
		public string Shippers_Phone
		{
			get { return shippers_Phone; }			
			set { shippers_Phone = value; }
		}
		
		#endregion
	}
	#endregion	
}
