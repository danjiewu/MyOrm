using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region EmployeeTerritories
	/// <summary>
	/// EmployeeTerritories object for table 'EmployeeTerritories'.
	/// </summary>
	[Table("EmployeeTerritories")]
	[Serializable]
	public class EmployeeTerritories 
	{
		#region Constant
		
		public const string	_EmployeeID = "EmployeeID";
		public const string	_TerritoryID = "TerritoryID";
		
		#endregion
		
		#region Member Variables
		
		private int employeeID;
		private string territoryID;

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
		[Column(IsPrimaryKey = true)]
		public string TerritoryID
		{
			get { return territoryID; }			
			set { territoryID = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region EmployeeTerritoriesView
	/// <summary>
	/// EmployeeTerritoriesView.
	/// </summary>	
	[TableJoin(typeof(Employees), "EmployeeID", AliasName = EmployeeTerritoriesView.Employees)]
	[TableJoin(typeof(Territories), "TerritoryID", AliasName = EmployeeTerritoriesView.Territories)]
	public class EmployeeTerritoriesView : EmployeeTerritories
	{
		#region Constant
		
		public const string	_Employees_LastName = "Employees_LastName";			
		public const string	_Employees_FirstName = "Employees_FirstName";			
		public const string	_Employees_Title = "Employees_Title";			
		public const string	_Employees_TitleOfCourtesy = "Employees_TitleOfCourtesy";			
		public const string	_Employees_BirthDate = "Employees_BirthDate";			
		public const string	_Employees_HireDate = "Employees_HireDate";			
		public const string	_Employees_Address = "Employees_Address";			
		public const string	_Employees_City = "Employees_City";			
		public const string	_Employees_Region = "Employees_Region";			
		public const string	_Employees_PostalCode = "Employees_PostalCode";			
		public const string	_Employees_Country = "Employees_Country";			
		public const string	_Employees_HomePhone = "Employees_HomePhone";			
		public const string	_Employees_Extension = "Employees_Extension";			
		public const string	_Employees_Photo = "Employees_Photo";			
		public const string	_Employees_Notes = "Employees_Notes";			
		public const string	_Employees_PhotoPath = "Employees_PhotoPath";			
		public const string	_Territories_TerritoryDescription = "Territories_TerritoryDescription";			
			
		private const string Employees = "Employees";
		private const string Territories = "Territories";
		
		#endregion
		
		#region Member Variables
		
		private string employees_LastName;			
		private string employees_FirstName;			
		private string employees_Title;			
		private string employees_TitleOfCourtesy;			
		private DateTime? employees_BirthDate;			
		private DateTime? employees_HireDate;			
		private string employees_Address;			
		private string employees_City;			
		private string employees_Region;			
		private string employees_PostalCode;			
		private string employees_Country;			
		private string employees_HomePhone;			
		private string employees_Extension;			
		private byte[] employees_Photo;			
		private string employees_Notes;			
		private string employees_PhotoPath;			
		private string territories_TerritoryDescription;			
		
		#endregion

		#region Public Properties

		[Column("LastName", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_LastName
		{
			get { return employees_LastName; }			
			set { employees_LastName = value; }
		}
		
		[Column("FirstName", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_FirstName
		{
			get { return employees_FirstName; }			
			set { employees_FirstName = value; }
		}
		
		[Column("Title", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Title
		{
			get { return employees_Title; }			
			set { employees_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_TitleOfCourtesy
		{
			get { return employees_TitleOfCourtesy; }			
			set { employees_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public DateTime? Employees_BirthDate
		{
			get { return employees_BirthDate; }			
			set { employees_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public DateTime? Employees_HireDate
		{
			get { return employees_HireDate; }			
			set { employees_HireDate = value; }
		}
		
		[Column("Address", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Address
		{
			get { return employees_Address; }			
			set { employees_Address = value; }
		}
		
		[Column("City", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_City
		{
			get { return employees_City; }			
			set { employees_City = value; }
		}
		
		[Column("Region", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Region
		{
			get { return employees_Region; }			
			set { employees_Region = value; }
		}
		
		[Column("PostalCode", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_PostalCode
		{
			get { return employees_PostalCode; }			
			set { employees_PostalCode = value; }
		}
		
		[Column("Country", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Country
		{
			get { return employees_Country; }			
			set { employees_Country = value; }
		}
		
		[Column("HomePhone", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_HomePhone
		{
			get { return employees_HomePhone; }			
			set { employees_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Extension
		{
			get { return employees_Extension; }			
			set { employees_Extension = value; }
		}
		
		[Column("Photo", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public byte[] Employees_Photo
		{
			get { return employees_Photo; }			
			set { employees_Photo = value; }
		}
		
		[Column("Notes", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_Notes
		{
			get { return employees_Notes; }			
			set { employees_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = EmployeeTerritoriesView.Employees, ColumnMode = ColumnMode.Read)]
		public string Employees_PhotoPath
		{
			get { return employees_PhotoPath; }			
			set { employees_PhotoPath = value; }
		}
		
		[Column("TerritoryDescription", Foreign = EmployeeTerritoriesView.Territories, ColumnMode = ColumnMode.Read)]
		public string Territories_TerritoryDescription
		{
			get { return territories_TerritoryDescription; }			
			set { territories_TerritoryDescription = value; }
		}
		
		#endregion
	}
	#endregion	
}
