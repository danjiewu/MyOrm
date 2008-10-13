using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region ISuppliersDAO
	/// <summary>
	/// Interface of DAO for object 'Suppliers'.
	/// </summary>	
	public interface ISuppliersDAO : IObjectDAO<Suppliers>, IObjectDAO
	{		
	}
	#endregion
}
