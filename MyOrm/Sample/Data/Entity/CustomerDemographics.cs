using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region CustomerDemographics
	/// <summary>
	/// CustomerDemographics object for table 'CustomerDemographics'.
	/// </summary>
	[Table("CustomerDemographics")]
	[Serializable]
	public class CustomerDemographics 
	{
		#region Constant
		
		public const string	_CustomerTypeID = "CustomerTypeID";
		public const string	_CustomerDesc = "CustomerDesc";
		
		#endregion
		
		#region Member Variables
		
		private string customerTypeID;
		private string customerDesc;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public string CustomerTypeID
		{
			get { return customerTypeID; }			
			set { customerTypeID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string CustomerDesc
		{
			get { return customerDesc; }			
			set { customerDesc = value; }
		}
		
		#endregion
	}
	#endregion
}
