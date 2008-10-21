using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region EmployeeTerritories
	/// <summary>
	/// EmployeeTerritories.
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
		[Column(IsPrimaryKey = true)]
		public int EmployeeID
		{
			get { return employeeID; }			
			set { employeeID = value; }
		}
		
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
	[TableJoin(typeof(Employees), EmployeeTerritories._EmployeeID, AliasName = EmployeeTerritoriesView.Employee)]
	[TableJoin(typeof(Territories), EmployeeTerritories._TerritoryID, AliasName = EmployeeTerritoriesView.Territory)]
    [Serializable]
	public class EmployeeTerritoriesView : EmployeeTerritories
	{
		#region Constant		
		public const string	_Employee_LastName = "Employee_LastName";			
		public const string	_Employee_FirstName = "Employee_FirstName";			
		public const string	_Employee_Title = "Employee_Title";			
		public const string	_Employee_TitleOfCourtesy = "Employee_TitleOfCourtesy";			
		public const string	_Employee_BirthDate = "Employee_BirthDate";			
		public const string	_Employee_HireDate = "Employee_HireDate";			
		public const string	_Employee_Address = "Employee_Address";			
		public const string	_Employee_City = "Employee_City";			
		public const string	_Employee_Region = "Employee_Region";			
		public const string	_Employee_PostalCode = "Employee_PostalCode";			
		public const string	_Employee_Country = "Employee_Country";			
		public const string	_Employee_HomePhone = "Employee_HomePhone";			
		public const string	_Employee_Extension = "Employee_Extension";			
		public const string	_Employee_Photo = "Employee_Photo";			
		public const string	_Employee_Notes = "Employee_Notes";			
		public const string	_Employee_PhotoPath = "Employee_PhotoPath";			
		public const string	_Territory_TerritoryDescription = "Territory_TerritoryDescription";			
			
		public const string Employee = "Employee";
		public const string Territory = "Territory";
		#endregion
		
		#region Member Variables		
		private string employee_LastName;			
		private string employee_FirstName;			
		private string employee_Title;			
		private string employee_TitleOfCourtesy;			
		private DateTime? employee_BirthDate;			
		private DateTime? employee_HireDate;			
		private string employee_Address;			
		private string employee_City;			
		private string employee_Region;			
		private string employee_PostalCode;			
		private string employee_Country;			
		private string employee_HomePhone;			
		private string employee_Extension;			
		private byte[] employee_Photo;			
		private string employee_Notes;			
		private string employee_PhotoPath;			
		private string territory_TerritoryDescription;			
		#endregion

		#region Public Properties
		[Column("LastName", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_LastName
		{
			get { return employee_LastName; }			
			set { employee_LastName = value; }
		}
		
		[Column("FirstName", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_FirstName
		{
			get { return employee_FirstName; }			
			set { employee_FirstName = value; }
		}
		
		[Column("Title", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Title
		{
			get { return employee_Title; }			
			set { employee_Title = value; }
		}
		
		[Column("TitleOfCourtesy", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_TitleOfCourtesy
		{
			get { return employee_TitleOfCourtesy; }			
			set { employee_TitleOfCourtesy = value; }
		}
		
		[Column("BirthDate", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public DateTime? Employee_BirthDate
		{
			get { return employee_BirthDate; }			
			set { employee_BirthDate = value; }
		}
		
		[Column("HireDate", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public DateTime? Employee_HireDate
		{
			get { return employee_HireDate; }			
			set { employee_HireDate = value; }
		}
		
		[Column("Address", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Address
		{
			get { return employee_Address; }			
			set { employee_Address = value; }
		}
		
		[Column("City", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_City
		{
			get { return employee_City; }			
			set { employee_City = value; }
		}
		
		[Column("Region", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Region
		{
			get { return employee_Region; }			
			set { employee_Region = value; }
		}
		
		[Column("PostalCode", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_PostalCode
		{
			get { return employee_PostalCode; }			
			set { employee_PostalCode = value; }
		}
		
		[Column("Country", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Country
		{
			get { return employee_Country; }			
			set { employee_Country = value; }
		}
		
		[Column("HomePhone", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_HomePhone
		{
			get { return employee_HomePhone; }			
			set { employee_HomePhone = value; }
		}
		
		[Column("Extension", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Extension
		{
			get { return employee_Extension; }			
			set { employee_Extension = value; }
		}

        [Column("Photo", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
        public byte[] Employee_Photo
        {
            get { return employee_Photo; }
            set { employee_Photo = value; }
        }
		
		[Column("Notes", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_Notes
		{
			get { return employee_Notes; }			
			set { employee_Notes = value; }
		}
		
		[Column("PhotoPath", Foreign = EmployeeTerritoriesView.Employee, ColumnMode = ColumnMode.Read)]
		public string Employee_PhotoPath
		{
			get { return employee_PhotoPath; }			
			set { employee_PhotoPath = value; }
		}
		
		[Column("TerritoryDescription", Foreign = EmployeeTerritoriesView.Territory, ColumnMode = ColumnMode.Read)]
		public string Territory_TerritoryDescription
		{
			get { return territory_TerritoryDescription; }			
			set { territory_TerritoryDescription = value; }
		}
		
		#endregion
	}
	#endregion	
}
