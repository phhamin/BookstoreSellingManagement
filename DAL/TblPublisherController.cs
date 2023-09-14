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
    /// Controller class for TblPublisher
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TblPublisherController
    {
        // Preload our schema..
        TblPublisher thisSchemaLoad = new TblPublisher();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public TblPublisherCollection FetchAll()
        {
            TblPublisherCollection coll = new TblPublisherCollection();
            Query qry = new Query(TblPublisher.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblPublisherCollection FetchByID(object Id)
        {
            TblPublisherCollection coll = new TblPublisherCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblPublisherCollection FetchByQuery(Query qry)
        {
            TblPublisherCollection coll = new TblPublisherCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TblPublisher.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TblPublisher.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid Id,string PublisherName,string Address,string Phone)
	    {
		    TblPublisher item = new TblPublisher();
		    
            item.Id = Id;
            
            item.PublisherName = PublisherName;
            
            item.Address = Address;
            
            item.Phone = Phone;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid Id,string PublisherName,string Address,string Phone)
	    {
		    TblPublisher item = new TblPublisher();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.PublisherName = PublisherName;
				
			item.Address = Address;
				
			item.Phone = Phone;
				
	        item.Save(UserName);
	    }
    }
}