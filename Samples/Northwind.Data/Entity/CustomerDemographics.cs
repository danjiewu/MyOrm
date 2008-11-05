using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region CustomerDemographics
	/// <summary>
	/// CustomerDemographics.
	/// </summary>
	[Table("CustomerDemographics")]
	[Serializable]
	public partial class CustomerDemographics 
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
		[Column(IsPrimaryKey = true)]
		public string CustomerTypeID
		{
			get { return customerTypeID; }			
			set { customerTypeID = value; }
		}
		
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
