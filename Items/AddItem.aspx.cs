using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class AddItem : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           txtProductCode.Focus();
           CategoryDDLDataBind();
        }
    }

    public void CategoryDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CategoryDDL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCategory.DataSource = dt;
            DDLCategory.DataTextField = "ItemCategory";
            DDLCategory.DataValueField = "ItemCategory";
            DDLCategory.DataBind();
            cn.Close();

        }
        catch
        {
           // lbtotalRow.Text = "No Records Found";
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Path.GetFileName(FUpimg.PostedFile.FileName);
            string extension = Path.GetExtension(FUpimg.PostedFile.FileName);

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_Items", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@ItemCode", txtProductCode.Text);
            cmd.Parameters.AddWithValue("@ItemName", txtproductName.Text);
            cmd.Parameters.AddWithValue("@PurchasePrice", txtpurchasePrice.Text);
            cmd.Parameters.AddWithValue("@RetailPrice", txtRetailPrice.Text);
            cmd.Parameters.AddWithValue("@ItemQty", txtItemQty.Text);
            cmd.Parameters.AddWithValue("@ItemCategory", DDLCategory.Text);
            cmd.Parameters.AddWithValue("@Discount", txtItemDiscRate.Text);
            cmd.Parameters.AddWithValue("@LogBy",       Request.Cookies["POSCookies"]["UserID"].ToString());
          //  cmd.Parameters.AddWithValue("@Itemphoto", "ItemsPhoto/" + txtProductCode.Text + extension);        
            
            if (FUpimg.HasFile)
            {
                if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                {
                    cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/" + txtProductCode.Text + extension);

                    string strPath = MapPath("ItemsPhoto/") + txtProductCode.Text + extension;
                    FUpimg.SaveAs(strPath);

                    //string uploadFolder = Request.PhysicalApplicationPath + "ItemsPhoto\\";
                    // FUpimg.SaveAs(uploadFolder + txtProductCode.Text + extension);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('.jpg and .Png Format can be support')", true);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/item.png"); //DBNull.Value for Database NULL value
            }

            cmd.ExecuteNonQuery();
            cn.Close();
            lblmessage.Text = "Successfully Saved";            
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
      
    }
}