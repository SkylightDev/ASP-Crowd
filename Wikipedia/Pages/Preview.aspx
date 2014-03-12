<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="Wikipedia.Pages.Preview" %>
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
    
    <asp:HyperLink ID="HistoryLink" runat="server" Text='View History' />
    <br /><br />

    <div id="pageContent" class="markdown" runat="server">

    </div>
</asp:Content>
