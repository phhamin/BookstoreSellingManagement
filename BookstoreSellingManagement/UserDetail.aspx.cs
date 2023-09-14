using BLL;
using Bookstore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookstoreSellingManagement
{
    public partial class UserDetail : System.Web.UI.Page
    {
        private TblUser GetUserInfoFromDatabase(Guid userId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào userId
            TblUser user = UsersManager.GetUserById(userId);

            return user;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["userId"], out Guid userId))
                    {
                        LoadData(userId);
                    }
                    else
                    {
                        // Xử lý khi không có userId (trang thêm mới)

                    }
                }
            }
        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if(txtUserName.Text == string.Empty)
            {
                iValidUserName.Visible = true;
                txtUserName.Focus();
                return;
            }

            //User Name trùng 
            if(Request.QueryString["userId"] == null)
            {
                if (IsUserNameExists(txtUserName.Text))
                {
                    iValidUserName.Visible = true;
                    iValidUserName.InnerHtml = "User Name already exists. Please choose a different User Name";
                    txtUserName.Text = string.Empty;
                    txtUserName.Focus();
                    return;
                }
            }    
          

            //Ràng buộc cho mật khẩu
            if (Request.QueryString["userId"] == null)
            {
                //Không được chừa trống
                if (txtPassword.Text == string.Empty) 
                {
                    iValidPassword.Visible = true;
                    txtPassword.Focus();
                    return;
                }
                //Ít nhất 8 ký tự
                string password = txtPassword.Text;
                if (password.Length < 8)
                {
                    iValidPassword.Visible = true;
                    iValidPassword.InnerHtml = "Password must contain at least 8 characters";
                    txtUserName.Focus();
                    return;
                }
                //Độ mạnh mật khẩu (1 hoa, 1 số, 1 ký tự đặc biệt)
                if (!IsStrongPassword(password))
                {
                    iValidPassword.Visible = true;
                    iValidPassword.InnerHtml = "Password must contain at least one uppercase letter, one number and one special character";
                    txtUserName.Focus();
                    return;
                }
            }
            
            //Ngăn không cho bỏ trống
            if (txtFirstName.Text == string.Empty)
            {
                iValidFirstName.Visible = true;
                txtFirstName.Focus();
                return;
            }
            if(txtLastName.Text == string.Empty)
            {
                iValidLastName.Visible = true;
                txtLastName.Focus();
                return;
            }    
            if(txtEmail.Text == string.Empty)
            {
                iValidEmail.Visible = true;
                txtEmail.Focus();
                return;
            }    
            if(txtAddress.Text == string.Empty)
            {
                iValidAddress.Visible = true;
                txtAddress.Focus();
                return;
            }    
            if(txtPhone.Text == string.Empty)
            {
                iValidPhone.Visible = true;
                txtPhone.Focus();
                return;
            } 
             
            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["userId"], out Guid userId))
            {
                TblUser userToUpdate = UsersManager.GetUserById(userId);

                if (userToUpdate != null)
                {
                    // Cập nhật thông tin người dùng
                    userToUpdate.UserPassword = txtPassword.Text;

                    if (fileAvatar.HasFile)
                    {
                        string fileName = Path.GetFileName(fileAvatar.PostedFile.FileName);

                        string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                        // Lưu file vào thư mục Uploads
                        fileAvatar.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                        // Lưu tên file vào trường Avatar trong CSDL                  
                        userToUpdate.Avatar = "img/" + fileName;
                    }

                    userToUpdate.FirstName = txtFirstName.Text;
                    userToUpdate.LastName = txtLastName.Text;
                    userToUpdate.Sex = drdSex.SelectedValue == "Male" ? true : false;
                    userToUpdate.Email = txtEmail.Text;
                    userToUpdate.UserAddress = txtAddress.Text;
                    userToUpdate.Phone = txtPhone.Text;
                    // Đặt giá trị UpdatedDate bằng ngày hiện tại
                    userToUpdate.UpdatedDate = DateTime.Now;

                    // Gọi phương thức UpdateUser để cập nhật vào cơ sở dữ liệu
                    TblUser updatedUser = UsersManager.UpdateUser(userToUpdate);

                    if (updatedUser != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        // Hiển thị pop-up thông báo thành công
                        iValidUserName.Visible = false;
                        iValidPassword.Visible = false;
                        LoadData(userId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("UserDetail.aspx");
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
                //Không có userId
                //Xử lý Create khi người dùng thêm mới
                TblUser newUser = new TblUser
                {
                    UserName = txtUserName.Text,
                    UserPassword = txtPassword.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Sex = drdSex.SelectedValue == "Male" ? true : false, // Chỉnh sửa giá trị này dựa trên giới tính
                    Email = txtEmail.Text,
                    UserAddress = txtAddress.Text,
                    Phone = txtPhone.Text,
                    // Đặt giá trị CreatedDate bằng ngày hiện tại
                    CreatedDate = DateTime.Now,
                    // Đặt giá trị UpdatedDate bằng ngày hiện tại
                    UpdatedDate = DateTime.Now,

                };
                if (fileAvatar.HasFile)
                {
                    string fileName = Path.GetFileName(fileAvatar.PostedFile.FileName);

                    string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                    // Lưu file vào thư mục Uploads
                    fileAvatar.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                    // Lưu tên file vào trường Avatar trong CSDL
                    newUser.Avatar = "img/" + fileName;

                }
                // Gọi phương thức BLL để thêm người dùng mới
                TblUser insertedUser = UsersManager.InsertUser(newUser);

                if (insertedUser != null)
                {
                    // Lưu userId vào session
                    Session["NewUserId"] = insertedUser.Id;

                    
                    iValidUserName.Visible = false;
                    iValidPassword.Visible = false;
                    
                    // Chuyển hướng
                    LoadData(insertedUser.Id);
                    //Response.Redirect("UserDetail.aspx?userId=" + insertedUser.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'UserDetail.aspx?userId=" + insertedUser.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("UserDetail.aspx?Id=" + insertedUser.Id);
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
            Response.Redirect("Users.aspx");
        }
        public void LoadData(Guid userId)
        {
            TblUser user = GetUserInfoFromDatabase(userId);

            txtUserName.Text = user.UserName;
            txtUserName.ReadOnly = true;

            if (!string.IsNullOrEmpty(user.Avatar))
            {
                imgAvatar.ImageUrl = user.Avatar;
                imgAvatar.Visible = true;
            }

            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            if (user.Sex == true)
            {
                drdSex.SelectedIndex = drdSex.Items.IndexOf(drdSex.Items.FindByValue("Male"));
            }
            else if (user.Sex == false)
            {
                drdSex.SelectedIndex = drdSex.Items.IndexOf(drdSex.Items.FindByValue("Female"));
            }
            txtEmail.Text = user.Email;
            txtAddress.Text = user.UserAddress;
            txtPhone.Text = user.Phone;
            
        }


        // Hàm kiểm tra độ mạnh của mật khẩu
        private bool IsStrongPassword(string password)
        {
            // Kiểm tra mật khẩu có ít nhất một chữ hoa
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Kiểm tra mật khẩu có ít nhất một chữ số
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Kiểm tra mật khẩu có ít nhất một ký tự đặc biệt
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            return true;
        }

        private bool IsUserNameExists(string userName)
        {
            return UsersManager.IsUserNameExists(userName);
        }



    }
}