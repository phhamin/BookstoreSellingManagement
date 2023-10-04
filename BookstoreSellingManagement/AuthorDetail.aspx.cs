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
    public partial class AuthorDetail : System.Web.UI.Page
    {
        private TblAuthor GetAuthorInfoFromDatabase(Guid authorId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào authorId
            TblAuthor author = AuthorsManager.GetAuthorById(authorId);

            return author;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["authorId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["authorId"], out Guid authorId))
                    {
                        LoadData(authorId);
                    }
                    else
                    {
                        // Xử lý khi không có authorId (trang thêm mới)

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if (txtAuthorName.Text == string.Empty)
            {
                iValidAuthorName.Visible = true;
                txtAuthorName.Focus();
                return;
            }

            //Author Name trùng 
            if (Request.QueryString["authorId"] == null)
            {
                if (IsAuthorNameExists(txtAuthorName.Text))
                {
                    iValidAuthorName.Visible = true;
                    iValidAuthorName.InnerHtml = "Author Name already exists. Please choose a different Author Name";
                    txtAuthorName.Text = string.Empty;
                    txtAuthorName.Focus();
                    return;
                }
            }

            //Ngăn không cho bỏ trống 
            if (txtAddress.Text == string.Empty)
            {
                iValidAddress.Visible = true;
                txtAddress.Focus();
                return;
            }
            if (txtBiography.Text == string.Empty)
            {
                iValidBiography.Visible = true;
                txtBiography.Focus();
                return;
            }
            if (txtPhone.Text == string.Empty)
            {
                iValidPhone.Visible = true;
                txtPhone.Focus();
                return;
            }

            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["authorId"], out Guid authorId))
            {
                TblAuthor authorToUpdate = AuthorsManager.GetAuthorById(authorId);

                if (authorToUpdate != null)
                {
                    // Cập nhật thông tin 
                    authorToUpdate.AuthorName = txtAuthorName.Text;
                    authorToUpdate.Address = txtAddress.Text;
                    authorToUpdate.Biography = txtBiography.Text;
                    authorToUpdate.Phone = txtPhone.Text;
                    // Gọi phương thức UpdateAuthor để cập nhật vào cơ sở dữ liệu
                    TblAuthor updatedAuthor = AuthorsManager.UpdateAuthor(authorToUpdate);

                    if (updatedAuthor != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        iValidAuthorName.Visible = false;
                        iValidAddress.Visible = false;
                        iValidBiography.Visible = false;
                        iValidPhone.Visible = false;
                        // Hiển thị pop-up thông báo thành công
                        LoadData(authorId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("AuthorDetail.aspx");
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
                //Không có authorId
                //Xử lý Create khi người dùng thêm mới
                TblAuthor newAuthor = new TblAuthor
                {
                    AuthorName = txtAuthorName.Text,
                    Address = txtAddress.Text,
                    Biography = txtBiography.Text,
                    Phone = txtPhone.Text,
                };
                
                // Gọi phương thức BLL để thêm người dùng mới
                TblAuthor insertedAuthor = AuthorsManager.InsertAuthor(newAuthor);

                if (insertedAuthor != null)
                {
                    // Lưu authorId vào session
                    Session["NewAuthorId"] = insertedAuthor.Id;
                    iValidAuthorName.Visible = false;
                    iValidAddress.Visible = false;
                    iValidBiography.Visible = false;
                    iValidPhone.Visible = false;

                    // Chuyển hướng
                    LoadData(insertedAuthor.Id);
                    //Response.Redirect("AuthorDetail.aspx?authorId=" + insertedAuthor.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'AuthorDetail.aspx?authorId=" + insertedAuthor.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("AuthorDetail.aspx?Id=" + insertedAuthor.Id);
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
            Response.Redirect("Authors.aspx");
        }
        public void LoadData(Guid authorId)
        {
            TblAuthor author = GetAuthorInfoFromDatabase(authorId);

            txtAuthorName.Text = author.AuthorName;
            txtAddress.Text = author.Address;
            txtBiography.Text = author.Biography;
            txtPhone.Text = author.Phone;
        }


        private bool IsAuthorNameExists(string authorName)
        {
            return AuthorsManager.IsAuthorNameExists(authorName);
        }
    }
}