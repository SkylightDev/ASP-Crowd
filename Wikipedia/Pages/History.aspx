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
                 <div class="media">
              <a class="pull-left" href="#">
                <img class="media-object" src="../img/article-placeholder.gif">
              </a>
              &nbsp;&nbsp;&nbsp;<div class="media-body" style="float:left">
                <h4 class="media-heading"><asp:HyperLink ID="ContentPreview" runat="server"></asp:HyperLink></h4>
                <p><%# Eval("EditedOn") %> -- Version <%# Eval("VersionNumber") %></p>
              </div>
              
              <div style="float:right">


             
                
                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            <div class="btn-group" runat="server" Visible='<%# Eval("Approved").ToString().Equals("0") %>'>
                                <asp:HyperLink ID="ApprovePage" runat="server" Text='Approve' cssClass="btn btn-mini btn-primary" />
                            </div>
                            
                            <div class="edit" runat="server" Visible='<%# Eval("Approved").ToString().Equals("1") %>'>
                                Approved
                            </div>
                        </LoggedInTemplate>    
                    </asp:LoginView>
                </div>
                <div style="clear:both"></div>
            </div>
            </ItemTemplate>
            <LayoutTemplate>
                <div id="itemPlaceholderContainer" class="article-placeholder" runat="server">
                
                 
                    <div id="itemPlaceholder" runat="server">
                    </div>
                
            </div>
            </LayoutTemplate>
        </asp:ListView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Id], [Content], [VersionNumber], [Approved], [EditedOn], [EditedBy] FROM [PagesContent] WHERE [PageId] = @PageId ORDER BY [VersionNumber] DESC">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="" Name="PageId" QueryStringField="id" Type="String"/>
            </SelectParameters>
        </asp:SqlDataSource>
       
    </div>
   
</asp:Content>
