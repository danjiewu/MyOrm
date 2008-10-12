using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Employees
	/// <summary>
	/// Employees object for table 'Employees'.
	/// </summary>
	[Table("Employees")]
	[Serializable]
	public class Employees 
	{
		#region Constant
		
		public const string	_EmployeeID = "EmployeeID";
		public const string	_LastName = "LastName";
		public const string	_FirstName = "FirstName";
		public const string	_Title = "Title";
		public const string	_TitleOfCourtesy = "TitleOfCourtesy";
		public const string	_BirthDate = "BirthDate";
		public const string	_HireDate = "HireDate";
		public const string	_Address = "Address";
		public const string	_City = "City";
		public const string	_Region = "Region";
		public const string	_PostalCode = "PostalCode";
		public const string	_Country = "Country";
		public const string	_HomePhone = "HomePhone";
		public const string	_Extension = "Extension";
		public const string	_Photo = "Photo";
		public const string	_Notes = "Notes";
		public const string	_ReportsTo = "ReportsTo";
		public const string	_PhotoPath = "PhotoPath";
		
		#endregion
		
		#region Member Variables
		
		private int employeeID;
		private string lastName;
		private string firstName;
		private string title;
		private string titleOfCourtesy;
		private DateTime? birthDate;
		private DateTime? hireDate;
		private string address;
		private string city;
		private string region;
		private string postalCode;
		private string country;
		private string homePhone;
		private string extension;
		private byte[] photo;
		private string notes;
		private int? reportsTo;
		private string photoPath;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public int EmployeeID
		{
			get { return employeeID; }			
			set { employeeID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string LastName
		{
			get { return lastName; }			
			set { lastName = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string FirstName
		{
			get { return firstName; }			
			set { firstName = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Title
		{
			get { return title; }			
			set { title = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string TitleOfCourtesy
		{
			get { return titleOfCourtesy; }			
			set { titleOfCourtesy = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public DateTime? BirthDate
		{
			get { return birthDate; }			
			set { birthDate = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public DateTime? HireDate
		{
			get { return hireDate; }			
			set { hireDate = value; }
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
		public string HomePhone
		{
			get { return homePhone; }			
			set { homePhone = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Extension
		{
			get { return extension; }			
			set { extension = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public byte[] Photo
		{
			get { return photo; }			
			set { photo = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string Notes
		{
			get { return notes; }			
			set { notes = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public int? ReportsTo
		{
			get { return reportsTo; }			
			set { reportsTo = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string PhotoPath
		{
			get { return photoPath; }			
			set { photoPath = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region EmployeesView
	/// <summary>
	/// EmployeesView.
	/// </summary>	
	[TableJoin(typeof(Employees), "ReportsTo", AliasName = EmployeesView.ReportsToEmployees)]
	public class EmployeesView : Employees
	{
		#region Constant
		
		public const string	_ReportsToEmployees_LastName = "ReportsToEmployees_LastName";			
		public const string	_ReportsToEmployees_FirstName = "ReportsToEmployees_FirstName";			
		public const string	_ReportsToEmployees_Title = "ReportsToEmployees_Title";			
		public const string	_ReportsToEmployees_TitleOfCourtesy = "ReportsToEmployees_TitleOfCourtesy";			
		public const string	_ReportsToEmployees_BirthDate = "ReportsToEmployees_BirthDate";			
		public const string	_ReportsToEmployees_HireDate = "ReportsToEmployees_HireDate";			
		public const string	_ReportsToEmployees_Address = "ReportsToEmployees_Address";			
		public const string	_ReportsToEmployees_City = "ReportsToEmployees_City";			
		public const string	_ReportsToEmployees_Region = "ReportsToEmployees_Region";			
		public const string	_ReportsToEmployees_PostalCode = "ReportsToEmployees_PostalCode";			
		public const string	_ReportsToEmployees_Country = "ReportsToEmployees_Country";			
		public const string	_ReportsToEmployees_HomePhone = "ReportsToEmployees_HomePhone";			
		public const string	_ReportsToEmployees_Extension = "ReportsToEmployees_Extension";			
		public const string	_ReportsToEmployees_Photo = "ReportsToEmployees_Photo";			
		public const string	_ReportsToEmployees_Notes = "ReportsToEmployees_Notes";			
		public const string	_ReportsToEmployees_PhotoPath = "ReportsToEmployees_PhotoPath";			
			
		private const string ReportsToEmployees = "ReportsToEmployees";
		
		#endregion
		
		#region Member Variables
		
		private string reportsToEmployees_LastName;			
		private string reportsToEmployees_FirstName;			
		private string reportsToEmployees_Title;			
		private string reportsToEmployees_TitleOfCourtesy;			
		private DateTime? reportsToEmployees_BirthDate;			
		private DateTime? reportsToEmployees_HireDate;			
		private string reportsToEmployees_Address;			
		private string reportsToEmployees_City;			
		private string reportsToEmployees_Region;			
		private string reportsToEmployees_PostalCode;			
		private string reportsToEmployees_Country;			
		private string reportsToEmployees_HomePhone;			
		private string reportsToEmployees_Extension;			
		private byte[] reportsToEmployees_Photo;			
		private string reportsToEmployees_Notes;			
		private string reportsToEmployees_PhotoPath;			
		
		#endregion

		#region Public Properties

		[Column("LastName", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_LastName
		{
			get { return reportsToEmployees_LastName; }			
			set { reportsToEmployees_LastName = value; }
		}
		
		[Column("FirstName", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_FirstName
		{
			get { return reportsToEmployees_FirstName; }			
			set { reportsToEmployees_FirstName = value; }
		}
		
		[Column("Title", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Title
		{
			get { return reportsToEmployees_Title; }			
			set { reportsToEmployees_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_TitleOfCourtesy
		{
			get { return reportsToEmployees_TitleOfCourtesy; }			
			set { reportsToEmployees_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public DateTime? ReportsToEmployees_BirthDate
		{
			get { return reportsToEmployees_BirthDate; }			
			set { reportsToEmployees_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public DateTime? ReportsToEmployees_HireDate
		{
			get { return reportsToEmployees_HireDate; }			
			set { reportsToEmployees_HireDate = value; }
		}
		
		[Column("Address", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Address
		{
			get { return reportsToEmployees_Address; }			
			set { reportsToEmployees_Address = value; }
		}
		
		[Column("City", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_City
		{
			get { return reportsToEmployees_City; }			
			set { reportsToEmployees_City = value; }
		}
		
		[Column("Region", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Region
		{
			get { return reportsToEmployees_Region; }			
			set { reportsToEmployees_Region = value; }
		}
		
		[Column("PostalCode", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_PostalCode
		{
			get { return reportsToEmployees_PostalCode; }			
			set { reportsToEmployees_PostalCode = value; }
		}
		
		[Column("Country", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Country
		{
			get { return reportsToEmployees_Country; }			
			set { reportsToEmployees_Country = value; }
		}
		
		[Column("HomePhone", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_HomePhone
		{
			get { return reportsToEmployees_HomePhone; }			
			set { reportsToEmployees_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Extension
		{
			get { return reportsToEmployees_Extension; }			
			set { reportsToEmployees_Extension = value; }
		}
		
		[Column("Photo", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public byte[] ReportsToEmployees_Photo
		{
			get { return reportsToEmployees_Photo; }			
			set { reportsToEmployees_Photo = value; }
		}
		
		[Column("Notes", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_Notes
		{
			get { return reportsToEmployees_Notes; }			
			set { reportsToEmployees_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = EmployeesView.ReportsToEmployees, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployees_PhotoPath
		{
			get { return reportsToEmployees_PhotoPath; }			
			set { reportsToEmployees_PhotoPath = value; }
		}
		
		#endregion
	}
	#endregion	
}
