using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region CustomerCustomerDemo
	/// <summary>
	/// CustomerCustomerDemo.
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
		[Column(IsPrimaryKey = true)]
		public string CustomerID
		{
			get { return customerID; }			
			set { customerID = value; }
		}
		
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
	[TableJoin(typeof(CustomerDemographics), "CustomerTypeID", AliasName = CustomerCustomerDemoView.CustomerDemographic)]
	[TableJoin(typeof(Customers), "CustomerID", AliasName = CustomerCustomerDemoView.Customer)]
	public class CustomerCustomerDemoView : CustomerCustomerDemo
	{
		#region Constant		
		public const string	_CustomerDemographic_CustomerDesc = "CustomerDemographic_CustomerDesc";			
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
			
		public const string CustomerDemographic = "CustomerDemographic";
		public const string Customer = "Customer";
		#endregion
		
		#region Member Variables		
		private string customerDemographic_CustomerDesc;			
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
		#endregion

		#region Public Properties
		[Column("CustomerDesc", Foreign = CustomerCustomerDemoView.CustomerDemographic, ColumnMode = ColumnMode.Read)]
		public string CustomerDemographic_CustomerDesc
		{
			get { return customerDemographic_CustomerDesc; }			
			set { customerDemographic_CustomerDesc = value; }
		}
		
		[Column("CompanyName", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_CompanyName
		{
			get { return customer_CompanyName; }			
			set { customer_CompanyName = value; }
		}
		
		[Column("ContactName", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_ContactName
		{
			get { return customer_ContactName; }			
			set { customer_ContactName = value; }
		}
		
		[Column("ContactTitle", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_ContactTitle
		{
			get { return customer_ContactTitle; }			
			set { customer_ContactTitle = value; }
		}
		
		[Column("Address", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Address
		{
			get { return customer_Address; }			
			set { customer_Address = value; }
		}
		
		[Column("City", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_City
		{
			get { return customer_City; }			
			set { customer_City = value; }
		}
		
		[Column("Region", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Region
		{
			get { return customer_Region; }			
			set { customer_Region = value; }
		}
		
		[Column("PostalCode", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_PostalCode
		{
			get { return customer_PostalCode; }			
			set { customer_PostalCode = value; }
		}
		
		[Column("Country", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Country
		{
			get { return customer_Country; }			
			set { customer_Country = value; }
		}
		
		[Column("Phone", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Phone
		{
			get { return customer_Phone; }			
			set { customer_Phone = value; }
		}
		
		[Column("Fax", Foreign = CustomerCustomerDemoView.Customer, ColumnMode = ColumnMode.Read)]
		public string Customer_Fax
		{
			get { return customer_Fax; }			
			set { customer_Fax = value; }
		}
		
		#endregion
	}
	#endregion	
}
