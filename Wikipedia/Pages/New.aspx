<%@ Page Title="New Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="Wikipedia.Pages.New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <hr />
    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-success">
                <strong>Success!</strong> <%: SuccessMessage %>.
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="errorMessage" Visible="false" ViewStateMode="Disabled">
            <div class="alert alert-danger">
                <strong>Error!</strong> <%: ErrorMessage %>.
            </div>
        </asp:PlaceHolder>
       
    </div>
    
    <div class="row">
        <div class="col-sm-6">
            <div class="form-horizontal">
        
                <div class="form-group">
                
                    <div class="col-sm-12">
          
                        <asp:TextBox runat="server" ID="PageTitle" CssClass="form-control" placeholder="Page Title"/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="PageTitle"
                            CssClass="text-danger" ErrorMessage="The title field is required." />
                    </div>
                </div>

                <div class="form-group">
                
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" ID="Tags" CssClass="form-control" data-role="tagsinput" placeholder="Add tags" />
                    
                    </div>
                </div>

                
                <textarea runat="server" class="form-control" cols="20" id="PageContent" name="PageContent" rows="2" tabindex="5" style="height: 481px;" data-role="markdown"></textarea>
                

                <div style="padding-top: 10px;" class="row">
				    <div class="col-sm-1">
					    <h6><a runat="server" href="~/">Cancel</a></h6>
				    </div>
				    <div style="text-align: right;" class="col-sm-11"><!-- Preview" -->
					    <asp:Button runat="server" OnClick="Submit_Click" Text="Save" CssClass="btn btn-default" />
				    </div>
			    </div>
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
