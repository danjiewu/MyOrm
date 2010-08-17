using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ITerritoriesService
	/// <summary>
	/// Service for entity 'Territories'.
	/// </summary>	
	public partial interface ITerritoriesService : IEntityService<Territories>, IEntityViewService<TerritoriesView>
	{
        /// <summary>
        /// Get all the Territorieses of the Region.
        /// </summary>
        /// <param name="regionID">ID of Region</param>
        /// <returns></returns>
		List<TerritoriesView> GetAllWithRegion(int regionID);
	}
	#endregion	
}
