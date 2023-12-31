using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace Bookstore
{
	/// <summary>
	/// Strongly-typed collection for the TblBook class.
	/// </summary>
    [Serializable]
	public partial class TblBookCollection : ActiveList<TblBook, TblBookCollection>
	{	   
		public TblBookCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>TblBookCollection</returns>
		public TblBookCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                TblBook o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the TblBook table.
	/// </summary>
	[Serializable]
	public partial class TblBook : ActiveRecord<TblBook>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public TblBook()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public TblBook(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public TblBook(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public TblBook(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("TblBook", TableType.Table, DataService.GetInstance("Bookstore"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Guid;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = false;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				
						colvarId.DefaultSetting = @"(newid())";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarCode = new TableSchema.TableColumn(schema);
				colvarCode.ColumnName = "Code";
				colvarCode.DataType = DbType.Guid;
				colvarCode.MaxLength = 0;
				colvarCode.AutoIncrement = false;
				colvarCode.IsNullable = true;
				colvarCode.IsPrimaryKey = false;
				colvarCode.IsForeignKey = false;
				colvarCode.IsReadOnly = false;
				
						colvarCode.DefaultSetting = @"(newid())";
				colvarCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCode);
				
				TableSchema.TableColumn colvarBookTitle = new TableSchema.TableColumn(schema);
				colvarBookTitle.ColumnName = "BookTitle";
				colvarBookTitle.DataType = DbType.String;
				colvarBookTitle.MaxLength = 50;
				colvarBookTitle.AutoIncrement = false;
				colvarBookTitle.IsNullable = false;
				colvarBookTitle.IsPrimaryKey = false;
				colvarBookTitle.IsForeignKey = false;
				colvarBookTitle.IsReadOnly = false;
				
						colvarBookTitle.DefaultSetting = @"('')";
				colvarBookTitle.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBookTitle);
				
				TableSchema.TableColumn colvarPrice = new TableSchema.TableColumn(schema);
				colvarPrice.ColumnName = "Price";
				colvarPrice.DataType = DbType.Decimal;
				colvarPrice.MaxLength = 0;
				colvarPrice.AutoIncrement = false;
				colvarPrice.IsNullable = false;
				colvarPrice.IsPrimaryKey = false;
				colvarPrice.IsForeignKey = false;
				colvarPrice.IsReadOnly = false;
				
						colvarPrice.DefaultSetting = @"((0))";
				colvarPrice.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPrice);
				
				TableSchema.TableColumn colvarBookDescription = new TableSchema.TableColumn(schema);
				colvarBookDescription.ColumnName = "BookDescription";
				colvarBookDescription.DataType = DbType.String;
				colvarBookDescription.MaxLength = 200;
				colvarBookDescription.AutoIncrement = false;
				colvarBookDescription.IsNullable = false;
				colvarBookDescription.IsPrimaryKey = false;
				colvarBookDescription.IsForeignKey = false;
				colvarBookDescription.IsReadOnly = false;
				
						colvarBookDescription.DefaultSetting = @"('')";
				colvarBookDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBookDescription);
				
				TableSchema.TableColumn colvarBookImage = new TableSchema.TableColumn(schema);
				colvarBookImage.ColumnName = "BookImage";
				colvarBookImage.DataType = DbType.String;
				colvarBookImage.MaxLength = 50;
				colvarBookImage.AutoIncrement = false;
				colvarBookImage.IsNullable = false;
				colvarBookImage.IsPrimaryKey = false;
				colvarBookImage.IsForeignKey = false;
				colvarBookImage.IsReadOnly = false;
				
						colvarBookImage.DefaultSetting = @"('')";
				colvarBookImage.ForeignKeyTableName = "";
				schema.Columns.Add(colvarBookImage);
				
				TableSchema.TableColumn colvarIsActived = new TableSchema.TableColumn(schema);
				colvarIsActived.ColumnName = "IsActived";
				colvarIsActived.DataType = DbType.Boolean;
				colvarIsActived.MaxLength = 0;
				colvarIsActived.AutoIncrement = false;
				colvarIsActived.IsNullable = false;
				colvarIsActived.IsPrimaryKey = false;
				colvarIsActived.IsForeignKey = false;
				colvarIsActived.IsReadOnly = false;
				
						colvarIsActived.DefaultSetting = @"((1))";
				colvarIsActived.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActived);
				
				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				
						colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);
				
				TableSchema.TableColumn colvarCreatedDate = new TableSchema.TableColumn(schema);
				colvarCreatedDate.ColumnName = "CreatedDate";
				colvarCreatedDate.DataType = DbType.DateTime;
				colvarCreatedDate.MaxLength = 0;
				colvarCreatedDate.AutoIncrement = false;
				colvarCreatedDate.IsNullable = false;
				colvarCreatedDate.IsPrimaryKey = false;
				colvarCreatedDate.IsForeignKey = false;
				colvarCreatedDate.IsReadOnly = false;
				
						colvarCreatedDate.DefaultSetting = @"(getdate())";
				colvarCreatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedDate);
				
				TableSchema.TableColumn colvarCreatedUser = new TableSchema.TableColumn(schema);
				colvarCreatedUser.ColumnName = "CreatedUser";
				colvarCreatedUser.DataType = DbType.AnsiString;
				colvarCreatedUser.MaxLength = 30;
				colvarCreatedUser.AutoIncrement = false;
				colvarCreatedUser.IsNullable = false;
				colvarCreatedUser.IsPrimaryKey = false;
				colvarCreatedUser.IsForeignKey = false;
				colvarCreatedUser.IsReadOnly = false;
				
						colvarCreatedUser.DefaultSetting = @"('')";
				colvarCreatedUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedUser);
				
				TableSchema.TableColumn colvarUpdatedUser = new TableSchema.TableColumn(schema);
				colvarUpdatedUser.ColumnName = "UpdatedUser";
				colvarUpdatedUser.DataType = DbType.AnsiString;
				colvarUpdatedUser.MaxLength = 30;
				colvarUpdatedUser.AutoIncrement = false;
				colvarUpdatedUser.IsNullable = false;
				colvarUpdatedUser.IsPrimaryKey = false;
				colvarUpdatedUser.IsForeignKey = false;
				colvarUpdatedUser.IsReadOnly = false;
				
						colvarUpdatedUser.DefaultSetting = @"('')";
				colvarUpdatedUser.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdatedUser);
				
				TableSchema.TableColumn colvarUpdatedDate = new TableSchema.TableColumn(schema);
				colvarUpdatedDate.ColumnName = "UpdatedDate";
				colvarUpdatedDate.DataType = DbType.DateTime;
				colvarUpdatedDate.MaxLength = 0;
				colvarUpdatedDate.AutoIncrement = false;
				colvarUpdatedDate.IsNullable = false;
				colvarUpdatedDate.IsPrimaryKey = false;
				colvarUpdatedDate.IsForeignKey = false;
				colvarUpdatedDate.IsReadOnly = false;
				
						colvarUpdatedDate.DefaultSetting = @"(getdate())";
				colvarUpdatedDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdatedDate);
				
				TableSchema.TableColumn colvarQuantity = new TableSchema.TableColumn(schema);
				colvarQuantity.ColumnName = "Quantity";
				colvarQuantity.DataType = DbType.Int32;
				colvarQuantity.MaxLength = 0;
				colvarQuantity.AutoIncrement = false;
				colvarQuantity.IsNullable = false;
				colvarQuantity.IsPrimaryKey = false;
				colvarQuantity.IsForeignKey = false;
				colvarQuantity.IsReadOnly = false;
				
						colvarQuantity.DefaultSetting = @"((0))";
				colvarQuantity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarQuantity);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["Bookstore"].AddSchema("TblBook",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public Guid Id 
		{
			get { return GetColumnValue<Guid>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("Code")]
		[Bindable(true)]
		public Guid? Code 
		{
			get { return GetColumnValue<Guid?>(Columns.Code); }
			set { SetColumnValue(Columns.Code, value); }
		}
		  
		[XmlAttribute("BookTitle")]
		[Bindable(true)]
		public string BookTitle 
		{
			get { return GetColumnValue<string>(Columns.BookTitle); }
			set { SetColumnValue(Columns.BookTitle, value); }
		}
		  
		[XmlAttribute("Price")]
		[Bindable(true)]
		public decimal Price 
		{
			get { return GetColumnValue<decimal>(Columns.Price); }
			set { SetColumnValue(Columns.Price, value); }
		}
		  
		[XmlAttribute("BookDescription")]
		[Bindable(true)]
		public string BookDescription 
		{
			get { return GetColumnValue<string>(Columns.BookDescription); }
			set { SetColumnValue(Columns.BookDescription, value); }
		}
		  
		[XmlAttribute("BookImage")]
		[Bindable(true)]
		public string BookImage 
		{
			get { return GetColumnValue<string>(Columns.BookImage); }
			set { SetColumnValue(Columns.BookImage, value); }
		}
		  
		[XmlAttribute("IsActived")]
		[Bindable(true)]
		public bool IsActived 
		{
			get { return GetColumnValue<bool>(Columns.IsActived); }
			set { SetColumnValue(Columns.IsActived, value); }
		}
		  
		[XmlAttribute("IsDeleted")]
		[Bindable(true)]
		public bool IsDeleted 
		{
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set { SetColumnValue(Columns.IsDeleted, value); }
		}
		  
		[XmlAttribute("CreatedDate")]
		[Bindable(true)]
		public DateTime CreatedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.CreatedDate); }
			set { SetColumnValue(Columns.CreatedDate, value); }
		}
		  
		[XmlAttribute("CreatedUser")]
		[Bindable(true)]
		public string CreatedUser 
		{
			get { return GetColumnValue<string>(Columns.CreatedUser); }
			set { SetColumnValue(Columns.CreatedUser, value); }
		}
		  
		[XmlAttribute("UpdatedUser")]
		[Bindable(true)]
		public string UpdatedUser 
		{
			get { return GetColumnValue<string>(Columns.UpdatedUser); }
			set { SetColumnValue(Columns.UpdatedUser, value); }
		}
		  
		[XmlAttribute("UpdatedDate")]
		[Bindable(true)]
		public DateTime UpdatedDate 
		{
			get { return GetColumnValue<DateTime>(Columns.UpdatedDate); }
			set { SetColumnValue(Columns.UpdatedDate, value); }
		}
		  
		[XmlAttribute("Quantity")]
		[Bindable(true)]
		public int Quantity 
		{
			get { return GetColumnValue<int>(Columns.Quantity); }
			set { SetColumnValue(Columns.Quantity, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(Guid varId,Guid? varCode,string varBookTitle,decimal varPrice,string varBookDescription,string varBookImage,bool varIsActived,bool varIsDeleted,DateTime varCreatedDate,string varCreatedUser,string varUpdatedUser,DateTime varUpdatedDate,int varQuantity)
		{
			TblBook item = new TblBook();
			
			item.Id = varId;
			
			item.Code = varCode;
			
			item.BookTitle = varBookTitle;
			
			item.Price = varPrice;
			
			item.BookDescription = varBookDescription;
			
			item.BookImage = varBookImage;
			
			item.IsActived = varIsActived;
			
			item.IsDeleted = varIsDeleted;
			
			item.CreatedDate = varCreatedDate;
			
			item.CreatedUser = varCreatedUser;
			
			item.UpdatedUser = varUpdatedUser;
			
			item.UpdatedDate = varUpdatedDate;
			
			item.Quantity = varQuantity;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(Guid varId,Guid? varCode,string varBookTitle,decimal varPrice,string varBookDescription,string varBookImage,bool varIsActived,bool varIsDeleted,DateTime varCreatedDate,string varCreatedUser,string varUpdatedUser,DateTime varUpdatedDate,int varQuantity)
		{
			TblBook item = new TblBook();
			
				item.Id = varId;
			
				item.Code = varCode;
			
				item.BookTitle = varBookTitle;
			
				item.Price = varPrice;
			
				item.BookDescription = varBookDescription;
			
				item.BookImage = varBookImage;
			
				item.IsActived = varIsActived;
			
				item.IsDeleted = varIsDeleted;
			
				item.CreatedDate = varCreatedDate;
			
				item.CreatedUser = varCreatedUser;
			
				item.UpdatedUser = varUpdatedUser;
			
				item.UpdatedDate = varUpdatedDate;
			
				item.Quantity = varQuantity;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn CodeColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn BookTitleColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn PriceColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn BookDescriptionColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn BookImageColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn IsActivedColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn IsDeletedColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedDateColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CreatedUserColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdatedUserColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdatedDateColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn QuantityColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string Code = @"Code";
			 public static string BookTitle = @"BookTitle";
			 public static string Price = @"Price";
			 public static string BookDescription = @"BookDescription";
			 public static string BookImage = @"BookImage";
			 public static string IsActived = @"IsActived";
			 public static string IsDeleted = @"IsDeleted";
			 public static string CreatedDate = @"CreatedDate";
			 public static string CreatedUser = @"CreatedUser";
			 public static string UpdatedUser = @"UpdatedUser";
			 public static string UpdatedDate = @"UpdatedDate";
			 public static string Quantity = @"Quantity";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
