<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="Wikipedia.Pages.History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="pageTitle" runat="server"></h2>
    <hr />
    <div>
        <asp:PlaceHolder runat="server" ID="errorMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-danger">
                <strong>Error!</strong> <%: ErrorMessage %>.
            </div>
        </asp:PlaceHolder>
        
        <asp:HyperLink ID="HistoryLink" runat="server" Text='View Page' />
        <br /><br />

        <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="ListView1_ItemDataBound">
            <ItemTemplate>
                <tr style="">
                    <td class="stretch">
                       <asp:HyperLink ID="ContentPreview" runat="server"></asp:HyperLink> - <%# Eval("EditedOn") %> -- Version <%# Eval("VersionNumber") %>
                    </td>
                
                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            
                            <td class="edit" runat="server" Visible='<%# Eval("Approved").ToString().Equals("0") %>'>
                                <!--<a runat="server" class="btn btn-mini btn-primary" href="~/Pages/Edit?">Edit</a>-->
                                <asp:HyperLink ID="ApprovePage" runat="server" Text='Approve' cssClass="btn btn-mini btn-primary" />
                            </td>
                            <td class="edit" runat="server" Visible='<%# Eval("Approved").ToString().Equals("1") %>'>
                                Approved
                            </td>
                        </LoggedInTemplate>    
                    </asp:LoginView>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="itemPlaceholderContainer" runat="server" class="table table-striped">
                
                        <tr runat="server" style="">
                            <th runat="server">Title</th>
                            <th></th>
                        </tr>
                
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                
                </table>
            </LayoutTemplate>
        </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [Content], [VersionNumber], [Approved], [EditedOn], [EditedBy] FROM [PagesContent] WHERE [PageId] = @PageId ORDER BY [VersionNumber] DESC">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="PageId" QueryStringField="id" Type="String"/>
            </SelectParameters>
        </asp:SqlDataSource>
       
    </div>
   
</asp:Content>
