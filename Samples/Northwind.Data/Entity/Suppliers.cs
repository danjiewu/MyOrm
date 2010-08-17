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
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.SupplierID: return SupplierID;
					case Properties.CompanyName: return CompanyName;
					case Properties.ContactName: return ContactName;
					case Properties.ContactTitle: return ContactTitle;
					case Properties.Address: return Address;
					case Properties.City: return City;
					case Properties.Region: return Region;
					case Properties.PostalCode: return PostalCode;
					case Properties.Country: return Country;
					case Properties.Phone: return Phone;
					case Properties.Fax: return Fax;
					case Properties.HomePage: return HomePage;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.SupplierID: SupplierID = (int)value; break;
					case Properties.CompanyName: CompanyName = (string)value; break;
					case Properties.ContactName: ContactName = (string)value; break;
					case Properties.ContactTitle: ContactTitle = (string)value; break;
					case Properties.Address: Address = (string)value; break;
					case Properties.City: City = (string)value; break;
					case Properties.Region: Region = (string)value; break;
					case Properties.PostalCode: PostalCode = (string)value; break;
					case Properties.Country: Country = (string)value; break;
					case Properties.Phone: Phone = (string)value; break;
					case Properties.Fax: Fax = (string)value; break;
					case Properties.HomePage: HomePage = (string)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
}
