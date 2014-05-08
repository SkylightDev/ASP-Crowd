using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Wikipedia.Pages
{
    public partial class History : System.Web.UI.Page
    {
        protected string ErrorMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var getId = Request.QueryString["id"];
                var getContentId = Request.QueryString["contentid"];
                int pageId;
                int pageContentId;

                if (getId != null && int.TryParse(getId, out pageId))
                {
                    HistoryLink.NavigateUrl = "~/Pages/View?id=" + getId;

                    if(getContentId != null && int.TryParse(getContentId, out pageContentId))
                    {
                        // Create the local login info and link the local account to the user
                        UserManager manager = new UserManager();

                        var user = manager.FindById(User.Identity.GetUserId());

                        /*IdentityResult result = manager.AddPassword(user.Id, password.Text);*/
                        //UPDATE [Carte] SET [TITLU] = @TITLU, [EDITURA] = @EDITURA, [AUTOR] = @AUTOR, [NR_PAGINI] = @NR_PAGINI, [DATA_AP] = @DATA_AP WHERE [ID] = @ID
                        string connectionString2 = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                        String queryStringPage = "UPDATE [PagesContent] SET [Approved] = 0 WHERE [PageId] = @PageId;UPDATE [PagesContent] SET [Approved] = @Approved WHERE [Id] = @Id";
                        

                        SqlConnection myConnection2 = new SqlConnection(connectionString2);
                        try
                        {
                            myConnection2.Open();

                            SqlCommand myCommandPage = new SqlCommand(queryStringPage, myConnection2);
                            myCommandPage.Parameters.Add(new SqlParameter("Approved", 1));
                            myCommandPage.Parameters.Add(new SqlParameter("PageId", pageId));
                            myCommandPage.Parameters.Add(new SqlParameter("Id", pageContentId));

                            myCommandPage.ExecuteNonQuery();
                
                           
                            //Response.Redirect("~/Pages/AllPages.aspx?m=SetPwdSuccess&id=1");
                            Response.Redirect("~/Pages/History.aspx?id=" + pageId);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;

                            errorMessage.Visible = !String.IsNullOrEmpty(ErrorMessage);
                        }
                        finally
                        {
                            myConnection2.Close();
                        }
                    }

                    String queryString = "SELECT TOP 1 * FROM [Pages] INNER JOIN [PagesContent] ON Pages.Id = PagesContent.PageId WHERE Pages.Id = @PageId AND PagesContent.Approved = 1 ORDER BY PagesContent.VersionNumber DESC;";
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    try
                    {
                        SqlCommand myCommand = new SqlCommand(queryString, myConnection);
                        myCommand.Parameters.Add(new SqlParameter("PageId", pageId));

                        myConnection.Open();

                        SqlDataReader reader = myCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            using (reader)
                            {
                                while (reader.Read())
                                {
                                    pageTitle.InnerText = reader["Title"].ToString() + " - History";
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("~/404.html");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = ex.Message;

                        errorMessage.Visible = !String.IsNullOrEmpty(ErrorMessage);
                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
                else
                {
                    Response.Redirect("~/404.html");
                }
            }
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(e.Item.ItemType == ListViewItemType.DataItem)
            {
                var getId = Request.QueryString["id"];
                int pageId;

                if (getId != null && int.TryParse(getId, out pageId))
                {
                    string Id = DataBinder.Eval(e.Item.DataItem, "Id").ToString();

                    HyperLink ContentPreview = (HyperLink)e.Item.FindControl("ContentPreview");
                    string Content = DataBinder.Eval(e.Item.DataItem, "Content").ToString();
                    if (Content.Length > 50)
                        Content = Content.Substring(0, 50);
                    ContentPreview.Text = Content;
                    ContentPreview.NavigateUrl = "~/Pages/Preview?id=" + getId + "&contentid=" + Id;

                    LoginView LoginView1 = (LoginView)e.Item.FindControl("LoginView1");
                    HyperLink ApprovePage = (HyperLink)LoginView1.FindControl("ApprovePage");
                    
                    ApprovePage.NavigateUrl = "~/Pages/History?id=" + getId + "&contentid=" + Id;
                }
            }
        }
    }
}