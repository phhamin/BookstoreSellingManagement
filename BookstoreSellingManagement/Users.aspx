<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BookstoreSellingManagement.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCss" runat="server">
    <link href="cms/admin/assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="cms/admin/assets/css/admin.style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phMain" runat="server">
        <!-- Start main content for Users page -->
        <div class="main-content">
        <!-- Start main content for Books page -->
            <div class="page-content">
                <div class="container-fluid">

                    <!-- start page title -->
                    <div class="row">
                        <div class="col-12">
                            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                <h4 class="mb-sm-0 font-size-18">Users Management</h4>
                            </div>
                        </div>
                    </div>
                    <!-- end page title -->

                    <div class="row">
                        <div class="col-12">
                            <div class="card">
                                <div class="card-header">
                                    <h4 class="card-title">Insert new User</h4>
                                    <div class="mt-4">
                                        <a href="UserDetail.aspx" class="btn btn-primary w-md">Create</a>
                                    </div>
                                </div>
                                <div class="card-body">                             
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUsers" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvUsers_PageIndexChanging" AllowSorting="true" OnSorting="gvUsers_Sorting" class="table table-editable table-nowrap align-middle table-edits" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUsers_RowCommand" OnSelectedIndexChanged="gvUsers_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName"/>
<%--                                                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName">
                                                    <HeaderStyle CssClass="sortable-header" />
                                                    <HeaderTemplate>
                                                        User Name
                                                        <span class="sortable-icon">
                                                            <i class="fa fa-sort"></i>
                                                        </span>
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="sortable-column" />
                                                </asp:BoundField>--%>
                                                <%--<asp:BoundField DataField="UserPassword" HeaderText="Password" />--%>  
                                                <%--<asp:BoundField DataField="Avatar" HeaderText="Avatar" />--%>    
                                                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />   
                                                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />               
                                               <asp:TemplateField HeaderText="Sex" SortExpression="Sex">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSex" runat="server" Text='<%# ConvertSexToString((bool)Eval("Sex")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"/>
                                                <asp:BoundField DataField="UserAddress" HeaderText="Address"/>
                                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton class="btn btn-outline-secondary btn-sm edit" runat="server" CommandName="EditUser" CommandArgument='<%# Eval("Id") %>'>
                                                                <i class="fas fa-pencil-alt"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>                                                        
                                                        <asp:LinkButton class="btn btn-outline-secondary btn-sm edit" runat="server" CommandName="DeleteUser" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirmDelete();">
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
                                    User has been deleted successfully.
                                </div>
                            </div>
                        </div>
                    </div>
                </div> <!-- container-fluid -->
            </div>
        <!-- End Page-content -->

    </div>
    <!-- End main content for Users page -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phJs" runat="server">
    <!-- Table Editable plugin -->
    <script src="assets/libs/table-edits/build/table-edits.min.js"></script>
    <script src="assets/js/pages/table-editable.int.js"></script> 
    <!-- JAVASCRIPT -->
    <script src="cms/admin/assets/libs/jquery/jquery.min.js"></script>
    <script src="cms/admin/assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Bootstrap Toasts Js -->
    <script src="cms/admin/assets/js/pages/bootstrap-toasts.init.js"></script>

    <script src="cms/admin/assets/js/AdminScript.js"></script>



</asp:Content>
