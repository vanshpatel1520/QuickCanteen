﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6thsem_project.MAdmin
{
    public partial class MAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lblLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("../User/Login.aspx");
        }
    }
}