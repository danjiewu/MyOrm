using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region CustomerDemographicsService
	/// <summary>
	/// Service for entity 'CustomerDemographics'.
	/// </summary>	
	public partial class CustomerDemographicsService : EntityServiceBase<CustomerDemographics, CustomerDemographics>, ICustomerDemographicsService
	{
	}
	#endregion	
}
