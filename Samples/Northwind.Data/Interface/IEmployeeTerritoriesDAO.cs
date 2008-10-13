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
	public interface IEmployeeTerritoriesDAO : IObjectDAO<EmployeeTerritories>, IObjectDAO
	{
		List<EmployeeTerritories> GetAllWithEmployees(Employees employees);
		List<EmployeeTerritories> GetAllWithTerritories(Territories territories);
	}
	#endregion
	
	#region IEmployeeTerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public interface IEmployeeTerritoriesViewDAO : IObjectViewDAO<EmployeeTerritoriesView>, IObjectViewDAO
	{
		List<EmployeeTerritoriesView> GetAllWithEmployees(Employees employees);
		List<EmployeeTerritoriesView> GetAllWithTerritories(Territories territories);
	}
	#endregion	
}
