<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="BookstoreSellingManagement.Books" %>
<asp:Content ID="ctCssBooks" ContentPlaceHolderID="phCss" runat="server">    
    <link href="cms/admin/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="cms/admin/assets/css/admin.style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="ctBooks" ContentPlaceHolderID="phMain" runat="server">
    <!-- Start main content for Books page -->
    <div class="main-content">
        <!-- Start main content for Book page -->
            <div class="page-content">
                <div class="container-fluid">
                    <!-- start page title -->
                    <div class="row">
                        <div class="col-12">
                            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                <h4 class="mb-sm-0 font-size-18">Books Management</h4>
                            </div>
                        </div>
                    </div>
                    <!-- end page title -->
                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header">
                                    <h4 class="card-title">Insert new Book</h4>
                                    <div class="mt-4">
                                        <a href="BookDetail.aspx" class="btn btn-primary w-md">Create</a>
                                    </div>
                                </div>
                                <div class="card-body">        
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvBooks" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvBooks_PageIndexChanging" AllowSorting="true" OnSorting="gvBooks_Sorting" class="table table-editable table-nowrap align-middle table-edits" runat="server" AutoGenerateColumns="False" OnRowCommand="gvBooks_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="BookTitle" HeaderText="Book Title" SortExpression="BookTitle"/>
                                                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price"/>
                                                <asp:BoundField DataField="BookDescription" HeaderText="Book Description" SortExpression="BookDescription"/>
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" /> 
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton class="btn btn-outline-secondary btn-sm edit" runat="server" CommandName="EditBook" CommandArgument='<%# Eval("Id") %>'>
                                                                <i class="fas fa-pencil-alt"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>                                                        
                                                        <asp:LinkButton class="btn btn-outline-secondary btn-sm edit" runat="server" CommandName="DeleteBook" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirmDelete();">
                                                            <i class="fas fa-trash-alt"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>       
                                    </div>
                                </div>
                            </div>
                        </div> <!-- end col -->
                    </div> <!-- end row -->
                <!-- Pop Up alert -->
                <div class="card-body">        
                <%--<button type="button" class="btn btn-primary" id="liveToastBtn">Show live toast</button>--%>
                    <div class="position-fixed bottom-0 end-0 p-3"  style="z-index: 11">
                        <div id="liveToast" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="1000" data-bs-autohide="true" runat="server">
                            <div class="toast-header text-white bg-success">
                                <img src="cms/admin/assets/images/logo-sm.svg" alt="" class="me-2" height="18">
                                <strong class="me-auto">Hamin Bookstore</strong>
                                <small class="text-white" >now</small>
                                <button type="button" class="btn-close" data-bs-dismiss="toast"
                                    aria-label="Close"></button>
                            </div>
                            <div class="toast-body text-success">
                                Book has been deleted successfully.
                            </div>
                        </div>
                    </div>
                </div>  
            </div> <!-- container-fluid -->
            </div>
        <!-- End Page-content -->
    </div>
    <!-- End main content for Books page -->
</asp:Content>
<asp:Content ID="ctJsBooks" ContentPlaceHolderID="phJs" runat="server">
     <!-- Table Editable plugin -->
    <script src="assets/libs/table-edits/build/table-edits.min.js"></script>
    <script src="assets/js/pages/table-editable.int.js"></script> 
    <!-- JAVASCRIPT -->
    <script src="cms/admin/assets/libs/jquery/jquery.min.js"></script>
    <script src="cms/admin/assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Bootstrap Toasts Js -->
    <script src="cms/admin/assets/js/pages/bootstrap-toasts.init.js"></script>
    <!-- Admin Js -->
    <script src="cms/admin/assets/js/AdminScript.js"></script>
</asp:Content>
