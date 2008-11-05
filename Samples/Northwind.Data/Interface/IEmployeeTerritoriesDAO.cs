using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IEmployeeTerritoriesDAO
	/// <summary>
	/// Interface of DAO for object 'EmployeeTerritories'.
	/// </summary>	
	public partial interface IEmployeeTerritoriesDAO : IObjectDAO<EmployeeTerritories>, IObjectViewDAO<EmployeeTerritories>
	{
		List<EmployeeTerritories> GetAllWithEmployee(Employees employee);
		List<EmployeeTerritories> GetAllWithTerritory(Territories territory);
	}
	#endregion
	
	#region IEmployeeTerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public partial interface IEmployeeTerritoriesViewDAO : IObjectViewDAO<EmployeeTerritoriesView>
	{
		List<EmployeeTerritoriesView> GetAllWithEmployee(Employees employee);
		List<EmployeeTerritoriesView> GetAllWithTerritory(Territories territory);
	}
	#endregion	
}
