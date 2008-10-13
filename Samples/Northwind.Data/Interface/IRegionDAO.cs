using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IRegionDAO
	/// <summary>
	/// Interface of DAO for object 'Region'.
	/// </summary>	
	public interface IRegionDAO : IObjectDAO<Region>, IObjectDAO
	{		
	}
	#endregion
}
