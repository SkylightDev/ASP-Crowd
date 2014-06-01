<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllPages.aspx.cs" Inherits="Wikipedia.Pages.AllPages" %>

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
            <div class="media">
              <a class="pull-left" href="#">
                <img class="media-object" src="../img/article-placeholder.gif">
              </a>
              &nbsp;&nbsp;&nbsp;<div class="media-body" style="float:left">
                <h4 class="media-heading"><asp:HyperLink ID="pageTitle" runat="server" Text='<%# Eval("Title") %>' NavigateUrl='<%# "~/Pages/View?id="+ Eval("Id") %>' /></h4>
                <p>Article description</p>
              </div>
              
              <div style="float:right">
                
                <asp:LoginView ID="LoginView1" runat="server">
                    <RoleGroups>
                        <asp:RoleGroup Roles="Administrators">
                            <ContentTemplate>

                                <div class="btn-group">
                                    <asp:HyperLink ID="pageTitle" runat="server" Text='Edit' NavigateUrl='<%# "~/Pages/Edit?id="+ Eval("Id") %>' cssClass="btn btn-sm btn-default" />
                                    <asp:HyperLink ID="deletePage" runat="server" Text='Delete' NavigateUrl='<%# "~/Pages/AllPages?delete=1&id="+ Eval("Id") %>' cssClass="btn btn-sm btn-danger" />
                                  
                                </div>
                                
                            </ContentTemplate>
                        </asp:RoleGroup>
                    </RoleGroups> 
                    <LoggedInTemplate>
                        <div class="edit">
                            <!--<a runat="server" class="btn btn-mini btn-primary" href="~/Pages/Edit?">Edit</a>-->
                            
                            <div class="btn-group">
                                <asp:HyperLink ID="pageTitle" runat="server" Text='Edit' NavigateUrl='<%# "~/Pages/Edit?id="+ Eval("Id") %>' cssClass="btn btn-sm btn-primary" />
                            </div>
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
