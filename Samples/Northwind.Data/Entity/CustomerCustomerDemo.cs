using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region CustomerCustomerDemo
	/// <summary>
	/// CustomerCustomerDemo.
	/// </summary>
	[Table("CustomerCustomerDemo")]	
	[Serializable]
	public partial class CustomerCustomerDemo : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	CustomerID = "CustomerID";
		    public const string	CustomerTypeID = "CustomerTypeID";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CustomerID
		/// </summary>
        [ForeignType(typeof(Customers))]
		[Column(IsPrimaryKey = true)]
		public string CustomerID { get; set; }	
        
		/// <summary>
		/// CustomerTypeID
		/// </summary>
        [ForeignType(typeof(CustomerDemographics))]
		[Column(IsPrimaryKey = true)]
		public string CustomerTypeID { get; set; }	
        
		#endregion
	}
	#endregion
	
	#region CustomerCustomerDemoView
	/// <summary>
	/// CustomerCustomerDemoView.
	/// </summary>
	[TableJoin(typeof(CustomerDemographics), CustomerCustomerDemo.Properties.CustomerTypeID, AliasName = "CustomerDemographic")]
	[TableJoin(typeof(Customers), CustomerCustomerDemo.Properties.CustomerID, AliasName = "Customer")]
	[Serializable]
	public partial class CustomerCustomerDemoView : CustomerCustomerDemo
	{
		#region Constant
        public new static class Properties
        {
		    public const string	CustomerID = "CustomerID";
		    public const string	CustomerTypeID = "CustomerTypeID";
		    public const string	CustomerDemographic_CustomerDesc = "CustomerDemographic_CustomerDesc";			
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
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CustomerDesc of CustomerDemographic
		/// </summary>
		[ForeignColumn("CustomerDemographic", Property = CustomerDemographics.Properties.CustomerDesc)]
		public string CustomerDemographic_CustomerDesc { get; set; }	
        
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
        
		#endregion
	}
	#endregion	
}
