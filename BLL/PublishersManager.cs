using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PublishersManager
    {
        public static TblPublisher InsertPublisher(TblPublisher item)
        {
            return new TblPublisherController().Insert(item);
        }

        public static TblPublisher UpdatePublisher(TblPublisher item)
        {
            return new TblPublisherController().Update(item);
        }
        public static TblPublisher GetPublisherById(Guid Id)
        {
            return new Select().From(TblPublisher.Schema.TableName).Where(TblPublisher.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblPublisher>();
        }
        public static List<TblPublisher> GetListPublisher()
        {
            return new Select().From(TblPublisher.Schema.TableName).ExecuteTypedList<TblPublisher>();
        }
        //public static bool DeletePublisher(Guid Id)
        //{
        //    var publisher = new Select().From<TblPublisher>().Where(TblPublisher.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblPublisher>();
        //    return true;
        //}
        public static bool DeletePublisher(Guid publisherId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblPublisher>().Where(TblPublisher.Columns.Id).IsEqualTo(publisherId);

                // Thực hiện truy vấn xóa
                deleteQuery.Execute();

                return true;
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu cần
                return false;
            }
        }
        // Phương thức kiểm tra sự tồn tại của tên người dùng
        public static bool IsPublisherNameExists(string publisherName)
        {
            var query = new Select().From<TblPublisher>().Where(TblPublisher.Columns.PublisherName).IsEqualTo(publisherName);

            int count = query.GetRecordCount();
            return count > 0;
        }

    }
}
