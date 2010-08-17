using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Region
	/// <summary>
	/// Region.
	/// </summary>
	[Table("Region")]	
	[Serializable]
	public partial class Region : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	RegionID = "RegionID";
		    public const string	RegionDescription = "RegionDescription";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// RegionID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int RegionID { get; set; }	
        
		/// <summary>
		/// RegionDescription
		/// </summary>
		[Column]
		public string RegionDescription { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.RegionID: return RegionID;
					case Properties.RegionDescription: return RegionDescription;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.RegionID: RegionID = (int)value; break;
					case Properties.RegionDescription: RegionDescription = (string)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
}
