<%@ Page Title="Response" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Response.aspx.cs" Inherits="Wikipedia.Pages.Response" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %> - <%: PageTitleText2 %></h2>
    <hr />
    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-success">
                <strong>Success!</strong> <%: SuccessMessage2 %>.
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-danger">
                <strong>Error!</strong> <%: ErrorMessage2 %>.
            </div>
        </asp:PlaceHolder>
       
    </div>
    
    <div class="row">
        <div class="col-sm-6">
            <div class="form-horizontal">
        
                <div class="form-group">
                
                    <div class="col-sm-12">
          
                        <asp:Label runat="server" ID="PageTitleLabel" />
                        
                    </div>
                </div>
                
                <textarea runat="server" class="form-control" cols="20" id="PageContent" name="PageContent" rows="2" tabindex="5" style="height: 481px;" data-role="markdown"></textarea>
                
                 <asp:LoginView ID="LoginView1" runat="server">
                    
                    <LoggedInTemplate>
                        <div style="padding-top: 10px;" class="row">
				            <div class="col-sm-1">
					            <h6><a runat="server" href="~/">Cancel</a></h6>
				            </div>
				            <div style="text-align: right;" class="col-sm-11"><!-- Preview" -->
					            <asp:Button runat="server" OnClick="Submit_Click" Text="Save" CssClass="btn btn-default"/>
				            </div>
			            </div>
                    </LoggedInTemplate>
                    <AnonymousTemplate>
                        <div class="row" >
                            <div class="col-sm-12">
					            <h3> You must be logged in to post a project! <a href="../Account/Login.aspx">Login</a></h3>
				            </div>
                           
                        </div>
                        <div style="padding-top: 10px;" class="row">
				            <div class="col-sm-1">
					            <h6><a runat="server" href="~/">Cancel</a></h6>
				            </div>
				            <div style="text-align: right;" class="col-sm-11"><!-- Preview" -->
					            <asp:Button runat="server" OnClick="Submit_Click" Text="Save" CssClass="btn btn-default" disabled="disabled"/>
				            </div>
			            </div>
                    </AnonymousTemplate>
                </asp:LoginView>

                
            </div>
        </div>
    
    
        <div class="col-sm-6">
            <div class="panel panel-default">
			    <div id="preview-heading" class="panel-heading">Preview</div>
			    <div id="wmd-preview-0" class="wmd-preview" style="height: 561px; padding: 15px">
				    Lorem ipsum dolor
			    </div>
		    </div>
        </div>
    </div>
</asp:Content>
