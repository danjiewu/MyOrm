using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region TerritoriesService
	/// <summary>
	/// Service for entity 'Territories'.
	/// </summary>	
	public partial class TerritoriesService : EntityServiceBase<Territories, TerritoriesView>, ITerritoriesService
	{
        /// <summary>
        /// Get all the Territorieses of the Region.
        /// </summary>
        /// <param name="regionID">ID of Region</param>
        /// <returns></returns>
		public List<TerritoriesView> GetAllWithRegion(int regionID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(TerritoriesView.Properties.RegionID, regionID));
		}
		
	}
	#endregion	
}
