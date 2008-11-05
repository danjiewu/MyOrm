using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Employees
	/// <summary>
	/// Employees.
	/// </summary>
	[Table("Employees")]
	[TableJoin(typeof(Employees), Employees._ReportsTo, AliasName = Employees.ReportsToEmployee)]
	[Serializable]
	public partial class Employees 
	{
		public const string ReportsToEmployee = "ReportsToEmployee";
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
		[Column(IsPrimaryKey = true)]
		public int EmployeeID
		{
			get { return employeeID; }			
			set { employeeID = value; }
		}
		
		[Column]
		public string LastName
		{
			get { return lastName; }			
			set { lastName = value; }
		}
		
		[Column]
		public string FirstName
		{
			get { return firstName; }			
			set { firstName = value; }
		}
		
		[Column]
		public string Title
		{
			get { return title; }			
			set { title = value; }
		}
		
		[Column]
		public string TitleOfCourtesy
		{
			get { return titleOfCourtesy; }			
			set { titleOfCourtesy = value; }
		}
		
		[Column]
		public DateTime? BirthDate
		{
			get { return birthDate; }			
			set { birthDate = value; }
		}
		
		[Column]
		public DateTime? HireDate
		{
			get { return hireDate; }			
			set { hireDate = value; }
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
		public string HomePhone
		{
			get { return homePhone; }			
			set { homePhone = value; }
		}
		
		[Column]
		public string Extension
		{
			get { return extension; }			
			set { extension = value; }
		}
		
		[Column]
		public byte[] Photo
		{
			get { return photo; }			
			set { photo = value; }
		}
		
		[Column]
		public string Notes
		{
			get { return notes; }			
			set { notes = value; }
		}
		
		[Column]
		public int? ReportsTo
		{
			get { return reportsTo; }			
			set { reportsTo = value; }
		}
		
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
	[Serializable]
	public partial class EmployeesView : Employees
	{
		#region Constant
		public const string	_ReportsToEmployee_LastName = "ReportsToEmployee_LastName";			
		public const string	_ReportsToEmployee_FirstName = "ReportsToEmployee_FirstName";			
		public const string	_ReportsToEmployee_Title = "ReportsToEmployee_Title";			
		public const string	_ReportsToEmployee_TitleOfCourtesy = "ReportsToEmployee_TitleOfCourtesy";			
		public const string	_ReportsToEmployee_BirthDate = "ReportsToEmployee_BirthDate";			
		public const string	_ReportsToEmployee_HireDate = "ReportsToEmployee_HireDate";			
		public const string	_ReportsToEmployee_Address = "ReportsToEmployee_Address";			
		public const string	_ReportsToEmployee_City = "ReportsToEmployee_City";			
		public const string	_ReportsToEmployee_Region = "ReportsToEmployee_Region";			
		public const string	_ReportsToEmployee_PostalCode = "ReportsToEmployee_PostalCode";			
		public const string	_ReportsToEmployee_Country = "ReportsToEmployee_Country";			
		public const string	_ReportsToEmployee_HomePhone = "ReportsToEmployee_HomePhone";			
		public const string	_ReportsToEmployee_Extension = "ReportsToEmployee_Extension";			
		public const string	_ReportsToEmployee_Photo = "ReportsToEmployee_Photo";			
		public const string	_ReportsToEmployee_Notes = "ReportsToEmployee_Notes";			
		public const string	_ReportsToEmployee_PhotoPath = "ReportsToEmployee_PhotoPath";			
		#endregion
		
		#region Member Variables		
		private string reportsToEmployee_LastName;			
		private string reportsToEmployee_FirstName;			
		private string reportsToEmployee_Title;			
		private string reportsToEmployee_TitleOfCourtesy;			
		private DateTime? reportsToEmployee_BirthDate;			
		private DateTime? reportsToEmployee_HireDate;			
		private string reportsToEmployee_Address;			
		private string reportsToEmployee_City;			
		private string reportsToEmployee_Region;			
		private string reportsToEmployee_PostalCode;			
		private string reportsToEmployee_Country;			
		private string reportsToEmployee_HomePhone;			
		private string reportsToEmployee_Extension;			
		private byte[] reportsToEmployee_Photo;			
		private string reportsToEmployee_Notes;			
		private string reportsToEmployee_PhotoPath;			
		#endregion

		#region Public Properties
		[Column("LastName", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_LastName
		{
			get { return reportsToEmployee_LastName; }			
			set { reportsToEmployee_LastName = value; }
		}
		
		[Column("FirstName", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_FirstName
		{
			get { return reportsToEmployee_FirstName; }			
			set { reportsToEmployee_FirstName = value; }
		}
		
		[Column("Title", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Title
		{
			get { return reportsToEmployee_Title; }			
			set { reportsToEmployee_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_TitleOfCourtesy
		{
			get { return reportsToEmployee_TitleOfCourtesy; }			
			set { reportsToEmployee_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public DateTime? ReportsToEmployee_BirthDate
		{
			get { return reportsToEmployee_BirthDate; }			
			set { reportsToEmployee_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public DateTime? ReportsToEmployee_HireDate
		{
			get { return reportsToEmployee_HireDate; }			
			set { reportsToEmployee_HireDate = value; }
		}
		
		[Column("Address", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Address
		{
			get { return reportsToEmployee_Address; }			
			set { reportsToEmployee_Address = value; }
		}
		
		[Column("City", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_City
		{
			get { return reportsToEmployee_City; }			
			set { reportsToEmployee_City = value; }
		}
		
		[Column("Region", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Region
		{
			get { return reportsToEmployee_Region; }			
			set { reportsToEmployee_Region = value; }
		}
		
		[Column("PostalCode", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_PostalCode
		{
			get { return reportsToEmployee_PostalCode; }			
			set { reportsToEmployee_PostalCode = value; }
		}
		
		[Column("Country", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Country
		{
			get { return reportsToEmployee_Country; }			
			set { reportsToEmployee_Country = value; }
		}
		
		[Column("HomePhone", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_HomePhone
		{
			get { return reportsToEmployee_HomePhone; }			
			set { reportsToEmployee_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Extension
		{
			get { return reportsToEmployee_Extension; }			
			set { reportsToEmployee_Extension = value; }
		}
		
		[Column("Photo", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public byte[] ReportsToEmployee_Photo
		{
			get { return reportsToEmployee_Photo; }			
			set { reportsToEmployee_Photo = value; }
		}
		
		[Column("Notes", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_Notes
		{
			get { return reportsToEmployee_Notes; }			
			set { reportsToEmployee_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = EmployeesView.ReportsToEmployee, ColumnMode = ColumnMode.Read)]
		public string ReportsToEmployee_PhotoPath
		{
			get { return reportsToEmployee_PhotoPath; }			
			set { reportsToEmployee_PhotoPath = value; }
		}
		
		#endregion
	}
	#endregion	
}
