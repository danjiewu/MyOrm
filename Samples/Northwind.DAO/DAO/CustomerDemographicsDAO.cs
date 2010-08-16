using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomerDemographicsDAO
	/// <summary>
	/// DAO for object 'CustomerDemographics'.
	/// </summary>	
	public partial class CustomerDemographicsDAO : ObjectDAO<CustomerDemographics> { }
	#endregion

	#region CustomerDemographicsViewDAO
	/// <summary>
	/// DAO for object's view 'CustomerDemographics'.
	/// </summary>	
	public partial class CustomerDemographicsViewDAO : ObjectViewDAO<CustomerDemographics>
	{
	}
	#endregion	
}
