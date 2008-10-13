using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region IEmployeeTerritoriesDAO
	/// <summary>
	/// Interface of DAO for object 'EmployeeTerritories'.
	/// </summary>	
	public interface IEmployeeTerritoriesDAO : IObjectDAO<EmployeeTerritories>, IObjectDAO
	{		
	}
	#endregion
	
	#region IEmployeeTerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public interface IEmployeeTerritoriesViewDAO : IObjectViewDAO<EmployeeTerritoriesView>, IObjectViewDAO
	{
	}
	#endregion	
}
