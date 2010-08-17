using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Employees
	/// <summary>
	/// Employees.
	/// </summary>
	[Table("Employees")]	
	[Serializable]
	public partial class Employees : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	EmployeeID = "EmployeeID";
		    public const string	LastName = "LastName";
		    public const string	FirstName = "FirstName";
		    public const string	Title = "Title";
		    public const string	TitleOfCourtesy = "TitleOfCourtesy";
		    public const string	BirthDate = "BirthDate";
		    public const string	HireDate = "HireDate";
		    public const string	Address = "Address";
		    public const string	City = "City";
		    public const string	Region = "Region";
		    public const string	PostalCode = "PostalCode";
		    public const string	Country = "Country";
		    public const string	HomePhone = "HomePhone";
		    public const string	Extension = "Extension";
		    public const string	Photo = "Photo";
		    public const string	Notes = "Notes";
		    public const string	ReportsTo = "ReportsTo";
		    public const string	PhotoPath = "PhotoPath";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// EmployeeID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int EmployeeID { get; set; }	
        
		/// <summary>
		/// LastName
		/// </summary>
		[Column(IsIndex = true)]
		public string LastName { get; set; }	
        
		/// <summary>
		/// FirstName
		/// </summary>
		[Column]
		public string FirstName { get; set; }	
        
		/// <summary>
		/// Title
		/// </summary>
		[Column]
		public string Title { get; set; }	
        
		/// <summary>
		/// TitleOfCourtesy
		/// </summary>
		[Column]
		public string TitleOfCourtesy { get; set; }	
        
		/// <summary>
		/// BirthDate
		/// </summary>
		[Column]
		public DateTime? BirthDate { get; set; }	
        
		/// <summary>
		/// HireDate
		/// </summary>
		[Column]
		public DateTime? HireDate { get; set; }	
        
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
		/// HomePhone
		/// </summary>
		[Column]
		public string HomePhone { get; set; }	
        
		/// <summary>
		/// Extension
		/// </summary>
		[Column]
		public string Extension { get; set; }	
        
		/// <summary>
		/// Photo
		/// </summary>
		[Column]
		public byte[] Photo { get; set; }	
        
		/// <summary>
		/// Notes
		/// </summary>
		[Column]
		public string Notes { get; set; }	
        
		/// <summary>
		/// ReportsTo
		/// </summary>
        [ForeignType(typeof(Employees))]
		[Column]
		public int? ReportsTo { get; set; }	
        
		/// <summary>
		/// PhotoPath
		/// </summary>
		[Column]
		public string PhotoPath { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.EmployeeID: return EmployeeID;
					case Properties.LastName: return LastName;
					case Properties.FirstName: return FirstName;
					case Properties.Title: return Title;
					case Properties.TitleOfCourtesy: return TitleOfCourtesy;
					case Properties.BirthDate: return BirthDate;
					case Properties.HireDate: return HireDate;
					case Properties.Address: return Address;
					case Properties.City: return City;
					case Properties.Region: return Region;
					case Properties.PostalCode: return PostalCode;
					case Properties.Country: return Country;
					case Properties.HomePhone: return HomePhone;
					case Properties.Extension: return Extension;
					case Properties.Photo: return Photo;
					case Properties.Notes: return Notes;
					case Properties.ReportsTo: return ReportsTo;
					case Properties.PhotoPath: return PhotoPath;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.EmployeeID: EmployeeID = (int)value; break;
					case Properties.LastName: LastName = (string)value; break;
					case Properties.FirstName: FirstName = (string)value; break;
					case Properties.Title: Title = (string)value; break;
					case Properties.TitleOfCourtesy: TitleOfCourtesy = (string)value; break;
					case Properties.BirthDate: BirthDate = (DateTime?)value; break;
					case Properties.HireDate: HireDate = (DateTime?)value; break;
					case Properties.Address: Address = (string)value; break;
					case Properties.City: City = (string)value; break;
					case Properties.Region: Region = (string)value; break;
					case Properties.PostalCode: PostalCode = (string)value; break;
					case Properties.Country: Country = (string)value; break;
					case Properties.HomePhone: HomePhone = (string)value; break;
					case Properties.Extension: Extension = (string)value; break;
					case Properties.Photo: Photo = (byte[])value; break;
					case Properties.Notes: Notes = (string)value; break;
					case Properties.ReportsTo: ReportsTo = (int?)value; break;
					case Properties.PhotoPath: PhotoPath = (string)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
	
	#region EmployeesView
	/// <summary>
	/// EmployeesView.
	/// </summary>
	[TableJoin(typeof(Employees), Employees.Properties.ReportsTo, AliasName = "ReportsToEmployee")]
	[Serializable]
	public partial class EmployeesView : Employees
	{
		#region Constant
        public new static class Properties
        {
		    public const string	EmployeeID = "EmployeeID";
		    public const string	LastName = "LastName";
		    public const string	FirstName = "FirstName";
		    public const string	Title = "Title";
		    public const string	TitleOfCourtesy = "TitleOfCourtesy";
		    public const string	BirthDate = "BirthDate";
		    public const string	HireDate = "HireDate";
		    public const string	Address = "Address";
		    public const string	City = "City";
		    public const string	Region = "Region";
		    public const string	PostalCode = "PostalCode";
		    public const string	Country = "Country";
		    public const string	HomePhone = "HomePhone";
		    public const string	Extension = "Extension";
		    public const string	Photo = "Photo";
		    public const string	Notes = "Notes";
		    public const string	ReportsTo = "ReportsTo";
		    public const string	PhotoPath = "PhotoPath";
		    public const string	ReportsToEmployee_LastName = "ReportsToEmployee_LastName";			
		    public const string	ReportsToEmployee_FirstName = "ReportsToEmployee_FirstName";			
		    public const string	ReportsToEmployee_Title = "ReportsToEmployee_Title";			
		    public const string	ReportsToEmployee_TitleOfCourtesy = "ReportsToEmployee_TitleOfCourtesy";			
		    public const string	ReportsToEmployee_BirthDate = "ReportsToEmployee_BirthDate";			
		    public const string	ReportsToEmployee_HireDate = "ReportsToEmployee_HireDate";			
		    public const string	ReportsToEmployee_Address = "ReportsToEmployee_Address";			
		    public const string	ReportsToEmployee_City = "ReportsToEmployee_City";			
		    public const string	ReportsToEmployee_Region = "ReportsToEmployee_Region";			
		    public const string	ReportsToEmployee_PostalCode = "ReportsToEmployee_PostalCode";			
		    public const string	ReportsToEmployee_Country = "ReportsToEmployee_Country";			
		    public const string	ReportsToEmployee_HomePhone = "ReportsToEmployee_HomePhone";			
		    public const string	ReportsToEmployee_Extension = "ReportsToEmployee_Extension";			
		    public const string	ReportsToEmployee_Photo = "ReportsToEmployee_Photo";			
		    public const string	ReportsToEmployee_Notes = "ReportsToEmployee_Notes";			
		    public const string	ReportsToEmployee_PhotoPath = "ReportsToEmployee_PhotoPath";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// LastName of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.LastName)]
		public string ReportsToEmployee_LastName { get; set; }	
        
		/// <summary>
		/// FirstName of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.FirstName)]
		public string ReportsToEmployee_FirstName { get; set; }	
        
		/// <summary>
		/// Title of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Title)]
		public string ReportsToEmployee_Title { get; set; }	
        
		/// <summary>
		/// TitleOfCourtesy of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.TitleOfCourtesy)]
		public string ReportsToEmployee_TitleOfCourtesy { get; set; }	
        
		/// <summary>
		/// BirthDate of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.BirthDate)]
		public DateTime? ReportsToEmployee_BirthDate { get; set; }	
        
		/// <summary>
		/// HireDate of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.HireDate)]
		public DateTime? ReportsToEmployee_HireDate { get; set; }	
        
		/// <summary>
		/// Address of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Address)]
		public string ReportsToEmployee_Address { get; set; }	
        
		/// <summary>
		/// City of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.City)]
		public string ReportsToEmployee_City { get; set; }	
        
		/// <summary>
		/// Region of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Region)]
		public string ReportsToEmployee_Region { get; set; }	
        
		/// <summary>
		/// PostalCode of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.PostalCode)]
		public string ReportsToEmployee_PostalCode { get; set; }	
        
		/// <summary>
		/// Country of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Country)]
		public string ReportsToEmployee_Country { get; set; }	
        
		/// <summary>
		/// HomePhone of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.HomePhone)]
		public string ReportsToEmployee_HomePhone { get; set; }	
        
		/// <summary>
		/// Extension of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Extension)]
		public string ReportsToEmployee_Extension { get; set; }	
        
		/// <summary>
		/// Photo of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Photo)]
		public byte[] ReportsToEmployee_Photo { get; set; }	
        
		/// <summary>
		/// Notes of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.Notes)]
		public string ReportsToEmployee_Notes { get; set; }	
        
		/// <summary>
		/// PhotoPath of ReportsToEmployee
		/// </summary>
		[ForeignColumn("ReportsToEmployee", Property = Employees.Properties.PhotoPath)]
		public string ReportsToEmployee_PhotoPath { get; set; }	
        
		#endregion
	}
	#endregion	
}
