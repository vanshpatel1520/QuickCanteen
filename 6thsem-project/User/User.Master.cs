using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6thsem_project.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                form1.Attributes.Add("class","sub_page");
            }
            else 
            {
                form1.Attributes.Remove("class");
                //load the control
                Control SliderUserControl = (Control)Page.LoadControl("SliderUserControl1.ascx");

                //add the control to the panel
                PnlSliderUC.Controls.Add(SliderUserControl);
            }

            if (Session["userId"] != null)
            {
                lblLoginOrLogout.Text = "Logout";
                Utils utils = new Utils();
                Session["cartCount"] = utils.cartCount(Convert.ToInt32(Session["userId"]));
            }
            else
            {
                lblLoginOrLogout.Text = "Login";
                Session["cartCount"] = "0";
            }
        }

        protected void lblLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (Session["userId"] == null )
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        protected void lbRegisterOrProfile_Click(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                lbRegisterOrProfile.ToolTip = "user Profile";
                Response.Redirect("Profile.aspx");
            }
            else
            {
                lbRegisterOrProfile.ToolTip = "user Registration";
                Response.Redirect("Registartion.aspx");
            }
        }
    }
    
}