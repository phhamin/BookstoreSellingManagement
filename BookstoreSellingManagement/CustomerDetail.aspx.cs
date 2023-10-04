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
    public partial class CustomerDetail : System.Web.UI.Page
    {
        private TblCustomer GetCustomerInfoFromDatabase(Guid customerId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào customerId
            TblCustomer customer = CustomersManager.GetCustomerById(customerId);

            return customer;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["customerId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["customerId"], out Guid customerId))
                    {
                        LoadData(customerId);
                    }
                    else
                    {
                        // Xử lý khi không có customerId (trang thêm mới)

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if (txtUserName.Text == string.Empty)
            {
                iValidUserName.Visible = true;
                txtUserName.Focus();
                return;
            }

            //User Name trùng 
            if (Request.QueryString["customerId"] == null)
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
            if (Request.QueryString["customerId"] == null)
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
            if (txtFullName.Text == string.Empty)
            {
                iValidFullName.Visible = true;
                txtFullName.Focus();
                return;
            }
            if (txtEmail.Text == string.Empty)
            {
                iValidEmail.Visible = true;
                txtEmail.Focus();
                return;
            }
            if (txtAddress.Text == string.Empty)
            {
                iValidAddress.Visible = true;
                txtAddress.Focus();
                return;
            }
            if (txtPhone.Text == string.Empty)
            {
                iValidPhone.Visible = true;
                txtPhone.Focus();
                return;
            }

            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["customerId"], out Guid customerId))
            {
                TblCustomer customerToUpdate = CustomersManager.GetCustomerById(customerId);

                if (customerToUpdate != null)
                {
                    // Cập nhật thông tin người dùng
                    customerToUpdate.CustomerPassword = txtPassword.Text;

                    if (fileAvatar.HasFile)
                    {
                        string fileName = Path.GetFileName(fileAvatar.PostedFile.FileName);

                        string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                        // Lưu file vào thư mục Uploads
                        fileAvatar.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                        // Lưu tên file vào trường Avatar trong CSDL                  
                        customerToUpdate.Avatar = "img/" + fileName;
                    }

                    customerToUpdate.FullName = txtFullName.Text;
                    customerToUpdate.Sex = drdSex.SelectedValue == "Male" ? true : false;

                    if (DateTime.TryParse(txtBirthday.Text, out DateTime birthday))
                    {
                        customerToUpdate.Birthday = birthday;
                    }
                    else
                    {
                        // Xử lý lỗi khi ngày sinh không hợp lệ
                    }

                    customerToUpdate.Email = txtEmail.Text;
                    customerToUpdate.CustomerAddress = txtAddress.Text;
                    customerToUpdate.Phone = txtPhone.Text;
                    // Đặt giá trị UpdatedDate bằng ngày hiện tại
                    customerToUpdate.UpdatedDate = DateTime.Now;

                    // Gọi phương thức UpdateCustomer để cập nhật vào cơ sở dữ liệu
                    TblCustomer updatedCustomer = CustomersManager.UpdateCustomer(customerToUpdate);

                    if (updatedCustomer != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        // Hiển thị pop-up thông báo thành công
                        iValidUserName.Visible = false;
                        iValidPassword.Visible = false;
                        LoadData(customerId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("CustomerDetail.aspx");
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
                //Không có customerId
                //Xử lý Create khi người dùng thêm mới
                TblCustomer newCustomer = new TblCustomer
                {
                    Code = Guid.NewGuid(),
                    UserName = txtUserName.Text,
                    CustomerPassword = txtPassword.Text,
                    FullName = txtFullName.Text,
                    Sex = drdSex.SelectedValue == "Male" ? true : false, // Chỉnh sửa giá trị này dựa trên giới tính

                    Email = txtEmail.Text,
                    CustomerAddress = txtAddress.Text,
                    Phone = txtPhone.Text,
                    // Đặt giá trị CreatedDate bằng ngày hiện tại
                    CreatedDate = DateTime.Now,
                    // Đặt giá trị UpdatedDate bằng ngày hiện tại
                    UpdatedDate = DateTime.Now,

                };
                // Thêm xử lý cho trường "Birthday"
                if (DateTime.TryParse(txtBirthday.Text, out DateTime birthday))
                {
                    newCustomer.Birthday = birthday;
                }
                else
                {
                    // Xử lý lỗi khi ngày sinh không hợp lệ

                    return;
                }
                if (fileAvatar.HasFile)
                {
                    string fileName = Path.GetFileName(fileAvatar.PostedFile.FileName);

                    string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                    // Lưu file vào thư mục Uploads
                    fileAvatar.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                    // Lưu tên file vào trường Avatar trong CSDL
                    newCustomer.Avatar = "img/" + fileName;

                }
                // Gọi phương thức BLL để thêm người dùng mới
                TblCustomer insertedCustomer = CustomersManager.InsertCustomer(newCustomer);

                if (insertedCustomer != null)
                {
                    // Lưu customerId vào session
                    Session["NewCustomerId"] = insertedCustomer.Id;


                    iValidUserName.Visible = false;
                    iValidPassword.Visible = false;

                    // Chuyển hướng
                    LoadData(insertedCustomer.Id);
                    //Response.Redirect("CustomerDetail.aspx?customerId=" + insertedCustomer.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'CustomerDetail.aspx?customerId=" + insertedCustomer.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("CustomerDetail.aspx?Id=" + insertedCustomer.Id);
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
            Response.Redirect("Customers.aspx");
        }
        public void LoadData(Guid customerId)
        {
            TblCustomer customer = GetCustomerInfoFromDatabase(customerId);

            txtUserName.Text = customer.UserName;
            txtUserName.ReadOnly = true;

            if (!string.IsNullOrEmpty(customer.Avatar))
            {
                imgAvatar.ImageUrl = customer.Avatar;
                imgAvatar.Visible = true;
            }

            txtFullName.Text = customer.FullName;
            if (customer.Sex == true)
            {
                drdSex.SelectedIndex = drdSex.Items.IndexOf(drdSex.Items.FindByValue("Male"));
            }
            else if (customer.Sex == false)
            {
                drdSex.SelectedIndex = drdSex.Items.IndexOf(drdSex.Items.FindByValue("Female"));
            }

            txtBirthday.Text = customer.Birthday.ToString("dd-MM-yyyy");
            txtBirthday.TextMode = TextBoxMode.Date;
            txtEmail.Text = customer.Email;
            txtAddress.Text = customer.CustomerAddress;
            txtPhone.Text = customer.Phone;

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
            return CustomersManager.IsUserNameExists(userName);
        }
    }
}