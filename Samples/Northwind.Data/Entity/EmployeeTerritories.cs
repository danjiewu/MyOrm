using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region EmployeeTerritories
	/// <summary>
	/// EmployeeTerritories.
	/// </summary>
	[Table("EmployeeTerritories")]	
	[Serializable]
	public partial class EmployeeTerritories : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	EmployeeID = "EmployeeID";
		    public const string	TerritoryID = "TerritoryID";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// EmployeeID
		/// </summary>
        [ForeignType(typeof(Employees))]
		[Column(IsPrimaryKey = true)]
		public int EmployeeID { get; set; }	
        
		/// <summary>
		/// TerritoryID
		/// </summary>
        [ForeignType(typeof(Territories))]
		[Column(IsPrimaryKey = true)]
		public string TerritoryID { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.EmployeeID: return EmployeeID;
					case Properties.TerritoryID: return TerritoryID;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.EmployeeID: EmployeeID = (int)value; break;
					case Properties.TerritoryID: TerritoryID = (string)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
	
	#region EmployeeTerritoriesView
	/// <summary>
	/// EmployeeTerritoriesView.
	/// </summary>
	[Serializable]
	public partial class EmployeeTerritoriesView : EmployeeTerritories
	{
		#region Constant
        public new static class Properties
        {
		    public const string	EmployeeID = "EmployeeID";
		    public const string	TerritoryID = "TerritoryID";
		    public const string	Employee_LastName = "Employee_LastName";			
		    public const string	Employee_FirstName = "Employee_FirstName";			
		    public const string	Employee_Title = "Employee_Title";			
		    public const string	Employee_TitleOfCourtesy = "Employee_TitleOfCourtesy";			
		    public const string	Employee_BirthDate = "Employee_BirthDate";			
		    public const string	Employee_HireDate = "Employee_HireDate";			
		    public const string	Employee_Address = "Employee_Address";			
		    public const string	Employee_City = "Employee_City";			
		    public const string	Employee_Region = "Employee_Region";			
		    public const string	Employee_PostalCode = "Employee_PostalCode";			
		    public const string	Employee_Country = "Employee_Country";			
		    public const string	Employee_HomePhone = "Employee_HomePhone";			
		    public const string	Employee_Extension = "Employee_Extension";			
		    public const string	Employee_Photo = "Employee_Photo";			
		    public const string	Employee_Notes = "Employee_Notes";			
		    public const string	Employee_PhotoPath = "Employee_PhotoPath";			
		    public const string	Territory_TerritoryDescription = "Territory_TerritoryDescription";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// LastName of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.LastName)]
		public string Employee_LastName { get; set; }	
        
		/// <summary>
		/// FirstName of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.FirstName)]
		public string Employee_FirstName { get; set; }	
        
		/// <summary>
		/// Title of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Title)]
		public string Employee_Title { get; set; }	
        
		/// <summary>
		/// TitleOfCourtesy of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.TitleOfCourtesy)]
		public string Employee_TitleOfCourtesy { get; set; }	
        
		/// <summary>
		/// BirthDate of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.BirthDate)]
		public DateTime? Employee_BirthDate { get; set; }	
        
		/// <summary>
		/// HireDate of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.HireDate)]
		public DateTime? Employee_HireDate { get; set; }	
        
		/// <summary>
		/// Address of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Address)]
		public string Employee_Address { get; set; }	
        
		/// <summary>
		/// City of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.City)]
		public string Employee_City { get; set; }	
        
		/// <summary>
		/// Region of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Region)]
		public string Employee_Region { get; set; }	
        
		/// <summary>
		/// PostalCode of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.PostalCode)]
		public string Employee_PostalCode { get; set; }	
        
		/// <summary>
		/// Country of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Country)]
		public string Employee_Country { get; set; }	
        
		/// <summary>
		/// HomePhone of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.HomePhone)]
		public string Employee_HomePhone { get; set; }	
        
		/// <summary>
		/// Extension of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Extension)]
		public string Employee_Extension { get; set; }	
        
		/// <summary>
		/// Photo of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Photo)]
		public byte[] Employee_Photo { get; set; }	
        
		/// <summary>
		/// Notes of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.Notes)]
		public string Employee_Notes { get; set; }	
        
		/// <summary>
		/// PhotoPath of Employee
		/// </summary>
		[ForeignColumn("Employee", Property = Employees.Properties.PhotoPath)]
		public string Employee_PhotoPath { get; set; }	
        
		/// <summary>
		/// TerritoryDescription of Territory
		/// </summary>
		[ForeignColumn("Territory", Property = Territories.Properties.TerritoryDescription)]
		public string Territory_TerritoryDescription { get; set; }	
        
		#endregion
	}
	#endregion	
}
