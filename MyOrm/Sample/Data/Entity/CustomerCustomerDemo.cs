using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region CustomerCustomerDemo
	/// <summary>
	/// CustomerCustomerDemo object for table 'CustomerCustomerDemo'.
	/// </summary>
	[Table("CustomerCustomerDemo")]
	[Serializable]
	public class CustomerCustomerDemo 
	{
		#region Constant
		
		public const string	_CustomerID = "CustomerID";
		public const string	_CustomerTypeID = "CustomerTypeID";
		
		#endregion
		
		#region Member Variables
		
		private string customerID;
		private string customerTypeID;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public string CustomerID
		{
			get { return customerID; }			
			set { customerID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public string CustomerTypeID
		{
			get { return customerTypeID; }			
			set { customerTypeID = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region CustomerCustomerDemoView
	/// <summary>
	/// CustomerCustomerDemoView.
	/// </summary>	
	[TableJoin(typeof(CustomerDemographics), "CustomerTypeID", AliasName = CustomerCustomerDemoView.CustomerDemographics)]
	[TableJoin(typeof(Customers), "CustomerID", AliasName = CustomerCustomerDemoView.Customers)]
	public class CustomerCustomerDemoView : CustomerCustomerDemo
	{
		#region Constant
		
		public const string	_CustomerDemographics_CustomerDesc = "CustomerDemographics_CustomerDesc";			
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
			
		private const string CustomerDemographics = "CustomerDemographics";
		private const string Customers = "Customers";
		
		#endregion
		
		#region Member Variables
		
		private string customerDemographics_CustomerDesc;			
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
		
		#endregion

		#region Public Properties

		[Column("CustomerDesc", Foreign = CustomerCustomerDemoView.CustomerDemographics, ColumnMode = ColumnMode.Read)]
		public string CustomerDemographics_CustomerDesc
		{
			get { return customerDemographics_CustomerDesc; }			
			set { customerDemographics_CustomerDesc = value; }
		}
		
		[Column("CompanyName", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_CompanyName
		{
			get { return customers_CompanyName; }			
			set { customers_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_ContactName
		{
			get { return customers_ContactName; }			
			set { customers_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_ContactTitle
		{
			get { return customers_ContactTitle; }			
			set { customers_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Address
		{
			get { return customers_Address; }			
			set { customers_Address = value; }
		}
		
		[Column("City", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_City
		{
			get { return customers_City; }			
			set { customers_City = value; }
		}
		
		[Column("Region", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Region
		{
			get { return customers_Region; }			
			set { customers_Region = value; }
		}
		
		[Column("PostalCode", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_PostalCode
		{
			get { return customers_PostalCode; }			
			set { customers_PostalCode = value; }
		}
		
		[Column("Country", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Country
		{
			get { return customers_Country; }			
			set { customers_Country = value; }
		}
		
		[Column("Phone", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Phone
		{
			get { return customers_Phone; }			
			set { customers_Phone = value; }
		}
		
		[Column("Fax", Foreign = CustomerCustomerDemoView.Customers, ColumnMode = ColumnMode.Read)]
		public string Customers_Fax
		{
			get { return customers_Fax; }			
			set { customers_Fax = value; }
		}
		
		#endregion
	}
	#endregion	
}
