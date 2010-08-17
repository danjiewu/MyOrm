using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region CustomerDemographics
	/// <summary>
	/// CustomerDemographics.
	/// </summary>
	[Table("CustomerDemographics")]	
	[Serializable]
	public partial class CustomerDemographics : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	CustomerTypeID = "CustomerTypeID";
		    public const string	CustomerDesc = "CustomerDesc";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CustomerTypeID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public string CustomerTypeID { get; set; }	
        
		/// <summary>
		/// CustomerDesc
		/// </summary>
		[Column]
		public string CustomerDesc { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.CustomerTypeID: return CustomerTypeID;
					case Properties.CustomerDesc: return CustomerDesc;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.CustomerTypeID: CustomerTypeID = (string)value; break;
					case Properties.CustomerDesc: CustomerDesc = (string)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
}
