<%@ Page Title="All Pages" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllPages.aspx.cs" Inherits="Wikipedia.Pages.AllPages" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%: Title %>
    </h2>
    <hr />
    <div>
        <asp:PlaceHolder runat="server" ID="errorMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-danger">
                <strong>Error!</strong> <%: ErrorMessage %>.
            </div>
        </asp:PlaceHolder>
       
    </div>
    
<!--
<td class="pagename stretch"><a href="/wiki/2014/meu-teste-e-aee-brazuka-o-teu-teste-agora-ficou-meu"> Meu Teste - e aee brazuka.. o teu teste? agora ficou meu</a></td>
-->		

    
    

    
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1">
        
        <ItemTemplate>
            <tr style="">
                <td class="stretch">
        
                    <asp:HyperLink ID="pageTitle" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# "~/Pages/View?id="+ Eval("Id") %>' />
                </td>
                
                <asp:LoginView ID="LoginView1" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="Administrators">
                            <ContentTemplate>
                                <td class="edit">
                                    <!--<a runat="server" class="btn btn-mini btn-primary" href="~/Pages/Edit?">Edit</a>-->
                                    <asp:HyperLink ID="pageTitle" runat="server" Text='Edit' NavigateUrl='<%# "~/Pages/Edit?id="+ Eval("Id") %>' cssClass="btn btn-mini btn-primary" />
                            
                                </td>
                                <td class="edit">
                                    <asp:HyperLink ID="deletePage" runat="server" Text='Delete' NavigateUrl='<%# "~/Pages/AllPages?delete=1&id="+ Eval("Id") %>' cssClass="btn btn-mini btn-danger" />
                                </td>
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups> 
                    <LoggedInTemplate>
                        <td class="edit">
                            <!--<a runat="server" class="btn btn-mini btn-primary" href="~/Pages/Edit?">Edit</a>-->
                            <asp:HyperLink ID="pageTitle" runat="server" Text='Edit' NavigateUrl='<%# "~/Pages/Edit?id="+ Eval("Id") %>' cssClass="btn btn-mini btn-primary" />
                            
                        </td>
                        <td></td>
                    </LoggedInTemplate>   
                    
                </asp:LoginView>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="itemPlaceholderContainer" runat="server" class="table table-striped">
                
                    <tr runat="server" style="">
                        <th runat="server">Title</th>
                        <th></th>
                        <th></th>
                    </tr>
                
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                
            </table>
        </LayoutTemplate>
       
    </asp:ListView>
    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="5">
        <Fields>
            
            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
        </Fields>
    </asp:DataPager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" SelectCommand="SELECT [Title], [Id] FROM [Pages] WHERE [Deleted]=0 AND [Tags] LIKE '%' + @Tag + '%' ">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="%%" Name="Tag" QueryStringField="tag" Type="String"/>
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
