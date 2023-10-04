<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryDetail.aspx.cs" Inherits="BookstoreSellingManagement.CategoryDetail" %>
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
                            <h4 class="mb-sm-0 font-size-18">Category Detail</h4>
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
                                            <label for="categoryImage-input" class="form-label">Category Image</label>                                                                            
                                        </div>
                                        <div class="mb-3">
                                            <div class="categoryImage-container">
                                                <asp:Image ID="imgCategoryImage" runat="server" class="categoryImage-preview" Visible="false"/>
                                            </div>
                                        </div>
                                            <div class="mb-3">
                                            <asp:FileUpload ID="fileCategoryImage"  runat="server" />                                              
                                        </div>
                                    </div>
                                        <div class="col-lg-9">      
                                            <asp:UpdatePanel ID="panel" runat="server">
                                                <ContentTemplate>
                                                    <div class="mb-3">
                                                        <label for="category-name-input" class="form-label">Category Name</label>
                                                        <asp:TextBox ID="txtCategoryName" class="form-control" runat="server" placeholder="Enter Category Name" ></asp:TextBox>
                                                        <i runat="server" class="input-notify" id="iValidCategoryName" visible="false" >Please enter Category Name!</i>
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
                                Category has been created successfully.
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
                                Category has been updated successfully.
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
