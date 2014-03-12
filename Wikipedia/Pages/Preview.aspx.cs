using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wikipedia.Pages
{
    public partial class Preview : System.Web.UI.Page
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
                    HistoryLink.NavigateUrl = "~/Pages/History?id=" + getId;

                    if (getContentId != null && int.TryParse(getContentId, out pageContentId))
                    {
                        String queryString = "SELECT TOP 1 * FROM [Pages] INNER JOIN [PagesContent] ON Pages.Id = PagesContent.PageId WHERE PagesContent.Id = @ContentId";
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                        SqlConnection myConnection = new SqlConnection(connectionString);

                        try
                        {
                            SqlCommand myCommand = new SqlCommand(queryString, myConnection);
                            myCommand.Parameters.Add(new SqlParameter("PageId", pageId));
                            myCommand.Parameters.Add(new SqlParameter("ContentId", pageContentId));

                            myConnection.Open();

                            SqlDataReader reader = myCommand.ExecuteReader();

                            if (reader.HasRows)
                            {
                                using (reader)
                                {
                                    while (reader.Read())
                                    {
                                        pageTitle.InnerText = reader["Title"].ToString() + " - Preview";
                                        pageContent.InnerText = reader["Content"].ToString();
                                        

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
                }
                else
                {
                    Response.Redirect("~/404.html");
                }
            }
        }
    }
}