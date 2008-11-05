using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Territories
	/// <summary>
	/// Territories.
	/// </summary>
	[Table("Territories")]
	[TableJoin(typeof(Region), Territories._RegionID, AliasName = Territories.Region)]
	[Serializable]
	public partial class Territories 
	{
		public const string Region = "Region";
		#region Constant		
		public const string	_TerritoryID = "TerritoryID";
		public const string	_TerritoryDescription = "TerritoryDescription";
		public const string	_RegionID = "RegionID";
		#endregion
		
		#region Member Variables		
		private string territoryID;
		private string territoryDescription;
		private int regionID;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public string TerritoryID
		{
			get { return territoryID; }			
			set { territoryID = value; }
		}
		
		[Column]
		public string TerritoryDescription
		{
			get { return territoryDescription; }			
			set { territoryDescription = value; }
		}
		
		[Column]
		public int RegionID
		{
			get { return regionID; }			
			set { regionID = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region TerritoriesView
	/// <summary>
	/// TerritoriesView.
	/// </summary>		
	[Serializable]
	public partial class TerritoriesView : Territories
	{
		#region Constant
		public const string	_Region_RegionDescription = "Region_RegionDescription";			
		#endregion
		
		#region Member Variables		
		private string region_RegionDescription;			
		#endregion

		#region Public Properties
		[Column("RegionDescription", Foreign = TerritoriesView.Region, ColumnMode = ColumnMode.Read)]
		public string Region_RegionDescription
		{
			get { return region_RegionDescription; }			
			set { region_RegionDescription = value; }
		}
		
		#endregion
	}
	#endregion	
}
