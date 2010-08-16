using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Suppliers
	/// <summary>
	/// Suppliers.
	/// </summary>
	[Table("Suppliers")]	
	[Serializable]
	public partial class Suppliers : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	SupplierID = "SupplierID";
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
		    public const string	HomePage = "HomePage";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// SupplierID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int SupplierID { get; set; }	
        
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
		[Column]
		public string City { get; set; }	
        
		/// <summary>
		/// Region
		/// </summary>
		[Column]
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
        
		/// <summary>
		/// HomePage
		/// </summary>
		[Column]
		public string HomePage { get; set; }	
        
		#endregion
	}
	#endregion
}
