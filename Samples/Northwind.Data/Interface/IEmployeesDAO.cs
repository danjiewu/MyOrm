using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IEmployeesDAO
	/// <summary>
	/// Interface of DAO for object 'Employees'.
	/// </summary>	
	public interface IEmployeesDAO : IObjectDAO<Employees>, IObjectDAO
	{		
	}
	#endregion
	
	#region IEmployeesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeesView'.
	/// </summary>	
	public interface IEmployeesViewDAO : IObjectViewDAO<EmployeesView>, IObjectViewDAO
	{
	}
	#endregion	
}
