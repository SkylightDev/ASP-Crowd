using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wikipedia.Pages
{
    public partial class View : System.Web.UI.Page
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
                int pageId;

                if (getId != null && int.TryParse(getId, out pageId))
                {
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
                                    pageTitle.InnerText = reader["Title"].ToString();
                                    pageContent.InnerText = reader["Content"].ToString();
                                    HyperLink HistoryLink = (HyperLink) LoginView1.FindControl("HistoryLink");
                                    if(HistoryLink != null)
                                    HistoryLink.NavigateUrl = "~/Pages/History?id=" + getId;

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
    }
}