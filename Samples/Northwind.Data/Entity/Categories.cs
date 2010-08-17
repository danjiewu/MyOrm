using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Categories
	/// <summary>
	/// Categories.
	/// </summary>
	[Table("Categories")]	
	[Serializable]
	public partial class Categories : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	CategoryID = "CategoryID";
		    public const string	CategoryName = "CategoryName";
		    public const string	Description = "Description";
		    public const string	Picture = "Picture";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// CategoryID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int CategoryID { get; set; }	
        
		/// <summary>
		/// CategoryName
		/// </summary>
		[Column(IsIndex = true)]
		public string CategoryName { get; set; }	
        
		/// <summary>
		/// Description
		/// </summary>
		[Column]
		public string Description { get; set; }	
        
		/// <summary>
		/// Picture
		/// </summary>
		[Column]
		public byte[] Picture { get; set; }	
        
		#endregion
        
        #region IIndexedProperty
		public override object this[string propertyName]
		{
			get
			{
				switch (propertyName)
				{
					case Properties.CategoryID: return CategoryID;
					case Properties.CategoryName: return CategoryName;
					case Properties.Description: return Description;
					case Properties.Picture: return Picture;
					default: return base[propertyName];
				}
			}
			set
			{
				switch (propertyName)
				{
					case Properties.CategoryID: CategoryID = (int)value; break;
					case Properties.CategoryName: CategoryName = (string)value; break;
					case Properties.Description: Description = (string)value; break;
					case Properties.Picture: Picture = (byte[])value; break;
					default: base[propertyName] = value; break;
				}
			}
		}
		
		#endregion
	}
	#endregion
}
