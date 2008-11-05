using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region EmployeeTerritoriesDAO
	/// <summary>
	/// DAO for object 'EmployeeTerritories'.
	/// </summary>	
	public partial class EmployeeTerritoriesDAO : ObjectDAO<EmployeeTerritories>, IEmployeeTerritoriesDAO
	{
		public List<EmployeeTerritories> GetAllWithEmployee(Employees employee)
		{
			return Search(new SimpleCondition(EmployeeTerritories._EmployeeID, employee.EmployeeID));
		}
		
		public List<EmployeeTerritories> GetAllWithTerritory(Territories territory)
		{
			return Search(new SimpleCondition(EmployeeTerritories._TerritoryID, territory.TerritoryID));
		}
		
	}
	#endregion
	
	#region EmployeeTerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public partial class EmployeeTerritoriesViewDAO : ObjectViewDAO<EmployeeTerritoriesView>, IEmployeeTerritoriesViewDAO
	{
		public List<EmployeeTerritoriesView> GetAllWithEmployee(Employees employee)
		{
			return Search(new SimpleCondition(EmployeeTerritoriesView._EmployeeID, employee.EmployeeID));
		}
		
		public List<EmployeeTerritoriesView> GetAllWithTerritory(Territories territory)
		{
			return Search(new SimpleCondition(EmployeeTerritoriesView._TerritoryID, territory.TerritoryID));
		}
		
	}
	#endregion	
}
