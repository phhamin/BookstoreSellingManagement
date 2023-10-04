using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AuthorsManager
    {
        public static TblAuthor InsertAuthor(TblAuthor item)
        {
            return new TblAuthorController().Insert(item);
        }

        public static TblAuthor UpdateAuthor(TblAuthor item)
        {
            return new TblAuthorController().Update(item);
        }
        public static TblAuthor GetAuthorById(Guid Id)
        {
            return new Select().From(TblAuthor.Schema.TableName).Where(TblAuthor.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblAuthor>();
        }
        public static List<TblAuthor> GetListAuthor()
        {
            return new Select().From(TblAuthor.Schema.TableName).ExecuteTypedList<TblAuthor>();
        }
        //public static bool DeleteAuthor(Guid Id)
        //{
        //    var author = new Select().From<TblAuthor>().Where(TblAuthor.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblAuthor>();
        //    return true;
        //}
        public static bool DeleteAuthor(Guid authorId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblAuthor>().Where(TblAuthor.Columns.Id).IsEqualTo(authorId);

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
        public static bool IsAuthorNameExists(string authorName)
        {
            var query = new Select().From<TblAuthor>().Where(TblAuthor.Columns.AuthorName).IsEqualTo(authorName);

            int count = query.GetRecordCount();
            return count > 0;
        }

    }
}
