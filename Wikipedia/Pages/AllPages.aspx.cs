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
    public partial class AllPages : System.Web.UI.Page
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
                var getDelete = Request.QueryString["delete"];
                int pageId;
                int delete;

                if (getId != null && int.TryParse(getId, out pageId))
                {

                    if (getDelete != null && int.TryParse(getDelete, out delete))
                    {
                        // Create the local login info and link the local account to the user
                        UserManager manager = new UserManager();

                        var user = manager.FindById(User.Identity.GetUserId());

                        /*IdentityResult result = manager.AddPassword(user.Id, password.Text);*/
                        //UPDATE [Carte] SET [TITLU] = @TITLU, [EDITURA] = @EDITURA, [AUTOR] = @AUTOR, [NR_PAGINI] = @NR_PAGINI, [DATA_AP] = @DATA_AP WHERE [ID] = @ID
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                        String queryStringPage = "UPDATE [Pages] SET [Deleted] = @Deleted WHERE [Id] = @Id";


                        SqlConnection myConnection = new SqlConnection(connectionString);
                        try
                        {
                            myConnection.Open();

                            SqlCommand myCommandPage = new SqlCommand(queryStringPage, myConnection);
                            myCommandPage.Parameters.Add(new SqlParameter("Deleted", 1));
                            myCommandPage.Parameters.Add(new SqlParameter("Id", pageId));

                            myCommandPage.ExecuteNonQuery();


                            //Response.Redirect("~/Pages/AllPages.aspx?m=SetPwdSuccess&id=1");
                            Response.Redirect("~/Pages/AllPages");
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
}