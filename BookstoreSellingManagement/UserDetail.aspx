<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="BookstoreSellingManagement.UserDetail" %>
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
                            <h4 class="mb-sm-0 font-size-18">User Detail</h4>
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
                                            <label for="avatar-input" class="form-label">Avatar</label>                                                                            
                                        </div>
                                        <div class="mb-3">
                                            <div class="avatar-container">
                                                <asp:Image ID="imgAvatar" runat="server" class="avatar-preview" Visible="false"/>
                                            </div>
                                        </div>
                                            <div class="mb-3">
                                            <asp:FileUpload ID="fileAvatar"  runat="server" />                                              
                                        </div>
                                    </div>
                                    <div class="col-lg-9">      
                                        <asp:UpdatePanel ID="panel" runat="server">
                                            <ContentTemplate>
                                                <div class="mb-3">
                                                    <label for="user-name-input" class="form-label">User Name</label>
                                                    <asp:TextBox ID="txtUserName" class="form-control" runat="server" placeholder="Enter User Name" ></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidUserName" visible="false" >Please enter User Name!</i>
                                                </div>              
                                                <div class="mb-3">
                                                    <label for="password-input" class="form-label">Password</label>
                                                    <asp:TextBox ID="txtPassword" class="form-control" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidPassword" visible="false">Please enter Password!</i>
                                                </div>  
                                                <div class="mb-3">
                                                    <label for="firstname-input" class="form-label">First Name</label>
                                                    <asp:TextBox ID="txtFirstName" class="form-control" runat="server" placeholder="Enter First Name"></asp:TextBox>
                                                    <i runat="server" class="input-notify"  id="iValidFirstName" visible="false">Please enter First Name!</i>
                                                </div>
                                                <div class="mb-3">
                                                    <label for="lastname-input" class="form-label">Last Name</label>
                                                    <asp:TextBox ID="txtLastName" class="form-control" runat="server" placeholder="Enter Last Name"></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidLastName" visible="false">Please enter Last Name!</i>
                                                </div>
                                                <div class="mb-3">
                                                    <label class="form-label">Sex</label>
                                                    <asp:DropDownList ID="drdSex" runat="server" CssClass="form-select">
                                                    <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Other" Value="Other"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>      
                                                <div class="mb-3">
                                                    <label for="birthday-input" class="form-label">Birthday</label>
                                                    <asp:TextBox ID ="txtBirthday" class="form-control" runat="server" placeholder="Enter Birthday" TextMode="Date"></asp:TextBox>
                                                </div>
                                                <div class="mb-3">
                                                    <label for="email-input" class="form-label">Email</label>
                                                    <asp:TextBox ID="txtEmail" class="form-control" runat="server" placeholder="Enter Mail"></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidEmail" visible="false">Please enter Email!</i>
                                                </div>
                                                <div class="mb-3">
                                                    <label for="address-input" class="form-label">Address</label>
                                                    <asp:TextBox ID="txtAddress" class="form-control" runat="server" placeholder="Enter Address"></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidAddress" visible="false">Please enter Address!</i>
                                                </div>
                                                    
                                                <div class="mb-3">
                                                    <label for="phone-input" class="form-label">Phone</label>
                                                    <asp:TextBox ID="txtPhone" class="form-control" runat="server" placeholder="Enter Phone"></asp:TextBox>
                                                    <i runat="server" class="input-notify" id="iValidPhone" visible="false">Please enter Phone!</i>
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
                </div>
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
                                User has been created successfully.
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
                                User has been updated successfully.
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
