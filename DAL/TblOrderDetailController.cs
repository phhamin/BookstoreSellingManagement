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
    /// Controller class for TblOrderDetail
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class TblOrderDetailController
    {
        // Preload our schema..
        TblOrderDetail thisSchemaLoad = new TblOrderDetail();
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
        public TblOrderDetailCollection FetchAll()
        {
            TblOrderDetailCollection coll = new TblOrderDetailCollection();
            Query qry = new Query(TblOrderDetail.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblOrderDetailCollection FetchByID(object Id)
        {
            TblOrderDetailCollection coll = new TblOrderDetailCollection().Where("Id", Id).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public TblOrderDetailCollection FetchByQuery(Query qry)
        {
            TblOrderDetailCollection coll = new TblOrderDetailCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Id)
        {
            return (TblOrderDetail.Delete(Id) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Id)
        {
            return (TblOrderDetail.Destroy(Id) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(Guid Id,Guid OrderId,Guid BookId,decimal Discount,int Quantity,decimal UnitPrice,decimal PaymentPrice)
	    {
		    TblOrderDetail item = new TblOrderDetail();
		    
            item.Id = Id;
            
            item.OrderId = OrderId;
            
            item.BookId = BookId;
            
            item.Discount = Discount;
            
            item.Quantity = Quantity;
            
            item.UnitPrice = UnitPrice;
            
            item.PaymentPrice = PaymentPrice;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(Guid Id,Guid OrderId,Guid BookId,decimal Discount,int Quantity,decimal UnitPrice,decimal PaymentPrice)
	    {
		    TblOrderDetail item = new TblOrderDetail();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Id = Id;
				
			item.OrderId = OrderId;
				
			item.BookId = BookId;
				
			item.Discount = Discount;
				
			item.Quantity = Quantity;
				
			item.UnitPrice = UnitPrice;
				
			item.PaymentPrice = PaymentPrice;
				
	        item.Save(UserName);
	    }
    }
}
