using BLL;
using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreSellingManagement
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra và áp dụng thông tin sắp xếp từ Session
                if (Session["SortExpression"] != null && Session["SortDirection"] != null)
                {
                    string sortExpression = Session["SortExpression"].ToString();
                    SortDirection sortDirection = (SortDirection)Session["SortDirection"];

                    ApplySorting(sortExpression, sortDirection);
                }

                // Kiểm tra và áp dụng thông tin trang hiện tại từ Session
                if (Session["CurrentPage"] != null)
                {
                    gvCustomers.PageIndex = (int)Session["CurrentPage"];
                }

                BindCustomers();
            }
        }


        private void BindCustomers()
        {
            BLL.CustomersManager customerManager = new BLL.CustomersManager();
            List<TblCustomer> customers = CustomersManager.GetListCustomer();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                customers = ApplySortingToCustomers(customers, sortExpression, sortDirection);
            }
            else
            {
                customers = customers.OrderByDescending(u => u.CreatedDate).ToList();
            }


            gvCustomers.DataSource = customers;
            gvCustomers.DataBind();
        }


        private List<TblCustomer> ApplySortingToCustomers(List<TblCustomer> customers, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblCustomer).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    customers = customers.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    customers = customers.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return customers;
        }
        protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCustomer")
            {
                string customerId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang CustomerDetail.aspx với tham số customerId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("CustomerDetail.aspx?customerId=" + customerId);
            }
            if (e.CommandName == "DeleteCustomer")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid customerId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = CustomersManager.DeleteCustomer(customerId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindCustomers();
                        //Response.Redirect("Customers.aspx");

                    }
                    else
                    {
                        // Xử lý khi xóa thất bại
                    }

                }
            }
        }
        protected string ConvertSexToString(bool sex)
        {
            return sex ? "Male" : "Female";
        }

        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSex = (Label)e.Row.FindControl("lblSex");
                bool sex = (bool)DataBinder.Eval(e.Row.DataItem, "Sex");
                lblSex.Text = ConvertSexToString(sex);
            }
        }

        protected void gvCustomers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindCustomers();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedCustomerQuery(sortExpression, sortDirection);
            List<TblCustomer> customers = sortedQuery.ExecuteTypedList<TblCustomer>();
            gvCustomers.DataSource = customers;
            gvCustomers.DataBind();
        }


        private SqlQuery GetSortedCustomerQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblCustomer>();

            if (sortDirection == SortDirection.Descending)
            {
                query = query.OrderDesc(sortExpression);
            }
            else
            {
                query = query.OrderAsc(sortExpression);
            }

            return query;
        }
        private SortDirection GetSortDirection(string sortExpression)
        {
            SortDirection sortDirection = SortDirection.Ascending; // Mặc định là tăng dần

            // Kiểm tra nếu cột hiện tại đã được sắp xếp trước đó
            if (Session["SortExpression"] != null && Session["SortExpression"].ToString() == sortExpression)
            {
                // Đảo ngược hướng sắp xếp nếu cùng cột
                sortDirection = (GetSortDirectionFromSession() == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            return sortDirection;
        }

        private SortDirection GetSortDirectionFromSession()
        {
            // Lấy hướng sắp xếp từ Session, mặc định là tăng dần
            if (Session["SortDirection"] == null)
            {
                Session["SortDirection"] = SortDirection.Ascending;
            }

            return (SortDirection)Session["SortDirection"];
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex != gvCustomers.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Ses sion
                gvCustomers.PageIndex = e.NewPageIndex;

                Session["pageNumber"] = e.NewPageIndex;

                // Gọi lại hàm BindCustomers để hiển thị trang mới
                BindCustomers();
            }
        }
    }
}