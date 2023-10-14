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
    public partial class Books : System.Web.UI.Page
    {
        public static string FunctionPage = "Books";
        private static string ssSortExpression = "SortExpression" + FunctionPage;
        private static string ssSortDirection = "SortDirection" + FunctionPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kiểm tra và áp dụng thông tin sắp xếp từ Session
                if (Session[ssSortExpression] != null && Session[ssSortDirection] != null)
                {
                    string sortExpression = Session[ssSortExpression].ToString();
                    SortDirection sortDirection = (SortDirection)Session[ssSortDirection];

                    ApplySorting(sortExpression, sortDirection);
                }

                // Kiểm tra và áp dụng thông tin trang hiện tại từ Session
                if (Session["CurrentPage"] != null)
                {
                    gvBooks.PageIndex = (int)Session["CurrentPage"];
                }

                BindBooks();
            }
        }


        private void BindBooks()
        {
            BLL.BooksManager BookManager = new BLL.BooksManager();
            List<TblBook> books = BooksManager.GetListBook();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                books = ApplySortingToBooks(books, sortExpression, sortDirection);
            }
            else
            {
                books = books.OrderByDescending(u => u.BookTitle).ToList();
            }


            gvBooks.DataSource = books;
            gvBooks.DataBind();
        }


        private List<TblBook> ApplySortingToBooks(List<TblBook> books, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblBook).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    books = books.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    books = books.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return books;
        }
        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBook")
            {
                string bookId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang BookDetail.aspx với tham số bookId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("BookDetail.aspx?bookId=" + bookId);
            }
            if (e.CommandName == "DeleteBook")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid bookId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = BooksManager.DeleteBook(bookId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindBooks();
                        //Response.Redirect("Books.aspx");

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

        protected void gvBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSex = (Label)e.Row.FindControl("lblSex");
                bool sex = (bool)DataBinder.Eval(e.Row.DataItem, "Sex");
                lblSex.Text = ConvertSexToString(sex);
            }
        }

        protected void gvBooks_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindBooks();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedBookQuery(sortExpression, sortDirection);
            List<TblBook> books = sortedQuery.ExecuteTypedList<TblBook>();
            gvBooks.DataSource = books;
            gvBooks.DataBind();
        }


        private SqlQuery GetSortedBookQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblBook>();

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

        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex != gvBooks.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Ses sion
                gvBooks.PageIndex = e.NewPageIndex;

                Session["pageNumber"] = e.NewPageIndex;

                // Gọi lại hàm BindBooks để hiển thị trang mới
                BindBooks();
            }
        }

      
    }
}