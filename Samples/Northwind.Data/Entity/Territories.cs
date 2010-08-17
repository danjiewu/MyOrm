using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Territories
	/// <summary>
	/// Territories.
	/// </summary>
	[Table("Territories")]	
	[Serializable]
	public partial class Territories : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	TerritoryID = "TerritoryID";
		    public const string	TerritoryDescription = "TerritoryDescription";
		    public const string	RegionID = "RegionID";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// TerritoryID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public string TerritoryID { get; set; }	
        
		/// <summary>
		/// TerritoryDescription
		/// </summary>
		[Column]
		public string TerritoryDescription { get; set; }	
        
		/// <summary>
		/// RegionID
		/// </summary>
        [ForeignType(typeof(Region))]
		[Column]
		public int RegionID { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.TerritoryID: return TerritoryID;
					case Properties.TerritoryDescription: return TerritoryDescription;
					case Properties.RegionID: return RegionID;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.TerritoryID: TerritoryID = (string)value; break;
					case Properties.TerritoryDescription: TerritoryDescription = (string)value; break;
					case Properties.RegionID: RegionID = (int)value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
	
	#region TerritoriesView
	/// <summary>
	/// TerritoriesView.
	/// </summary>
	[TableJoin(typeof(Region), Territories.Properties.RegionID, AliasName = "Region")]
	[Serializable]
	public partial class TerritoriesView : Territories
	{
		#region Constant
        public new static class Properties
        {
		    public const string	TerritoryID = "TerritoryID";
		    public const string	TerritoryDescription = "TerritoryDescription";
		    public const string	RegionID = "RegionID";
		    public const string	Region_RegionDescription = "Region_RegionDescription";			
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// RegionDescription of Region
		/// </summary>
		[ForeignColumn("Region", Property = Region.Properties.RegionDescription)]
		public string Region_RegionDescription { get; set; }	
        
		#endregion
	}
	#endregion	
}
