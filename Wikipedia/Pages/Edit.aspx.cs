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
    public partial class Edit : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected string ErrorMessage
        {
            get;
            private set;
        }
        protected string PageTitleText
        {
            get;
            private set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var getId = Request.QueryString["id"];
                int pageId;

                if (getId != null && int.TryParse(getId, out pageId))
                {
                    String queryString = "SELECT * FROM [Pages] INNER JOIN [PagesContent] ON Pages.Id = PagesContent.PageId WHERE Pages.Id = @PageId;";
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
                                    //pageTitle.InnerText = reader["Title"].ToString();
                                    //pageContent.InnerText = reader["Content"].ToString();
                                    PageTitle.Text = reader["Title"].ToString();
                                    PageTitleText = reader["Title"].ToString();
                                    Tags.Text = reader["Tags"].ToString();
                                    PageContent.InnerText = reader["Content"].ToString();
                                    PageId.Value = getId;
                                    VersionNumber.Value = reader["VersionNumber"].ToString();

                                    UserManager manager = new UserManager();

                                    var user = manager.FindById(User.Identity.GetUserId());
                                    if (!manager.IsInRole(user.Id, "Administrators"))
                                    {
                                        PageTitleGroup.Style.Add("display", "none");
                                        TagsGroup.Style.Add("display", "none");
                                    }
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Create the local login info and link the local account to the user
                UserManager manager = new UserManager();

                var user = manager.FindById(User.Identity.GetUserId());

                /*IdentityResult result = manager.AddPassword(user.Id, password.Text);*/
                //UPDATE [Carte] SET [TITLU] = @TITLU, [EDITURA] = @EDITURA, [AUTOR] = @AUTOR, [NR_PAGINI] = @NR_PAGINI, [DATA_AP] = @DATA_AP WHERE [ID] = @ID
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                String queryStringPage = "UPDATE [Pages] SET [Title] = @Title, [Tags] =  @Tags, [ModifiedBy] = @ModifiedBy, [ModifiedOn] = @ModifiedOn WHERE [Id] = @Id";
                String queryStringPageContent = "UPDATE [PagesContent] SET [Approved] = 0 WHERE [PageId] = @PageId;INSERT INTO [PagesContent] ([EditedBy], [EditedOn], [VersionNumber], [Content], [PageId], [Approved]) VALUES (@EditedBy, @EditedOn, @VersionNumber, @Content, @PageId, @Approved);";
                //

                SqlConnection myConnection = new SqlConnection(connectionString);
                try
                {
                    myConnection.Open();

                    SqlCommand myCommandPage = new SqlCommand(queryStringPage, myConnection);
                    myCommandPage.Parameters.Add(new SqlParameter("Title", PageTitle.Text));
                    myCommandPage.Parameters.Add(new SqlParameter("Tags", Tags.Text));
                    myCommandPage.Parameters.Add(new SqlParameter("ModifiedBy", user.UserName));
                    myCommandPage.Parameters.Add(new SqlParameter("ModifiedOn", DateTime.Now));
                    myCommandPage.Parameters.Add(new SqlParameter("Id", int.Parse(PageId.Value)));

                    myCommandPage.ExecuteNonQuery();
                
                    SqlCommand myCommandPageContent = new SqlCommand(queryStringPageContent, myConnection);
                    myCommandPageContent.Parameters.Add(new SqlParameter("EditedBy", user.UserName));
                    myCommandPageContent.Parameters.Add(new SqlParameter("EditedOn", DateTime.Now));
                    myCommandPageContent.Parameters.Add(new SqlParameter("Content", PageContent.Value));
                    myCommandPageContent.Parameters.Add(new SqlParameter("PageId", int.Parse(PageId.Value) ));
                    myCommandPageContent.Parameters.Add(new SqlParameter("VersionNumber", int.Parse(VersionNumber.Value) + 1));
                    myCommandPageContent.Parameters.Add(new SqlParameter("Approved", 1));

                    myCommandPageContent.ExecuteNonQuery();

                    //Response.Redirect("~/Pages/AllPages.aspx?m=SetPwdSuccess&id=1");
                    Response.Redirect("~/Pages/View.aspx?id=" + int.Parse(PageId.Value));
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
        }
    }
}