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
    public partial class PublisherDetail : System.Web.UI.Page
    {
        private TblPublisher GetPublisherInfoFromDatabase(Guid publisherId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào publisherId
            TblPublisher publisher = PublishersManager.GetPublisherById(publisherId);

            return publisher;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["publisherId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["publisherId"], out Guid publisherId))
                    {
                        LoadData(publisherId);
                    }
                    else
                    {
                        // Xử lý khi không có publisherId (trang thêm mới)

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if (txtPublisherName.Text == string.Empty)
            {
                iValidPublisherName.Visible = true;
                txtPublisherName.Focus();
                return;
            }

            //Publisher Name trùng 
            if (Request.QueryString["publisherId"] == null)
            {
                if (IsPublisherNameExists(txtPublisherName.Text))
                {
                    iValidPublisherName.Visible = true;
                    iValidPublisherName.InnerHtml = "Publisher Name already exists. Please choose a different Publisher Name";
                    txtPublisherName.Text = string.Empty;
                    txtPublisherName.Focus();
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
            if (txtPhone.Text == string.Empty)
            {
                iValidPhone.Visible = true;
                txtPhone.Focus();
                return;
            }

            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["publisherId"], out Guid publisherId))
            {
                TblPublisher publisherToUpdate = PublishersManager.GetPublisherById(publisherId);

                if (publisherToUpdate != null)
                {
                    // Cập nhật thông tin 
                    publisherToUpdate.PublisherName = txtPublisherName.Text;
                    publisherToUpdate.Address = txtAddress.Text;
                    publisherToUpdate.Phone = txtPhone.Text;
                    // Gọi phương thức UpdatePublisher để cập nhật vào cơ sở dữ liệu
                    TblPublisher updatedPublisher = PublishersManager.UpdatePublisher(publisherToUpdate);

                    if (updatedPublisher != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        iValidPublisherName.Visible = false;
                        iValidAddress.Visible = false;
                        iValidPhone.Visible = false;
                        // Hiển thị pop-up thông báo thành công
                        LoadData(publisherId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("PublisherDetail.aspx");
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
                //Không có publisherId
                //Xử lý Create khi người dùng thêm mới
                TblPublisher newPublisher = new TblPublisher
                {
                    PublisherName = txtPublisherName.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                };

                // Gọi phương thức BLL để thêm người dùng mới
                TblPublisher insertedPublisher = PublishersManager.InsertPublisher(newPublisher);

                if (insertedPublisher != null)
                {
                    // Lưu publisherId vào session
                    Session["NewPublisherId"] = insertedPublisher.Id;
                    iValidPublisherName.Visible = false;
                    iValidAddress.Visible = false;
                    iValidPhone.Visible = false;

                    // Chuyển hướng
                    LoadData(insertedPublisher.Id);
                    //Response.Redirect("PublisherDetail.aspx?publisherId=" + insertedPublisher.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'PublisherDetail.aspx?publisherId=" + insertedPublisher.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("PublisherDetail.aspx?Id=" + insertedPublisher.Id);
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
            Response.Redirect("Publishers.aspx");
        }
        public void LoadData(Guid publisherId)
        {
            TblPublisher publisher = GetPublisherInfoFromDatabase(publisherId);

            txtPublisherName.Text = publisher.PublisherName;
            txtAddress.Text = publisher.Address;
            txtPhone.Text = publisher.Phone;
        }


        private bool IsPublisherNameExists(string publisherName)
        {
            return PublishersManager.IsPublisherNameExists(publisherName);
        }
    }
}