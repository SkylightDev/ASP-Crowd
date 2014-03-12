using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wikipedia.Pages
{
    public partial class Categories : System.Web.UI.Page
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
                string temp;
                ArrayList list = new ArrayList();
                
                String queryString = "SELECT * FROM [Pages];";
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                SqlConnection myConnection = new SqlConnection(connectionString);

                try
                {
                    SqlCommand myCommand = new SqlCommand(queryString, myConnection);
                    
                    myConnection.Open();

                    SqlDataReader reader = myCommand.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            temp = reader["Tags"].ToString();
                            Array tempArray = temp.Split(',');
                            foreach (string s in tempArray)
                            {
                                if (s.Length != 0)
                                    list.Add(s);
                            }
                        }
                    }

                    string tagList = "";
                    Dictionary<string, int> distinctItems = new Dictionary<string, int>();

                    foreach (string item in list)
                    {
                        if (!distinctItems.ContainsKey(item))
                        {
                            distinctItems.Add(item, 0);
                        }
                        distinctItems[item] += 1;
                    }
                    int tagNumber;
                    foreach (KeyValuePair<string, int> distinctItem in distinctItems)
                    {

                        tagNumber = 1;

                        if (distinctItem.Value == 2)
                            tagNumber = 2;
                        if (distinctItem.Value >= 3 && distinctItem.Value <= 5)
                            tagNumber = 3;
                        if (distinctItem.Value > 5 && distinctItem.Value <= 10)
                            tagNumber = 4;
                        if (distinctItem.Value > 10)
                            tagNumber = 5;

                        tagList += "<li class='tagcloud" + tagNumber + "'>" + "<a href='/Pages/AllPages?tag=" + distinctItem.Key.Trim() + "' runat='server' >" + distinctItem.Key + "</a></li>";

                        //System.Diagnostics.Debug.WriteLine("\"{0}\" occurs {1} time(s).", distinctItem.Key, distinctItem.Value);
                    }

                    if (tagList.Length == 0)
                        tagList = "No tags available";

                    tagCloud.InnerHtml = tagList;
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