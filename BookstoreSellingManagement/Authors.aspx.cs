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
    public partial class Authors : System.Web.UI.Page
    {
        public static string FunctionPage = "Books";
        private static string ssSortExpression = "SortExpression" + FunctionPage;
        private static string ssSortDirection = "SortDirection" + FunctionPage;
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
                    gvAuthors.PageIndex = (int)Session["CurrentPage"];
                }

                BindAuthors();
            }
        }


        private void BindAuthors()
        {
            BLL.AuthorsManager AuthorManager = new BLL.AuthorsManager();
            List<TblAuthor> authors = AuthorsManager.GetListAuthor();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                authors = ApplySortingToAuthors(authors, sortExpression, sortDirection);
            }
            else
            {
                authors = authors.OrderByDescending(u => u.AuthorName).ToList();
            }


            gvAuthors.DataSource = authors;
            gvAuthors.DataBind();
        }


        private List<TblAuthor> ApplySortingToAuthors(List<TblAuthor> authors, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblAuthor).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    authors = authors.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    authors = authors.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return authors;
        }
        protected void gvAuthors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditAuthor")
            {
                string authorId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang AuthorDetail.aspx với tham số authorId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("AuthorDetail.aspx?authorId=" + authorId);
            }
            if (e.CommandName == "DeleteAuthor")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid authorId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = AuthorsManager.DeleteAuthor(authorId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindAuthors();
                        //Response.Redirect("Authors.aspx");

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

        protected void gvAuthors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSex = (Label)e.Row.FindControl("lblSex");
                bool sex = (bool)DataBinder.Eval(e.Row.DataItem, "Sex");
                lblSex.Text = ConvertSexToString(sex);
            }
        }

        protected void gvAuthors_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindAuthors();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedAuthorQuery(sortExpression, sortDirection);
            List<TblAuthor> authors = sortedQuery.ExecuteTypedList<TblAuthor>();
            gvAuthors.DataSource = authors;
            gvAuthors.DataBind();
        }


        private SqlQuery GetSortedAuthorQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblAuthor>();

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

        protected void gvAuthors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex != gvAuthors.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Ses sion
                gvAuthors.PageIndex = e.NewPageIndex;

                Session["pageNumber"] = e.NewPageIndex;

                // Gọi lại hàm BindAuthors để hiển thị trang mới
                BindAuthors();
            }
        }
    }
}