using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ITerritoriesDAO
	/// <summary>
	/// Interface of DAO for object 'Territories'.
	/// </summary>	
	public interface ITerritoriesDAO : IObjectDAO<Territories>, IObjectDAO
	{		
	}
	#endregion
	
	#region ITerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public interface ITerritoriesViewDAO : IObjectViewDAO<TerritoriesView>, IObjectViewDAO
	{
	}
	#endregion	
}
