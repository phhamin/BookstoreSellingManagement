using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BooksManager
    {
        public static TblBook InsertBook(TblBook item)
        {
            return new TblBookController().Insert(item);
        }

        public static TblBook UpdateBook(TblBook item)
        {
            return new TblBookController().Update(item);
        }
        public static TblBook GetBookById(Guid Id)
        {
            return new Select().From(TblBook.Schema.TableName).Where(TblBook.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblBook>();
        }
        public static List<TblBook> GetListBook()
        {
            return new Select().From(TblBook.Schema.TableName).ExecuteTypedList<TblBook>();
        }
        //public static bool DeleteBook(Guid Id)
        //{
        //    var book = new Select().From<TblBook>().Where(TblBook.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblBook>();
        //    return true;
        //}
        public static bool DeleteBook(Guid bookId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblBook>().Where(TblBook.Columns.Id).IsEqualTo(bookId);

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
        public static bool IsBookTitleExists(string bookTitle)
        {
            var query = new Select().From<TblBook>().Where(TblBook.Columns.BookTitle).IsEqualTo(bookTitle);

            int count = query.GetRecordCount();
            return count > 0;
        }
    }
}
