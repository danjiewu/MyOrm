using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Orders
	/// <summary>
	/// Orders.
	/// </summary>
	[Table("Orders")]	
	[Serializable]
	public partial class Orders : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	OrderID = "OrderID";
		    public const string	CustomerID = "CustomerID";
		    public const string	EmployeeID = "EmployeeID";
		    public const string	OrderDate = "OrderDate";
		    public const string	RequiredDate = "RequiredDate";
		    public const string	ShippedDate = "ShippedDate";
		    public const string	ShipVia = "ShipVia";
		    public const string	Freight = "Freight";
		    public const string	ShipName = "ShipName";
		    public const string	ShipAddress = "ShipAddress";
		    public const string	ShipCity = "ShipCity";
		    public const string	ShipRegion = "ShipRegion";
		    public const string	ShipPostalCode = "ShipPostalCode";
		    public const string	ShipCountry = "ShipCountry";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// OrderID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int OrderID { get; set; }	
        
		/// <summary>
		/// CustomerID
		/// </summary>
        [ForeignType(typeof(Customers))]
		[Column(IsIndex = true)]
		public string CustomerID { get; set; }	
        
		/// <summary>
		/// EmployeeID
		/// </summary>
        [ForeignType(typeof(Employees))]
		[Column(IsIndex = true)]
		public int? EmployeeID { get; set; }	
        
		/// <summary>
		/// OrderDate
		/// </summary>
		[Column(IsIndex = true)]
		public DateTime? OrderDate { get; set; }	
        
		/// <summary>
		/// RequiredDate
		/// </summary>
		[Column]
		public DateTime? RequiredDate { get; set; }	
        
		/// <summary>
		/// ShippedDate
		/// </summary>
		[Column(IsIndex = true)]
		public DateTime? ShippedDate { get; set; }	
        
		/// <summary>
		/// ShipVia
		/// </summary>
        [ForeignType(typeof(Shippers))]
		[Column(IsIndex = true)]
		public int? ShipVia { get; set; }	
        
		/// <summary>
		/// Freight
		/// </summary>
		[Column]
		public decimal? Freight { get; set; }	
        
		/// <summary>
		/// ShipName
		/// </summary>
		[Column]
		public string ShipName { get; set; }	
        
		/// <summary>
		/// ShipAddress
		/// </summary>
		[Column]
		public string ShipAddress { get; set; }	
        
		/// <summary>
		/// ShipCity
		/// </summary>
		[Column]
		public string ShipCity { get; set; }	
        
		/// <summary>
		/// ShipRegion
		/// </summary>
		[Column]
		public string ShipRegion { get; set; }	
        
		/// <summary>
		/// ShipPostalCode
		/// </summary>
		[Column(IsIndex = true)]
		public string ShipPostalCode { get; set; }	
        
		/// <summary>
		/// ShipCountry
		/// </summary>
		[Column]
		public string ShipCountry { get; set; }	
        
		#endregion
	}
	#endregion
	
	#region OrdersView
	/// <summary>
	/// OrdersView.
	/// </summary>
	[TableJoin(typeof(Customers), Orders.Properties.CustomerID, AliasName = "Customer")]
	[TableJoin(typeof(Employees), Orders.Properties.EmployeeID, AliasName = "Employee")]
	[TableJoin(typeof(Shippers), Orders.Properties.ShipVia, AliasName = "Shipper")]
	[Serializable]
	public partial class OrdersView : Orders
	{
		#region Constant
        public new static class Properties
        {
		    public const string	OrderID = "OrderID";
		    public const string	CustomerID = "CustomerID";
		    public const string	EmployeeID = "EmployeeID";
		    public const string	OrderDate = "OrderDate";
		    public const string	RequiredDate = "RequiredDate";
		    public const string	ShippedDate = "ShippedDate";
		    public const string	ShipVia = "ShipVia";
		    public const string	Freight = "Freight";
		    public const string	ShipName = "ShipName";
		    public const string	ShipAddress = "ShipAddress";
		    public const string	ShipCity = "ShipCity";
		    public const string	ShipRegion = "ShipRegion";
		    public const string	ShipPostalCode = "ShipPostalCode";
		    public const string	ShipCountry = "ShipCountry";
		    public const string	Customer_CompanyName = "Customer_CompanyName";			
		    public const string	Customer_ContactName = "Customer_ContactName";			
		    public const string	Customer_ContactTitle = "Customer_ContactTitle";			
		    public const string	Customer_Address = "Customer_Address";			
		    public const string	Customer_City = "Customer_City";			
		    public const string	Customer_Region = "Customer_Region";			
		    public const string	Customer_PostalCode = "Customer_PostalCode";			
		    public const string	Customer_Country = "Customer_Country";			
		    public const string	Customer_Phone = "Customer_Phone";			
		    public const string	Customer_Fax = "Customer_Fax";			
		    public const string	Employee_LastName = "Employee_LastName";			
		    public const string	Employee_FirstName = "Employee_FirstName";			
		    public const string	Employee_Title = "Employee_Title";			
		    public const string	Employee_TitleOfCourtesy = "Employee_TitleOfCourtesy";			
		    public const string	Employee_BirthDate = "Employee_BirthDate";			
		    public const string	Employee_HireDate = "Employee_HireDate";			
		    public const string	Employee_Address = "Employee_Address";			
		    public const string	Employee_City = "Employee_City";			
		    public const string	Employee_Region = "Employee_Region";			
		    public const string	Employee_PostalCode = "Employee_PostalCode";			
		    public const string	Employee_Country = "Employee_Country";			
		    public const string	Employee_HomePhone = "Employee_HomePhone";			
		    public const string	Employee_Extension = "Employee_Extension";			
		    public const string	Employee_Photo = "Employee_Photo";			
		    public const string	Employee_Notes = "Employee_Notes";			
		    public const string	Employee_PhotoPath = "Employee_PhotoPath";			
		    public const string	Shipper_CompanyName = "Shipper_CompanyName";			
		    public const string	Shipper_Phone = "Shipper_Phone";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CompanyName of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.CompanyName)]
		public string Customer_CompanyName { get; set; }	
        
		/// <summary>
		/// ContactName of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.ContactName)]
		public string Customer_ContactName { get; set; }	
        
		/// <summary>
		/// ContactTitle of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.ContactTitle)]
		public string Customer_ContactTitle { get; set; }	
        
		/// <summary>
		/// Address of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.Address)]
		public string Customer_Address { get; set; }	
        
		/// <summary>
		/// City of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.City)]
		public string Customer_City { get; set; }	
        
		/// <summary>
		/// Region of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.Region)]
		public string Customer_Region { get; set; }	
        
		/// <summary>
		/// PostalCode of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.PostalCode)]
		public string Customer_PostalCode { get; set; }	
        
		/// <summary>
		/// Country of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.Country)]
		public string Customer_Country { get; set; }	
        
		/// <summary>
		/// Phone of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.Phone)]
		public string Customer_Phone { get; set; }	
        
		/// <summary>
		/// Fax of Customer
		/// </summary>
		[ForeignColumn("Customer", Property = Customers.Properties.Fax)]
		public string Customer_Fax { get; set; }	
        
		/// <summary>
		/// LastName of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.LastName)]
		public string Employee_LastName { get; set; }	
        
		/// <summary>
		/// FirstName of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.FirstName)]
		public string Employee_FirstName { get; set; }	
        
		/// <summary>
		/// Title of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Title)]
		public string Employee_Title { get; set; }	
        
		/// <summary>
		/// TitleOfCourtesy of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.TitleOfCourtesy)]
		public string Employee_TitleOfCourtesy { get; set; }	
        
		/// <summary>
		/// BirthDate of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.BirthDate)]
		public DateTime? Employee_BirthDate { get; set; }	
        
		/// <summary>
		/// HireDate of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.HireDate)]
		public DateTime? Employee_HireDate { get; set; }	
        
		/// <summary>
		/// Address of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Address)]
		public string Employee_Address { get; set; }	
        
		/// <summary>
		/// City of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.City)]
		public string Employee_City { get; set; }	
        
		/// <summary>
		/// Region of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Region)]
		public string Employee_Region { get; set; }	
        
		/// <summary>
		/// PostalCode of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.PostalCode)]
		public string Employee_PostalCode { get; set; }	
        
		/// <summary>
		/// Country of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Country)]
		public string Employee_Country { get; set; }	
        
		/// <summary>
		/// HomePhone of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.HomePhone)]
		public string Employee_HomePhone { get; set; }	
        
		/// <summary>
		/// Extension of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Extension)]
		public string Employee_Extension { get; set; }	
        
		/// <summary>
		/// Photo of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Photo)]
		public byte[] Employee_Photo { get; set; }	
        
		/// <summary>
		/// Notes of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Notes)]
		public string Employee_Notes { get; set; }	
        
		/// <summary>
		/// PhotoPath of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.PhotoPath)]
		public string Employee_PhotoPath { get; set; }	
        
		/// <summary>
		/// CompanyName of Shipper
		/// </summary>
		[ForeignColumn("Shipper", Property = Shippers.Properties.CompanyName)]
		public string Shipper_CompanyName { get; set; }	
        
		/// <summary>
		/// Phone of Shipper
		/// </summary>
		[ForeignColumn("Shipper", Property = Shippers.Properties.Phone)]
		public string Shipper_Phone { get; set; }	
        
		#endregion
	}
	#endregion	
}
