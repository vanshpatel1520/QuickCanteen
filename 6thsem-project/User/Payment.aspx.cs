using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _6thsem_project.User
{
    public partial class Payment : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlDataReader dr, dr1;
        SqlTransaction transaction = null;

        string _name = string.Empty; string _cardNo = string.Empty; string _expiryDate = string.Empty;
        string _cvv = string.Empty; string _paymentMode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void lbCardSubmit_Click(object sender, EventArgs e)
        {
            _name = txtName.Text.Trim();
            _cardNo = txtCardNo.Text.Trim();
            _cardNo = string.Format("************{0}", txtCardNo.Text.Trim().Substring(12, 4));
            _expiryDate = txtExpMonth.Text.Trim() + "/" + txtExpYear.Text.Trim();
            _cvv = txtCvv.Text.Trim();
            _paymentMode = "card";

            if (Session["userId"] != null)
            {
                OrderPayment(_name, _cardNo, _expiryDate, _cvv, _paymentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void lbCodSubmit_Click(object sender, EventArgs e)
        {
            _paymentMode = "cod";
            if (Session["userId"] != null)
            {
                OrderPayment(_name, _cardNo, _expiryDate, _cvv, _paymentMode);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        void OrderPayment(string name, string cardNo, string expiryDate, string cvv, string paymentMode)
        {
            int paymentId;
            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
        new DataColumn("OrderNo", typeof(string)),
        new DataColumn("ProductId", typeof(int)),
        new DataColumn("Quantity", typeof(int)),
        new DataColumn("UserId", typeof(int)),
        new DataColumn("Status", typeof(string)),
        new DataColumn("PaymentId", typeof(int)),
        new DataColumn("OrderDate", typeof(DateTime)),
            });

            con = new SqlConnection(Connection.GetConnectionString());
            con.Open();

            #region Sql Transaction
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("Save_Payment2", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@CardNo", cardNo);
            cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
            cmd.Parameters.AddWithValue("@Cvv", cvv);
            cmd.Parameters.AddWithValue("@PaymentMode", paymentMode);
            cmd.Parameters.Add("@InsertedId", SqlDbType.Int);
            cmd.Parameters["@InsertedId"].Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
                paymentId = Convert.ToInt32(cmd.Parameters["@InsertedId"].Value);

                // Generate a common order number for all items in the order
                string orderNumber = Utils.GetuniqueId();

                #region Getting Cart Item's
                cmd = new SqlCommand("Cart_Crud", con, transaction);
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                cmd.CommandType = CommandType.StoredProcedure;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int productId = (int)dr["ProductId"];
                    int quantity = (int)dr["Quantity"];

                    //update quantity
                    UpdateQuantity(productId, quantity, transaction, con);
                    //end

                    //delete cartItem from db
                    DeleteCartItem(productId, transaction, con);
                    //end

                    // Add row to DataTable with common order number
                    dt.Rows.Add(orderNumber, productId, quantity, (int)Session["userId"],
                        "Pending", paymentId, Convert.ToDateTime(DateTime.Now));
                }
                dr.Close();
                #endregion

                #region OrderDetails

                if (dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("Save_Orders2", con, transaction);
                    cmd.Parameters.AddWithValue("@tblOrders2", dt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                #endregion OrderDetails

                transaction.Commit();
                lblMsg.Visible = true;
                lblMsg.Text = "Your items ordered successfully!!!";
                lblMsg.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=Invoice.aspx?id=" + paymentId);
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex1)
                {
                    Response.Write("<script>alert('" + ex1.Message + "');</script>");
                }
            }
            #endregion

            finally
            {
                con.Close();
            }
        }


        void UpdateQuantity(int _productId, int _quantity, SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            int dbQuantity;
            cmd = new SqlCommand("Product_Crud", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@ProductId", _productId);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dr1 = cmd.ExecuteReader();
                while(dr1.Read()) 
                {
                    dbQuantity = (int)dr1["Quantity"];

                    if (dbQuantity > _quantity && dbQuantity > 2)
                    {
                        dbQuantity = dbQuantity - _quantity;
                        cmd = new SqlCommand("Product_Crud", sqlConnection, sqlTransaction);
                        cmd.Parameters.AddWithValue("@Action", "QTYUPDATE");
                        cmd.Parameters.AddWithValue("@Quantity", dbQuantity);
                        cmd.Parameters.AddWithValue("@ProductId", _productId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                dr1.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }
        
        void DeleteCartItem(int _productId, SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            cmd = new SqlCommand("Cart_Crud", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@ProductId", _productId);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}