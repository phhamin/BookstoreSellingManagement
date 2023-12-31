﻿using BLL;
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
    public partial class Category : System.Web.UI.Page
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
                    gvCategorys.PageIndex = (int)Session["CurrentPage"];
                }

                BindCategorys();
            }
        }


        private void BindCategorys()
        {
            BLL.CategoryManager CategoryManager = new BLL.CategoryManager();
            List<TblCategory> categorys = CategoryManager.GetListCategory();

            if (Session["SortExpression"] != null && Session["SortDirection"] != null)
            {
                string sortExpression = Session["SortExpression"].ToString();
                SortDirection sortDirection = (SortDirection)Session["SortDirection"];
                categorys = ApplySortingToCategorys(categorys, sortExpression, sortDirection);
            }
            else
            {
                categorys = categorys.OrderByDescending(u => u.CategoryName).ToList();
            }


            gvCategorys.DataSource = categorys;
            gvCategorys.DataBind();
        }


        private List<TblCategory> ApplySortingToCategorys(List<TblCategory> categorys, string sortExpression, SortDirection sortDirection)
        {
            PropertyInfo propertyInfo = typeof(TblCategory).GetProperty(sortExpression);

            if (propertyInfo != null)
            {
                if (sortDirection == SortDirection.Descending)
                {
                    categorys = categorys.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
                }
                else
                {
                    categorys = categorys.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
                }
            }

            return categorys;
        }
        protected void gvCategorys_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCategory")
            {
                string categoryId = e.CommandArgument.ToString();

                // Chuyển hướng đến trang CategoryDetail.aspx với tham số categoryId để hiển thị chi tiết người dùng và cho chỉnh sửa
                Response.Redirect("CategoryDetail.aspx?categoryId=" + categoryId);
            }
            if (e.CommandName == "DeleteCategory")
            {
                if (Guid.TryParse(e.CommandArgument.ToString(), out Guid categoryId))
                {
                    // Gọi hàm xóa người dùng từ BLL bằng SubSonic
                    bool isDeleted = CategoryManager.DeleteCategory(categoryId);
                    if (isDeleted)
                    {
                        liveToast.Attributes["class"] = "toast show";
                        BindCategorys();
                        //Response.Redirect("Categorys.aspx");

                    }
                    else
                    {
                        // Xử lý khi xóa thất bại
                    }

                }
            }
        }

        protected void gvCategorys_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            SortDirection sortDirection = GetSortDirection(sortExpression);

            // Lưu thông tin sắp xếp vào Session
            Session["SortExpression"] = sortExpression;
            Session["SortDirection"] = sortDirection;

            // Thực hiện sắp xếp và cập nhật dữ liệu trong GridView
            BindCategorys();
        }

        private void ApplySorting(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery sortedQuery = GetSortedCategoryQuery(sortExpression, sortDirection);
            List<TblCategory> categorys = sortedQuery.ExecuteTypedList<TblCategory>();
            gvCategorys.DataSource = categorys;
            gvCategorys.DataBind();
        }


        private SqlQuery GetSortedCategoryQuery(string sortExpression, SortDirection sortDirection)
        {
            SqlQuery query = new Select().From<TblCategory>();

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

        protected void gvCategorys_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex != gvCategorys.PageIndex)
            {
                // Trang hiện tại đã thay đổi, lưu trang mới vào Ses sion
                gvCategorys.PageIndex = e.NewPageIndex;

                Session["pageNumber"] = e.NewPageIndex;

                // Gọi lại hàm BindCategorys để hiển thị trang mới
                BindCategorys();
            }
        }
    }
}