<%@ Page Title="Manage Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleManagement.aspx.cs" Inherits="Wikipedia.IdentityAccount.RoleManagement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <hr />

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-success">
                <strong>Success!</strong> <%: SuccessMessage %>.
            </div>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h4>Create Roles</h4>
            <hr />
        
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="AddRole" CssClass="col-md-3 control-label">Role name</asp:Label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="AddRole" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="AddRole"
                            Display="Dynamic" CssClass="text-danger" ErrorMessage="The role name is required." ValidationGroup="AddRolesGroup"/>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" OnClick="AddRoleButton_Click" ValidationGroup="AddRolesGroup" ID="AddRoleButton" Text="Add Role" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <h4>Add user to role</h4>
            <hr />

            <div class="form-horizontal">
                <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="Username" CssClass="col-md-3 control-label">Username</asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="Username" runat="server" DataSourceID="SqlDataSource2" DataTextField="UserName" DataValueField="Id" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [UserName] FROM [AspNetUsers]"></asp:SqlDataSource>
        
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Username"
                            Display="Dynamic" CssClass="text-danger" ErrorMessage="The username is required." ValidationGroup="AddUserRoleGroup" />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="RoleName" CssClass="col-md-3 control-label">Role Name</asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="RoleName" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Id" CssClass="form-control" OnPreRender="RoleName_PreRender"> 
                            
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [Name] FROM [AspNetRoles]"></asp:SqlDataSource>
                
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="RoleName"
                            Display="Dynamic" CssClass="text-danger" ErrorMessage="The role name is required." ValidationGroup="AddUserRoleGroup" />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <asp:Button runat="server" OnClick="AddUserRole_Click" ID="AddUserRole" ValidationGroup="AddUserRoleGroup" Text="Add User to Role" CssClass="btn btn-default" />
                    </div>
                </div>
            </div>
        </div> <!-- end of div col-6 -->
    </div><!-- end of div row -->
    <hr />
    <div class="row">
        <div class="col-md-6">
            <h4>Roles</h4>
            <hr />
            <p>Lorem ipsum dolor sit amet constructerur
                
              
                <asp:GridView ID="RoleList" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" CssClass="table table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    </Columns>
                </asp:GridView>
                
              
            </p>
        </div>
        <div class="col-md-6">
            <h4>Roles assigned to users</h4>
            <hr />
            <p>Lorem ipsum dolor sit amet constructerur<asp:GridView ID="UserRoleList" runat="server" AutoGenerateColumns="False" DataKeyNames="Name,UserName" DataSourceID="SqlDataSource3" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
                    SelectCommand="SELECT AspNetUserRoles.UserId, AspNetUserRoles.RoleId, AspNetRoles.Name, AspNetUsers.UserName FROM AspNetUserRoles INNER JOIN AspNetRoles ON AspNetUserRoles.RoleId = AspNetRoles.Id INNER JOIN AspNetUsers ON AspNetUserRoles.UserId = AspNetUsers.Id"></asp:SqlDataSource>
            </p>
        </div>
    </div>
</asp:Content>