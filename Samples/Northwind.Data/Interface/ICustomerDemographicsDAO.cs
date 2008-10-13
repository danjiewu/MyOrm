using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region ICustomerDemographicsDAO
	/// <summary>
	/// Interface of DAO for object 'CustomerDemographics'.
	/// </summary>	
	public interface ICustomerDemographicsDAO : IObjectDAO<CustomerDemographics>, IObjectDAO
	{		
	}
	#endregion
}
