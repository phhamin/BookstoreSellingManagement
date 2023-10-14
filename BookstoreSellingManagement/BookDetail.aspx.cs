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
    public partial class BookDetail : System.Web.UI.Page
    {
        private TblBook GetBookInfoFromDatabase(Guid bookId)
        {
            // Gọi phương thức BLL để lấy thông tin người dùng dựa vào bookId
            TblBook book = BooksManager.GetBookById(bookId);

            return book;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["bookId"] != null)
                {
                    if (Guid.TryParse(Request.QueryString["bookId"], out Guid bookId))
                    {
                        LoadData(bookId);
                    }
                    else
                    {
                        // Xử lý khi không có bookId (trang thêm mới)

                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Xử lý khi người dùng bỏ trống
            if (txtBookTitle.Text == string.Empty)
            {
                iValidBookTitle.Visible = true;
                txtBookTitle.Focus();
                return;
            }

            //Book Name trùng 
            if (Request.QueryString["bookId"] == null)
            {
                if (IsBookTitleExists(txtBookTitle.Text))
                {
                    iValidBookTitle.Visible = true;
                    iValidBookTitle.InnerHtml = "Book Name already exists. Please choose a different Book Name";
                    txtBookTitle.Text = string.Empty;
                    txtBookTitle.Focus();
                    return;
                }
            }

            //Ngăn không cho bỏ trống 
           
            if (txtPrice.Text == string.Empty)
            {
                iValidPrice.Visible = true;
                txtPrice.Focus();
                return;
            }
            if (txtBookDescription.Text == string.Empty)
            {
                iValidBookDescription.Visible = true;
                txtBookDescription.Focus();
                return;
            }
            if (txtQuantity.Text == string.Empty)
            {
                iValidQuantity.Visible = true;
                txtQuantity.Focus();
                return;
            }
            //Kiểm tra không cho nhập ký tự
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                iValidPrice.Visible = true;
                iValidPrice.InnerHtml = "Invalid Price. Please enter a valid numeric value.";
                txtPrice.Text = string.Empty;
                txtPrice.Focus();
                return;
            }

            int quantity;
            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                iValidQuantity.Visible = true;
                iValidQuantity.InnerHtml = "Invalid Quantity. Please enter a valid integer value.";
                txtQuantity.Text = string.Empty;
                txtQuantity.Focus();
                return;
            }




            //Xử lý Update khi người dùng chọn Edit
            if (Guid.TryParse(Request.QueryString["bookId"], out Guid bookId))
            {
                TblBook bookToUpdate = BooksManager.GetBookById(bookId);

                if (bookToUpdate != null)
                {
                    // Cập nhật thông tin 
                    if (fileBookImage.HasFile)
                    {
                        string fileName = Path.GetFileName(fileBookImage.PostedFile.FileName);

                        string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                        // Lưu file vào thư mục Uploads
                        fileBookImage.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                        // Lưu tên file vào trường BookImage trong CSDL                  
                        bookToUpdate.BookImage = "img/" + fileName;
                    }
                    bookToUpdate.BookTitle = txtBookTitle.Text;
                    bookToUpdate.Price = price;
                    bookToUpdate.BookDescription = txtBookDescription.Text;
                    bookToUpdate.Quantity = quantity;
                    // Gọi phương thức UpdateBook để cập nhật vào cơ sở dữ liệu
                    TblBook updatedBook = BooksManager.UpdateBook(bookToUpdate);

                    if (updatedBook != null)
                    {
                        // Người dùng đã được cập nhật thành công
                        iValidBookTitle.Visible = false;
                        iValidPrice.Visible = false;
                        iValidBookDescription.Visible = false;
                        iValidQuantity.Visible = false;
                        // Hiển thị pop-up thông báo thành công
                        LoadData(bookId);
                        liveToastUpdate.Attributes["class"] = "toast show";
                        //Response.Redirect("BookDetail.aspx");
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
                //Không có bookId
                //Xử lý Create khi người dùng thêm mới
                TblBook newBook = new TblBook
                {
                    BookTitle = txtBookTitle.Text,
                    Price = price,
                    BookDescription = txtBookDescription.Text,
                    Quantity = quantity,
                };
                if (fileBookImage.HasFile)
                {
                    string fileName = Path.GetFileName(fileBookImage.PostedFile.FileName);

                    string uploadPath = Server.MapPath("~/img/"); // Đường dẫn lưu file

                    // Lưu file vào thư mục Uploads
                    fileBookImage.PostedFile.SaveAs(Path.Combine(uploadPath, fileName));

                    // Lưu tên file vào trường BookImage trong CSDL
                    newBook.BookImage = "img/" + fileName;

                }
                // Gọi phương thức BLL để thêm người dùng mới
                TblBook insertedBook = BooksManager.InsertBook(newBook);

                if (insertedBook != null)
                {
                    // Lưu bookId vào session
                    Session["NewBookId"] = insertedBook.Id;
                    iValidBookTitle.Visible = false;
                    iValidPrice.Visible = false;
                    iValidBookDescription.Visible = false;
                    iValidQuantity.Visible = false;

                    // Chuyển hướng
                    LoadData(insertedBook.Id);
                    //Response.Redirect("BookDetail.aspx?bookId=" + insertedBook.Id);

                    // Hiển thị pop-up thông báo thành công
                    liveToastCreate.Attributes["class"] = "toast show";
                    string script = "setTimeout(function() { window.location.href = 'BookDetail.aspx?bookId=" + insertedBook.Id + "'; }, 2000);";
                    ClientScript.RegisterStartupScript(this.GetType(), "RedirectScript", script, true);
                    //if (Request.QueryString["Id"] != null)
                    //{
                    //    Response.Redirect("BookDetail.aspx?Id=" + insertedBook.Id);
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
            Response.Redirect("Books.aspx");
        }

        public void LoadData(Guid bookId)
        {
            TblBook book = GetBookInfoFromDatabase(bookId);
            if (!string.IsNullOrEmpty(book.BookImage))
            {
                imgBookImage.ImageUrl = book.BookImage;
                imgBookImage.Visible = true;
            }
            txtBookTitle.Text = book.BookTitle;
            txtPrice.Text = book.Price.ToString();
            txtBookDescription.Text = book.BookDescription;
            txtQuantity.Text = book.Quantity.ToString();
        }


        private bool IsBookTitleExists(string bookTitle)
        {
            return BooksManager.IsBookTitleExists(bookTitle);
        }
    }
}