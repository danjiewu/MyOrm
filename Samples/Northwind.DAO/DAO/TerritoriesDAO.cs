using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region TerritoriesDAO
	/// <summary>
	/// DAO for object 'Territories'.
	/// </summary>	
	public partial class TerritoriesDAO : ObjectDAO<Territories> { }
	#endregion

	#region TerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public partial class TerritoriesViewDAO : ObjectViewDAO<TerritoriesView>
	{
        /// <summary>
        /// Get all the Territorieses of the Region.
        /// </summary>
        /// <param name="regionID">ID of Region</param>
        /// <returns></returns>
		public List<TerritoriesView> GetAllWithRegion(int regionID)
		{			
			return Search(new SimpleCondition(TerritoriesView.Properties.RegionID, regionID));
		}
		
	}
	#endregion	
}
