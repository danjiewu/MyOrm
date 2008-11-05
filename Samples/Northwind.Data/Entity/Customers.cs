using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Customers
	/// <summary>
	/// Customers.
	/// </summary>
	[Table("Customers")]
	[Serializable]
	public partial class Customers 
	{
		#region Constant		
		public const string	_CustomerID = "CustomerID";
		public const string	_CompanyName = "CompanyName";
		public const string	_ContactName = "ContactName";
		public const string	_ContactTitle = "ContactTitle";
		public const string	_Address = "Address";
		public const string	_City = "City";
		public const string	_Region = "Region";
		public const string	_PostalCode = "PostalCode";
		public const string	_Country = "Country";
		public const string	_Phone = "Phone";
		public const string	_Fax = "Fax";
		#endregion
		
		#region Member Variables		
		private string customerID;
		private string companyName;
		private string contactName;
		private string contactTitle;
		private string address;
		private string city;
		private string region;
		private string postalCode;
		private string country;
		private string phone;
		private string fax;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public string CustomerID
		{
			get { return customerID; }			
			set { customerID = value; }
		}
		
		[Column]
		public string CompanyName
		{
			get { return companyName; }			
			set { companyName = value; }
		}
		
		[Column]
		public string ContactName
		{
			get { return contactName; }			
			set { contactName = value; }
		}
		
		[Column]
		public string ContactTitle
		{
			get { return contactTitle; }			
			set { contactTitle = value; }
		}
		
		[Column]
		public string Address
		{
			get { return address; }			
			set { address = value; }
		}
		
		[Column]
		public string City
		{
			get { return city; }			
			set { city = value; }
		}
		
		[Column]
		public string Region
		{
			get { return region; }			
			set { region = value; }
		}
		
		[Column]
		public string PostalCode
		{
			get { return postalCode; }			
			set { postalCode = value; }
		}
		
		[Column]
		public string Country
		{
			get { return country; }			
			set { country = value; }
		}
		
		[Column]
		public string Phone
		{
			get { return phone; }			
			set { phone = value; }
		}
		
		[Column]
		public string Fax
		{
			get { return fax; }			
			set { fax = value; }
		}
		
		#endregion
	}
	#endregion
}
