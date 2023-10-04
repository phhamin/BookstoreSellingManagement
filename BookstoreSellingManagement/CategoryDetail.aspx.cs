using BLL;
using Bookstore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreSellingManagement
{
    public partial class CategoryDetail : System.Web.UI.Page
    {
        private TblCategory GetCategoryInfoFromDatabase(Guid categoryId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào categoryId
            TblCategory category = CategoryManager.GetCategoryById(categoryId);

            return category;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["categoryId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["categoryId"], out Guid categoryId))
                    {
                        LoadData(categoryId);
                    }
                    else
                    {
                        // Xử lý khi không có categoryId (trang thêm mới)

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if (txtCategoryName.Text == string.Empty)
            {
                iValidCategoryName.Visible = true;
                txtCategoryName.Focus();
                return;
            }

            //Category Name trùng 
            if (Request.QueryString["categoryId"] == null)
            {
                if (IsCategoryNameExists(txtCategoryName.Text))
                {
                    iValidCategoryName.Visible = true;
                    iValidCategoryName.InnerHtml = "Category Name already exists. Please choose a different Category Name";
                    txtCategoryName.Text = string.Empty;
                    txtCategoryName.Focus();
                    return;
                }
            }

            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["categoryId"], out Guid categoryId))
            {
                TblCategory categoryToUpdate = CategoryManager.GetCategoryById(categoryId);

                if (categoryToUpdate != null)
                {
                    // Cập nhật thông tin 
                    categoryToUpdate.CategoryName = txtCategoryName.Text;
                    // Gọi phương thức UpdateCategory để cập nhật vào cơ sở dữ liệu
                    TblCategory updatedCategory = CategoryManager.UpdateCategory(categoryToUpdate);

                    if (updatedCategory != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        iValidCategoryName.Visible = false;
                        // Hiển thị pop-up thông báo thành công
                        LoadData(categoryId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("CategoryDetail.aspx");
                    }
                    else
                    {
                        // Xử lý khi người dùng không được cập nhật thành công
                    }
                }
                else
                {
                    //Không tìm thấy người dùng
                }
            }
            else
            {
                //Không có categoryId
                //Xử lý Create khi người dùng thêm mới
                TblCategory newCategory = new TblCategory
                {
                    CategoryName = txtCategoryName.Text,
                };

                // Gọi phương thức BLL để thêm người dùng mới
                TblCategory insertedCategory = CategoryManager.InsertCategory(newCategory);

                if (insertedCategory != null)
                {
                    // Lưu categoryId vào session
                    Session["NewCategoryId"] = insertedCategory.Id;
                    iValidCategoryName.Visible = false;

                    // Chuyển hướng
                    LoadData(insertedCategory.Id);
                    //Response.Redirect("CategoryDetail.aspx?categoryId=" + insertedCategory.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'CategoryDetail.aspx?categoryId=" + insertedCategory.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("CategoryDetail.aspx?Id=" + insertedCategory.Id);
                    //}

                }
                else
                {
                    //Xử lý lỗi
                }
            }


        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Category.aspx");
        }
        public void LoadData(Guid categoryId)
        {
            TblCategory category = GetCategoryInfoFromDatabase(categoryId);

            txtCategoryName.Text = category.CategoryName;
        }


        private bool IsCategoryNameExists(string categoryName)
        {
            return CategoryManager.IsCategoryNameExists(categoryName);
        }
    }
}