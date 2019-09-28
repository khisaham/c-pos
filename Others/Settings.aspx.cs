using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Settings : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateBindUpdate();
           // txtFname.Focus();
            
        }
    }

    public void UpdateBindUpdate()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_SettingsUpdate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);

            txtCompanyName.Text = dt.Rows[0].ItemArray[1].ToString();
            txtCompanyAddress.Text = dt.Rows[0].ItemArray[2].ToString();
            txtPhone.Text = dt.Rows[0].ItemArray[3].ToString();
            txtEmailAddress.Text = dt.Rows[0].ItemArray[4].ToString();            
            txtWebAddress.Text   = dt.Rows[0].ItemArray[5].ToString();
            txtVatRate.Text = dt.Rows[0].ItemArray[6].ToString();
            txtvatRegiNo.Text   = dt.Rows[0].ItemArray[7].ToString();
            txtFooterMessage.Text = dt.Rows[0].ItemArray[8].ToString();
            cn.Close();
        }
        catch
        {
        }
    }


    protected void btnUpdateSettings_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyName.Text == string.Empty || txtCompanyAddress.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Update the information below')", true);
            }
            else
            {
                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Update_Settings", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                

                cmd.Parameters.AddWithValue("@CompanyName",     txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@CompanyAddress",  txtCompanyAddress.Text);
                cmd.Parameters.AddWithValue("@Phone",           txtPhone.Text);
                cmd.Parameters.AddWithValue("@EmailAddress",    txtEmailAddress.Text);
                cmd.Parameters.AddWithValue("@WebAddress",      txtWebAddress.Text);
                cmd.Parameters.AddWithValue("@VatRate",         txtVatRate.Text);
                cmd.Parameters.AddWithValue("@VATRegistration", txtvatRegiNo.Text);
                cmd.Parameters.AddWithValue("@Footermsg",       txtFooterMessage.Text);
                cmd.Parameters.AddWithValue("@LastUpdateBy",    Request.Cookies["POSCookies"]["UserID"].ToString());
                
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated')", true);

                UpdateBindUpdate();
            }            
        }

        catch
        {
        }
    }
}