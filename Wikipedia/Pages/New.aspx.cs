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
    public partial class New : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Create the local login info and link the local account to the user
                UserManager manager = new UserManager();

                var user = manager.FindById(User.Identity.GetUserId());
                
                /*IdentityResult result = manager.AddPassword(user.Id, password.Text);*/

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                String queryStringPage = "INSERT INTO [Pages] ([Title], [Tags], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [Bounty]) VALUES (@Title, @Tags, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn, @Bounty); SELECT @Id=SCOPE_IDENTITY();";
                String queryStringPageContent = "INSERT INTO [PagesContent] ([EditedBy], [EditedOn], [VersionNumber], [Content], [PageId], [Approved]) VALUES (@EditedBy, @EditedOn, @VersionNumber, @Content, @PageId, @Approved);";
                //

                string money = Bounty.Text;
                SqlConnection myConnection = new SqlConnection(connectionString);
                try
                {
                    myConnection.Open();

                    SqlCommand myCommandPage = new SqlCommand(queryStringPage, myConnection);
                    myCommandPage.Parameters.Add(new SqlParameter("Title", PageTitle.Text));
                    myCommandPage.Parameters.Add(new SqlParameter("Tags", Tags.Text));
                    myCommandPage.Parameters.Add(new SqlParameter("Bounty", Int32.Parse(Bounty.Text)));
                    myCommandPage.Parameters.Add(new SqlParameter("CreatedBy", user.UserName));
                    myCommandPage.Parameters.Add(new SqlParameter("CreatedOn", DateTime.Now));
                    myCommandPage.Parameters.Add(new SqlParameter("ModifiedBy", user.UserName));
                    myCommandPage.Parameters.Add(new SqlParameter("ModifiedOn", DateTime.Now));

                    SqlParameter idParam = new SqlParameter("@Id", SqlDbType.Int, 4);
                    idParam.Direction = ParameterDirection.Output;
                    myCommandPage.Parameters.Add(idParam);

                    myCommandPage.ExecuteNonQuery();

                    int myNewId = Convert.ToInt32(idParam.Value);

                    SqlCommand myCommandPageContent = new SqlCommand(queryStringPageContent, myConnection);
                    myCommandPageContent.Parameters.Add(new SqlParameter("EditedBy", user.UserName));
                    myCommandPageContent.Parameters.Add(new SqlParameter("EditedOn", DateTime.Now));
                    myCommandPageContent.Parameters.Add(new SqlParameter("Content", PageContent.Value));
                    myCommandPageContent.Parameters.Add(new SqlParameter("PageId", myNewId));
                    myCommandPageContent.Parameters.Add(new SqlParameter("VersionNumber", 1));
                    myCommandPageContent.Parameters.Add(new SqlParameter("Approved", 1));

                    myCommandPageContent.ExecuteNonQuery();
                    
                    //Response.Redirect("~/Pages/AllPages.aspx?m=SetPwdSuccess&id=1");
                    Response.Redirect("~/Pages/View.aspx?id="+ myNewId);
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