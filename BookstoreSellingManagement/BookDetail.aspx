<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="BookstoreSellingManagement.BookDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCss" runat="server">
    <link href="cms/admin/assets/css/admin.style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phMain" runat="server">
    <!-- Start right Content here -->
    <!-- ============================================================== -->
    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">
                <!-- start page title -->
                <div class="row">
                    <div class="col-12">
                        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                            <h4 class="mb-sm-0 font-size-18">Book Detail</h4>
                        </div>
                    </div>
                </div>
                <!-- end page title -->
                <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body p-4">
                                    <div class="row">      
                                        <div class="col-lg-3 mb-12">
                                            <div class="mb-3">
                                                <label for="bookImage-input" class="form-label">Book Image</label>                                                                            
                                            </div>
                                            <div class="mb-3">
                                                <div class="avatar-container">
                                                    <asp:Image ID="imgBookImage" runat="server" class="book-preview" Visible="false"/>
                                                </div>
                                            </div>
                                                <div class="mb-3">
                                                <asp:FileUpload ID="fileBookImage"  runat="server" />                                              
                                            </div>
                                        </div>
                                        <div class="col-lg-9">      
                                            <asp:UpdatePanel ID="panel" runat="server">
                                                <ContentTemplate>
                                                    <div class="mb-3">
                                                        <label for="book-title-input" class="form-label">Book Title</label>
                                                        <asp:TextBox ID="txtBookTitle" class="form-control" runat="server" placeholder="Enter Book Title" ></asp:TextBox>
                                                        <i runat="server" class="input-notify" id="iValidBookTitle" visible="false" >Please enter Book Title!</i>
                                                    </div>    
                                                    <div class="mb-3">
                                                        <label for="price-input" class="form-label">Price</label>
                                                        <asp:TextBox ID="txtPrice" class="form-control" runat="server" placeholder="Enter Price"></asp:TextBox>
                                                        <i runat="server" class="input-notify" id="iValidPrice" visible="false">Please enter Price!</i>
                                                    </div>
                                                     <div class="mb-3">
                                                        <label for="book-description-input" class="form-label">Book Description</label>
                                                        <asp:TextBox ID="txtBookDescription" class="form-control" runat="server" placeholder="Enter BookDescription"></asp:TextBox>
                                                        <i runat="server" class="input-notify" id="iValidBookDescription" visible="false">Please enter Book Description!</i>
                                                    </div>
                                                    <div class="mb-3">
                                                        <label for="quantity-input" class="form-label">Quantity</label>
                                                        <asp:TextBox ID="txtQuantity" class="form-control" runat="server" placeholder="Enter Quantity"></asp:TextBox>
                                                        <i runat="server" class="input-notify" id="iValidQuantity" visible="false">Please enter Quantity!</i>
                                                    </div>                                                                
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="mt-4">
                                                <asp:Button class="btn btn-primary w-md" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />                                                         
                                                <asp:Button class="btn btn-secondary w-md" ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"/>
                                            </div>   
                                        </div>    
                                    </div>               
                            </div>
                        </div> <!-- end col -->
                    </div>
                <!-- end row -->
                <!-- Pop Up alert Create   -->
                <div class="card-body">
                    <div class="position-fixed bottom-0 end-0 p-3"  style="z-index: 11">
                        <div id="liveToastCreate" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="1000" data-bs-autohide="true" runat="server">
                            <div class="toast-header text-white bg-success">
                                <img src="cms/admin/assets/images/logo-sm.svg" alt="" class="me-2" height="18">
                                <strong class="me-auto">Hamin Bookstore</strong>
                                <small class="text-white" >now</small>
                                <button type="button" class="btn-close" data-bs-dismiss="toast"
                                    aria-label="Close"></button>
                            </div>
                            <div class="toast-body text-success">
                                Book has been created successfully.
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Pop Up alert Update-->
                <div class="card-body">
                    <div class="position-fixed bottom-0 end-0 p-3"  style="z-index: 11">
                        <div id="liveToastUpdate" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="1000" data-bs-autohide="true" runat="server">
                            <div class="toast-header text-white bg-success">
                                <img src="cms/admin/assets/images/logo-sm.svg" alt="" class="me-2" height="18">
                                <strong class="me-auto">Hamin Bookstore</strong>
                                <small class="text-white" >now</small>
                                <button type="button" class="btn-close" data-bs-dismiss="toast"
                                    aria-label="Close"></button>
                            </div>
                            <div class="toast-body text-success">
                                Book has been updated successfully.
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </div> <!-- container-fluid -->
        </div>
    <!-- End Page-content -->
    </div>
    <!-- end main content-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phJs" runat="server">
    <!-- Bootstrap Toasts Js -->
    <script src="cms/admin/assets/js/pages/bootstrap-toasts.init.js"></script>
</asp:Content>
