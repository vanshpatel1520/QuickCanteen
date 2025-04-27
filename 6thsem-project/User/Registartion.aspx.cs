using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace _6thsem_project.User
{
    public partial class Registartion : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null ) /*&& Session["userId"] != null*/
                {
                    getUserDetails();
                    //verify();
                }
                else if (Session["userId"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string actioname = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isvalidToExecute = false;
            int userId = Convert.ToInt32(Request.QueryString["id"]);
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", userId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

            if (fuUserImage.HasFile)
            {
                if (Utils.IsvalidExtension(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/User/" + obj.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isvalidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "please select .jpg, .jpeg, or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isvalidToExecute = false;
                }
            }
            else
            {
                isvalidToExecute = true;
            }

            if (isvalidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actioname = userId == 0 ?
                        "registration is successful! <b> <a href='Login.aspx'> Click here></a> </b> to do login" :
                        "details update successful! <b> <a href='Profile.aspx'> Can check here </a></b> ";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b>" + actioname;
                    lblMsg.CssClass = "alert alert-success";

                    // Update Session["imageUrl"] with the new image URL
                    Session["imageUrl"] = imagePath != string.Empty ? imagePath : "../Images/No_image.png";

                    if (userId != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    Clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> username already exists, try a new one...!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }


        void getUserDetails()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                imgUser.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString())
                    ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgUser.Height = 200; imgUser.Width = 200;
                txtPassword.TextMode = TextBoxMode.SingleLine;
                txtPassword.ReadOnly = true;
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
                txtUsername.ReadOnly = true;
            }
            lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
            btnRegister.Text = "Update";
            lblAlreadyUser.Text = "";
        }


        private void Clear()
        {
           txtName.Text = string.Empty;
           txtUsername.Text = string.Empty;
           txtMobile.Text = string.Empty;
           txtEmail.Text = string.Empty;
           txtPassword.Text = string.Empty;
        }

        //void verify ()
        //{
        //    if (txtOTP.Text == Session["otp"].ToString())
        //    {
        //        Response.Redirect("Default.aspx");
        //        return;
        //    }
        //    else
        //    {
        //        //rvfOTP.Text = "Enterd Otp is inccorrect !";
        //    }
           
        //}

    //    protected void btnOTP_Click(object sender, EventArgs e)
    //    {
    //        Random ran = new Random();
    //        int value = ran.Next(1001, 9999);
    //        string desadder = "91" + txtMobile.Text;
    //        string message = "your Numebr is " + value + "(Sent By : QuickCanteen)";
    //        string message1 = HttpUtility.UrlEncode(message);

    //        try
    //        {
    //            string apiUrl = "https://api.textlocal.in/send";
    //            string apiKey = "NDY2NDU4NjUzNjMzNTU3NDc1NjQzOTY5NDI0Yzc1NGY=";

    //            using (var wb = new WebClient())
    //            {
    //                var data = new NameValueCollection()
    //    {
    //        { "apikey", apiKey },
    //        { "numbers", desadder },
    //        { "message", message1 },
    //        { "sender", "TXTLCL" }
    //    };

    //                byte[] response = wb.UploadValues(apiUrl, "POST", data);
    //                string result = System.Text.Encoding.UTF8.GetString(response);

    //                // Handle the result or perform additional processing
    //                Session["otp"] = value;
    //            }
    //        }
    //        catch (WebException ex)
    //        {
    //            // Handle web-related exceptions
    //            Console.WriteLine($"WebException: {ex.Message}");
    //        }
    //    }
    }
}