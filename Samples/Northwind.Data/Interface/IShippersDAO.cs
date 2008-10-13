using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region IShippersDAO
	/// <summary>
	/// Interface of DAO for object 'Shippers'.
	/// </summary>	
	public interface IShippersDAO : IObjectDAO<Shippers>, IObjectDAO
	{		
	}
	#endregion
}
