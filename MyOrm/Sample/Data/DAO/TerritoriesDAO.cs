using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region TerritoriesDAO
	/// <summary>
	/// DAO for object 'Territories'.
	/// </summary>	
	public class TerritoriesDAO : ObjectDAO<Territories>, ITerritoriesDAO
	{
	}
	#endregion
	
	#region TerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public class TerritoriesViewDAO : ObjectViewDAO<TerritoriesView>, ITerritoriesViewDAO
	{
	}
	#endregion	
}
