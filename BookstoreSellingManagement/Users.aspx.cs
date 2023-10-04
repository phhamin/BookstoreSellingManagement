using BLL;
using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreSellingManagement
{
    public partial class Users : System.Web.UI.Page
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
                    gvUsers.PageIndex = (int)Session["CurrentPage"];
                }

                BindUsers();
            }
        }


        private void BindUsers()
        {
            BLL.UsersManager userManager = new BLL.UsersManager();
            List<TblUser> users = UsersManager.GetListUser();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                users = ApplySortingToUsers(users, sortExpression, sortDirection);
            }
            else
            {
                users = users.OrderByDescending(u => u.CreatedDate).ToList();
            }


            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }


        private List<TblUser> ApplySortingToUsers(List<TblUser> users, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblUser).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    users = users.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    users = users.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return users;
        }


        //protected void btnSaveUser_Click(object sender, EventArgs e)
        //{
        //    TblUser objUser = new TblUser();

        //    UsersManager.InsertUser(objUser);

        //}
        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUser")
            {
                string userId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang UserDetail.aspx với tham số userId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("UserDetail.aspx?userId=" + userId);
            }
            if (e.CommandName == "DeleteUser")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid userId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = UsersManager.DeleteUser(userId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindUsers();
                        //Response.Redirect("Users.aspx");

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

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSex = (Label)e.Row.FindControl("lblSex");
                bool sex = (bool)DataBinder.Eval(e.Row.DataItem, "Sex");
                lblSex.Text = ConvertSexToString(sex);
            }
        }
        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            if (e.NewPageIndex != gvUsers.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Session
                gvUsers.PageIndex = e.NewPageIndex;
                
                Session["pageNumber"] = e.NewPageIndex;
         
                // Gọi lại hàm BindUsers để hiển thị trang mới
                BindUsers();
            }

        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindUsers();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedUserQuery(sortExpression, sortDirection);
            List<TblUser> users = sortedQuery.ExecuteTypedList<TblUser>();

            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }


        private SqlQuery GetSortedUserQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblUser>();
           
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
    }
}