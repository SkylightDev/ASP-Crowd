<%@ Page Title="Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Wikipedia.Pages.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="pageTitle" runat="server"></h2>
    <hr />
    <div>
        <asp:PlaceHolder runat="server" ID="errorMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-danger">
                <strong>Error!</strong> <%: ErrorMessage %>.
            </div>
        </asp:PlaceHolder>
       
    </div>
    
    <asp:LoginView ID="LoginView1" runat="server">
        <RoleGroups>
            <asp:RoleGroup Roles="Administrators">
                <ContentTemplate>
                    <asp:HyperLink ID="HistoryLink" runat="server" Text='View History' />
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
                            
    </asp:LoginView>
    <br /><br />
    <div id="pageContent" class="markdown" runat="server">

    </div>
</asp:Content>
