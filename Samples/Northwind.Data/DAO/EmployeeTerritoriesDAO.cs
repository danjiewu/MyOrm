using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region EmployeeTerritoriesDAO
	/// <summary>
	/// DAO for object 'EmployeeTerritories'.
	/// </summary>	
	public class EmployeeTerritoriesDAO : ObjectDAO<EmployeeTerritories>, IEmployeeTerritoriesDAO
	{
	}
	#endregion
	
	#region EmployeeTerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public class EmployeeTerritoriesViewDAO : ObjectViewDAO<EmployeeTerritoriesView>, IEmployeeTerritoriesViewDAO
	{
	}
	#endregion	
}
