using Bookstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubSonic;
namespace BLL
{
    public class CustomersManager
    {
        public static TblCustomer InsertCustomer(TblCustomer item)
        {
            return new TblCustomerController().Insert(item);
        }

        public static TblCustomer UpdateCustomer(TblCustomer item)
        {
            return new TblCustomerController().Update(item);
        }
        public static TblCustomer GetCustomerById(Guid Id)
        {
            return new Select().From(TblCustomer.Schema.TableName).Where(TblCustomer.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblCustomer>();
        }
        public static List<TblCustomer> GetListCustomer()
        {
            return new Select().From(TblCustomer.Schema.TableName).ExecuteTypedList<TblCustomer>();
        }
        //public static bool DeleteCustomer(Guid Id)
        //{
        //    var customer = new Select().From<TblCustomer>().Where(TblCustomer.Columns.Id).IsEqualTo(Id).ExecuteSingle<TblCustomer>();
        //    return true;
        //}
        public static bool DeleteCustomer(Guid customerId)
        {
            try
            {
                // Sử dụng SubSonic để tạo truy vấn xóa
                var deleteQuery = new Delete().From<TblCustomer>().Where(TblCustomer.Columns.Id).IsEqualTo(customerId);

                // Thực hiện truy vấn xóa
                deleteQuery.Execute();

                return true;
            }
            catch (Exception )
            {
                // Xử lý lỗi nếu cần
                return false;
            }
        }
        // Phương thức kiểm tra sự tồn tại của tên người dùng
        public static bool IsUserNameExists(string userName)
        {
            var query = new Select().From<TblCustomer>().Where(TblCustomer.Columns.UserName).IsEqualTo(userName);

            int count = query.GetRecordCount();
            return count > 0;
        }
    }
}
