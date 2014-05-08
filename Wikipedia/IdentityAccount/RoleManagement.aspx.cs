using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wikipedia;

namespace Wikipedia.IdentityAccount
{
    public partial class RoleManagement : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddRoleButton_Click(object sender, EventArgs e)
        {
            var manager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!manager.RoleExists(AddRole.Text))
            {
                var result = manager.Create(new IdentityRole() { Name = AddRole.Text });
                if (result.Succeeded)
                {
                    SuccessMessage = "Role created successfully";

                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                    RoleList.DataBind();
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
                }
            }
        }

        protected void AddUserRole_Click(object sender, EventArgs e)
        {
            var manager = new UserManager();
            var user = manager.FindByName(Username.SelectedItem.Text);

            if (user == null)
            {
                ModelState.AddModelError("", "No user found");
                return;
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //var role = roleManager.FindByName(RoleName.Text);

            // if (role == null)

            if (!RoleName.SelectedItem.Value.Equals("0"))
            {
                if (!roleManager.RoleExists(RoleName.SelectedItem.Text))
                {
                    ModelState.AddModelError("", "No role found");
                    return;
                }
            }

            if (manager.GetRoles(user.Id).Count != 0)
            {
                string oldrole = manager.GetRoles(user.Id)[0];
                manager.RemoveFromRole(user.Id, oldrole);
            }

            if (!RoleName.SelectedItem.Value.Equals("0"))
            {
                var result = manager.AddToRole(user.Id, RoleName.SelectedItem.Text);

                if (result.Succeeded)
                {
                    SuccessMessage = "Role assigned to user";

                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                    UserRoleList.DataBind();
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
                }
            }
            else
            {
                SuccessMessage = "Role assigned to user";
                successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                UserRoleList.DataBind();
            }
        }

        protected void RoleName_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RoleName.Items.Insert(0, new ListItem("None", "0"));
            }
        }

    }
}