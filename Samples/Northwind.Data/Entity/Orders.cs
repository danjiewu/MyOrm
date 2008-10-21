using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Orders
	/// <summary>
	/// Orders.
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
		[Column(IsPrimaryKey = true)]
		public int OrderID
		{
			get { return orderID; }			
			set { orderID = value; }
		}
		
		[Column]
		public string CustomerID
		{
			get { return customerID; }			
			set { customerID = value; }
		}
		
		[Column]
		public int? EmployeeID
		{
			get { return employeeID; }			
			set { employeeID = value; }
		}
		
		[Column]
		public DateTime? OrderDate
		{
			get { return orderDate; }			
			set { orderDate = value; }
		}
		
		[Column]
		public DateTime? RequiredDate
		{
			get { return requiredDate; }			
			set { requiredDate = value; }
		}
		
		[Column]
		public DateTime? ShippedDate
		{
			get { return shippedDate; }			
			set { shippedDate = value; }
		}
		
		[Column]
		public int? ShipVia
		{
			get { return shipVia; }			
			set { shipVia = value; }
		}
		
		[Column]
		public decimal? Freight
		{
			get { return freight; }			
			set { freight = value; }
		}
		
		[Column]
		public string ShipName
		{
			get { return shipName; }			
			set { shipName = value; }
		}
		
		[Column]
		public string ShipAddress
		{
			get { return shipAddress; }			
			set { shipAddress = value; }
		}
		
		[Column]
		public string ShipCity
		{
			get { return shipCity; }			
			set { shipCity = value; }
		}
		
		[Column]
		public string ShipRegion
		{
			get { return shipRegion; }			
			set { shipRegion = value; }
		}
		
		[Column]
		public string ShipPostalCode
		{
			get { return shipPostalCode; }			
			set { shipPostalCode = value; }
		}
		
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
	[TableJoin(typeof(Customers), Orders._CustomerID, AliasName = OrdersView.Customer)]
	[TableJoin(typeof(Employees), Orders._EmployeeID, AliasName = OrdersView.Employee)]
	[TableJoin(typeof(Shippers), Orders._ShipVia, AliasName = OrdersView.Shipper)]
    [Serializable]
	public class OrdersView : Orders
	{
		#region Constant		
		public const string	_Customer_CompanyName = "Customer_CompanyName";			
		public const string	_Customer_ContactName = "Customer_ContactName";			
		public const string	_Customer_ContactTitle = "Customer_ContactTitle";			
		public const string	_Customer_Address = "Customer_Address";			
		public const string	_Customer_City = "Customer_City";			
		public const string	_Customer_Region = "Customer_Region";			
		public const string	_Customer_PostalCode = "Customer_PostalCode";			
		public const string	_Customer_Country = "Customer_Country";			
		public const string	_Customer_Phone = "Customer_Phone";			
		public const string	_Customer_Fax = "Customer_Fax";			
		public const string	_Employee_LastName = "Employee_LastName";			
		public const string	_Employee_FirstName = "Employee_FirstName";			
		public const string	_Employee_Title = "Employee_Title";			
		public const string	_Employee_TitleOfCourtesy = "Employee_TitleOfCourtesy";			
		public const string	_Employee_BirthDate = "Employee_BirthDate";			
		public const string	_Employee_HireDate = "Employee_HireDate";			
		public const string	_Employee_Address = "Employee_Address";			
		public const string	_Employee_City = "Employee_City";			
		public const string	_Employee_Region = "Employee_Region";			
		public const string	_Employee_PostalCode = "Employee_PostalCode";			
		public const string	_Employee_Country = "Employee_Country";			
		public const string	_Employee_HomePhone = "Employee_HomePhone";			
		public const string	_Employee_Extension = "Employee_Extension";			
		public const string	_Employee_Photo = "Employee_Photo";			
		public const string	_Employee_Notes = "Employee_Notes";			
		public const string	_Employee_PhotoPath = "Employee_PhotoPath";			
		public const string	_Shipper_CompanyName = "Shipper_CompanyName";			
		public const string	_Shipper_Phone = "Shipper_Phone";			
			
		public const string Customer = "Customer";
		public const string Employee = "Employee";
		public const string Shipper = "Shipper";
		#endregion
		
		#region Member Variables		
		private string customer_CompanyName;			
		private string customer_ContactName;			
		private string customer_ContactTitle;			
		private string customer_Address;			
		private string customer_City;			
		private string customer_Region;			
		private string customer_PostalCode;			
		private string customer_Country;			
		private string customer_Phone;			
		private string customer_Fax;			
		private string employee_LastName;			
		private string employee_FirstName;			
		private string employee_Title;			
		private string employee_TitleOfCourtesy;			
		private DateTime? employee_BirthDate;			
		private DateTime? employee_HireDate;			
		private string employee_Address;			
		private string employee_City;			
		private string employee_Region;			
		private string employee_PostalCode;			
		private string employee_Country;			
		private string employee_HomePhone;			
		private string employee_Extension;			
		private byte[] employee_Photo;			
		private string employee_Notes;			
		private string employee_PhotoPath;			
		private string shipper_CompanyName;			
		private string shipper_Phone;			
		#endregion

		#region Public Properties
		[Column("CompanyName", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_CompanyName
		{
			get { return customer_CompanyName; }			
			set { customer_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_ContactName
		{
			get { return customer_ContactName; }			
			set { customer_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_ContactTitle
		{
			get { return customer_ContactTitle; }			
			set { customer_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Address
		{
			get { return customer_Address; }			
			set { customer_Address = value; }
		}
		
		[Column("City", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_City
		{
			get { return customer_City; }			
			set { customer_City = value; }
		}
		
		[Column("Region", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Region
		{
			get { return customer_Region; }			
			set { customer_Region = value; }
		}
		
		[Column("PostalCode", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_PostalCode
		{
			get { return customer_PostalCode; }			
			set { customer_PostalCode = value; }
		}
		
		[Column("Country", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Country
		{
			get { return customer_Country; }			
			set { customer_Country = value; }
		}
		
		[Column("Phone", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Phone
		{
			get { return customer_Phone; }			
			set { customer_Phone = value; }
		}
		
		[Column("Fax", Foreign = OrdersView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Fax
		{
			get { return customer_Fax; }			
			set { customer_Fax = value; }
		}
		
		[Column("LastName", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_LastName
		{
			get { return employee_LastName; }			
			set { employee_LastName = value; }
		}
		
		[Column("FirstName", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_FirstName
		{
			get { return employee_FirstName; }			
			set { employee_FirstName = value; }
		}
		
		[Column("Title", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Title
		{
			get { return employee_Title; }			
			set { employee_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_TitleOfCourtesy
		{
			get { return employee_TitleOfCourtesy; }			
			set { employee_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public DateTime? Employee_BirthDate
		{
			get { return employee_BirthDate; }			
			set { employee_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public DateTime? Employee_HireDate
		{
			get { return employee_HireDate; }			
			set { employee_HireDate = value; }
		}
		
		[Column("Address", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Address
		{
			get { return employee_Address; }			
			set { employee_Address = value; }
		}
		
		[Column("City", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_City
		{
			get { return employee_City; }			
			set { employee_City = value; }
		}
		
		[Column("Region", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Region
		{
			get { return employee_Region; }			
			set { employee_Region = value; }
		}
		
		[Column("PostalCode", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_PostalCode
		{
			get { return employee_PostalCode; }			
			set { employee_PostalCode = value; }
		}
		
		[Column("Country", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Country
		{
			get { return employee_Country; }			
			set { employee_Country = value; }
		}
		
		[Column("HomePhone", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_HomePhone
		{
			get { return employee_HomePhone; }			
			set { employee_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Extension
		{
			get { return employee_Extension; }			
			set { employee_Extension = value; }
		}

        [Column("Photo", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
        public byte[] Employee_Photo
        {
            get { return employee_Photo; }
            set { employee_Photo = value; }
        }
		
		[Column("Notes", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Notes
		{
			get { return employee_Notes; }			
			set { employee_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = OrdersView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_PhotoPath
		{
			get { return employee_PhotoPath; }			
			set { employee_PhotoPath = value; }
		}
		
		[Column("CompanyName", Foreign = OrdersView.Shipper, ColumnMode = ColumnMode.Read)]
		public string Shipper_CompanyName
		{
			get { return shipper_CompanyName; }			
			set { shipper_CompanyName = value; }
		}
		
		[Column("Phone", Foreign = OrdersView.Shipper, ColumnMode = ColumnMode.Read)]
		public string Shipper_Phone
		{
			get { return shipper_Phone; }			
			set { shipper_Phone = value; }
		}
		
		#endregion
	}
	#endregion	
}
