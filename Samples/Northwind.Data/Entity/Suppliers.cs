using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region Suppliers
	/// <summary>
	/// Suppliers object for table 'Suppliers'.
	/// </summary>
	[Table("Suppliers")]
	[Serializable]
	public class Suppliers 
	{
		#region Constant
		
		public const string	_SupplierID = "SupplierID";
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
		public const string	_HomePage = "HomePage";
		
		#endregion
		
		#region Member Variables
		
		private int supplierID;
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
		private string homePage;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public int SupplierID
		{
			get { return supplierID; }			
			set { supplierID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string CompanyName
		{
			get { return companyName; }			
			set { companyName = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ContactName
		{
			get { return contactName; }			
			set { contactName = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string ContactTitle
		{
			get { return contactTitle; }			
			set { contactTitle = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Address
		{
			get { return address; }			
			set { address = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string City
		{
			get { return city; }			
			set { city = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Region
		{
			get { return region; }			
			set { region = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string PostalCode
		{
			get { return postalCode; }			
			set { postalCode = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Country
		{
			get { return country; }			
			set { country = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Phone
		{
			get { return phone; }			
			set { phone = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Fax
		{
			get { return fax; }			
			set { fax = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string HomePage
		{
			get { return homePage; }			
			set { homePage = value; }
		}
		
		#endregion
	}
	#endregion
}
