using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Customers
	/// <summary>
	/// Customers.
	/// </summary>
	[Table("Customers")]	
	[Serializable]
	public partial class Customers : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	CustomerID = "CustomerID";
		    public const string	CompanyName = "CompanyName";
		    public const string	ContactName = "ContactName";
		    public const string	ContactTitle = "ContactTitle";
		    public const string	Address = "Address";
		    public const string	City = "City";
		    public const string	Region = "Region";
		    public const string	PostalCode = "PostalCode";
		    public const string	Country = "Country";
		    public const string	Phone = "Phone";
		    public const string	Fax = "Fax";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CustomerID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public string CustomerID { get; set; }	
        
		/// <summary>
		/// CompanyName
		/// </summary>
		[Column(IsIndex = true)]
		public string CompanyName { get; set; }	
        
		/// <summary>
		/// ContactName
		/// </summary>
		[Column]
		public string ContactName { get; set; }	
        
		/// <summary>
		/// ContactTitle
		/// </summary>
		[Column]
		public string ContactTitle { get; set; }	
        
		/// <summary>
		/// Address
		/// </summary>
		[Column]
		public string Address { get; set; }	
        
		/// <summary>
		/// City
		/// </summary>
		[Column(IsIndex = true)]
		public string City { get; set; }	
        
		/// <summary>
		/// Region
		/// </summary>
		[Column(IsIndex = true)]
		public string Region { get; set; }	
        
		/// <summary>
		/// PostalCode
		/// </summary>
		[Column(IsIndex = true)]
		public string PostalCode { get; set; }	
        
		/// <summary>
		/// Country
		/// </summary>
		[Column]
		public string Country { get; set; }	
        
		/// <summary>
		/// Phone
		/// </summary>
		[Column]
		public string Phone { get; set; }	
        
		/// <summary>
		/// Fax
		/// </summary>
		[Column]
		public string Fax { get; set; }	
        
		#endregion
	}
	#endregion
}
