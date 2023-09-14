using Bookstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubSonic;

namespace BLL
{
    public class UsersManager
    {
        public static TblUser InsertUser(TblUser item)
        {
            return new TblUserController().Insert(item);
        }

        public static TblUser UpdateUser(TblUser item)
        {
            return new TblUserController().Update(item);
        }
        public static TblUser GetUserById(Guid Id)
        {
           return  new Select().From(TblUser.Schema.TableName).Where(TblUser.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblUser>();
        }
         public static List<TblUser> GetListUser()
         {
           return new Select().From(TblUser.Schema.TableName).ExecuteTypedList<TblUser>();
         }
        //public static bool DeleteUser(Guid Id)
        //{
        //    var user = new Select().From<TblUser>().Where(TblUser.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblUser>();
        //    return true;
        //}
        public static bool DeleteUser(Guid userId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblUser>().Where(TblUser.Columns.Id).IsEqualTo(userId);

                // Thực hiện truy vấn xóa
                deleteQuery.Execute();

                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return false;
            }
        }
        // Phương thức kiểm tra sự tồn tại của tên người dùng
        public static bool IsUserNameExists(string userName)
        {
            var query = new Select().From<TblUser>().Where(TblUser.Columns.UserName).IsEqualTo(userName);

            int count = query.GetRecordCount();
            return count > 0;
        }
      
    }
}
