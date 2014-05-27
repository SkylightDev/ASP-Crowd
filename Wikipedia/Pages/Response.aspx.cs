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
    public partial class Response : System.Web.UI.Page
    {
        protected string SuccessMessage2
        {
            get;
            private set;
        }
        protected string PageTitleText2
        {
            get;
            private set;
        }
        protected string ErrorMessage2
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
                    String queryString = "SELECT * FROM [Pages] WHERE Pages.Id = @PageId;";
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
                                if (reader.Read())
                                {
                                    string s = reader.GetString(1);
                                   // PageTitle.Text = reader["Title"].ToString();
                                    PageTitleText2 = reader["Title"].ToString();
                                   
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
                        ErrorMessage2 = ex.Message;

                        //errorMessage2.Visible = !String.IsNullOrEmpty(ErrorMessage2);
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
                
               // IdentityResult result = manager.AddPassword(user.Id, password.Text);
                
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                String queryStringPage = "INSERT INTO [Projects_Solutions] ([User_id], [Project_id], [content]) VALUES (@u_id, @p_id, @content);";

                SqlConnection myConnection = new SqlConnection(connectionString);
                try
                {
                    myConnection.Open();

                    SqlCommand myCommandPage = new SqlCommand(queryStringPage, myConnection);
                    myCommandPage.Parameters.Add(new SqlParameter("p_id", Request.QueryString["id"]));
                    myCommandPage.Parameters.Add(new SqlParameter("content", PageContent.Value));
                    myCommandPage.Parameters.Add(new SqlParameter("u_id", user.Id));
                   
                    myCommandPage.ExecuteNonQuery();


                    //Response.Redirect("~/Pages/AllPages.aspx?m=SetPwdSuccess&id=1");
                    Response.Redirect("~/Pages/View.aspx?id=" + Request.QueryString["id"]);
                }
                catch (Exception ex)
                {
                    ErrorMessage2 = ex.Message;

                    errorMessage.Visible = !String.IsNullOrEmpty(ErrorMessage2);
                }
                finally
                {
                    myConnection.Close();
                }
                
            }
        }

    }
}