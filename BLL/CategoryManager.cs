using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryManager
    {
        public static TblCategory InsertCategory(TblCategory item)
        {
            return new TblCategoryController().Insert(item);
        }

        public static TblCategory UpdateCategory(TblCategory item)
        {
            return new TblCategoryController().Update(item);
        }
        public static TblCategory GetCategoryById(Guid Id)
        {
            return new Select().From(TblCategory.Schema.TableName).Where(TblCategory.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblCategory>();
        }
        public static List<TblCategory> GetListCategory()
        {
            return new Select().From(TblCategory.Schema.TableName).ExecuteTypedList<TblCategory>();
        }
        //public static bool DeleteCategory(Guid Id)
        //{
        //    var category = new Select().From<TblCategory>().Where(TblCategory.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblCategory>();
        //    return true;
        //}
        public static bool DeleteCategory(Guid categoryId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblCategory>().Where(TblCategory.Columns.Id).IsEqualTo(categoryId);

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
        public static bool IsCategoryNameExists(string categoryName)
        {
            var query = new Select().From<TblCategory>().Where(TblCategory.Columns.CategoryName).IsEqualTo(categoryName);

            int count = query.GetRecordCount();
            return count > 0;
        }

    }
}
