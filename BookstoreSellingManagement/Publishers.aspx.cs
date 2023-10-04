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
    public partial class Publisher : System.Web.UI.Page
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
                    gvPublishers.PageIndex = (int)Session["CurrentPage"];
                }

                BindPublishers();
            }
        }


        private void BindPublishers()
        {
            BLL.PublishersManager PublisherManager = new BLL.PublishersManager();
            List<TblPublisher> publishers = PublishersManager.GetListPublisher();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                publishers = ApplySortingToPublishers(publishers, sortExpression, sortDirection);
            }
            else
            {
                publishers = publishers.OrderByDescending(u => u.PublisherName).ToList();
            }


            gvPublishers.DataSource = publishers;
            gvPublishers.DataBind();
        }


        private List<TblPublisher> ApplySortingToPublishers(List<TblPublisher> publishers, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblPublisher).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    publishers = publishers.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    publishers = publishers.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return publishers;
        }
        protected void gvPublishers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPublisher")
            {
                string publisherId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang PublisherDetail.aspx với tham số publisherId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("PublisherDetail.aspx?publisherId=" + publisherId);
            }
            if (e.CommandName == "DeletePublisher")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid publisherId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = PublishersManager.DeletePublisher(publisherId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindPublishers();
                        //Response.Redirect("Publishers.aspx");

                    }
                    else
                    {
                        // Xử lý khi xóa thất bại
                    }

                }
            }
        }

        protected void gvPublishers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindPublishers();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedPublisherQuery(sortExpression, sortDirection);
            List<TblPublisher> publishers = sortedQuery.ExecuteTypedList<TblPublisher>();
            gvPublishers.DataSource = publishers;
            gvPublishers.DataBind();
        }


        private SqlQuery GetSortedPublisherQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblPublisher>();

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

        protected void gvPublishers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex != gvPublishers.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Ses sion
                gvPublishers.PageIndex = e.NewPageIndex;

                Session["pageNumber"] = e.NewPageIndex;

                // Gọi lại hàm BindPublishers để hiển thị trang mới
                BindPublishers();
            }
        }
    }
}