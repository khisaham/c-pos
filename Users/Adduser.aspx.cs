using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Adduser : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFname.Focus();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_Users", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
         
            cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
            cmd.Parameters.AddWithValue("@LName", txtLName.Text);
            cmd.Parameters.AddWithValue("@UserPhone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmailaddr.Text);
            cmd.Parameters.AddWithValue("@Supervisor", txtSupervisor.Text);
            cmd.Parameters.AddWithValue("@UserName", txtUserID.Text);
            cmd.Parameters.AddWithValue("@Password",txtPassword.Text);
            cmd.Parameters.AddWithValue("@Designation",txtDesignation.Text);            
            cmd.Parameters.AddWithValue("@DateofBirth", txtDOB.Text);
            cmd.Parameters.AddWithValue("@ShopID",txtShopID.Text);
            cmd.Parameters.AddWithValue("@UserAddress", txtAddress.Text);
            cmd.Parameters.AddWithValue("@LogBy", Request.Cookies["POSCookies"]["UserID"].ToString());

            cmd.ExecuteNonQuery();
            cn.Close();

           // lblmessage.Text = "Successfully Saved";

            //Move to Customer List Page
            Response.Redirect("~/Users/ManageUsers.aspx");
        }
        catch  (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }
   
}