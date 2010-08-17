using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ICustomerDemographicsService
	/// <summary>
	/// Service for entity 'CustomerDemographics'.
	/// </summary>	
	public partial interface ICustomerDemographicsService : IEntityService<CustomerDemographics>, IEntityViewService<CustomerDemographics>
	{
	}
	#endregion	
}
