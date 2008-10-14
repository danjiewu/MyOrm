using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Categories
	/// <summary>
	/// Categories.
	/// </summary>
	[Table("Categories")]
	[Serializable]
	public class Categories 
	{
		#region Constant		
		public const string	_CategoryID = "CategoryID";
		public const string	_CategoryName = "CategoryName";
		public const string	_Description = "Description";
		public const string	_Picture = "Picture";
		#endregion
		
		#region Member Variables		
		private int categoryID;
		private string categoryName;
		private string description;
		private byte[] picture;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public int CategoryID
		{
			get { return categoryID; }			
			set { categoryID = value; }
		}
		
		[Column]
		public string CategoryName
		{
			get { return categoryName; }			
			set { categoryName = value; }
		}
		
		[Column]
		public string Description
		{
			get { return description; }			
			set { description = value; }
		}
		
		[Column]
		public byte[] Picture
		{
			get { return picture; }			
			set { picture = value; }
		}
		
		#endregion
	}
	#endregion
}
