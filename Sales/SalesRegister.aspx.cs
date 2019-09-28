using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class SalesRegister : System.Web.UI.Page
{
    List<string> lv = new List<string>();

    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtItemSearch.Focus();
            CategoryDDLDataBind();
          //  ItemsListDataBind(DDLCategory.Text);
            CustomerNameDDLDataBind();
            VatRate();
            BindData(DDLCategory.Text);

            //Add item from item list 
            DataTable table = new DataTable();
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Qty", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Disc%", typeof(string));
            table.Columns.Add("Total", typeof(string));
            Session["value"] = table;           
        }    

    }


    // Vat Rate from Terminal table its vary terminal to terminal Example ON rate 13% QC Rate 14.975
    public void VatRate()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_terminalInfo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopId", Request.Cookies["POSCookies"]["ShopID"].ToString());
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            lblVatRate.Text = dt.Rows[0].ItemArray[4].ToString();
            cn.Close();
        }
        catch
        {
        }
    }
    
    //Datalits Item List
   
    protected void BindData(string category)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_SR");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@category", category);
            con.Open();

            DataList1.DataSource = cmd.ExecuteReader();
            DataList1.DataBind();
            con.Close();
        }
        catch
        {
        }
    }


    //Click add to cart menu
    protected void btn_Goclick(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblId = (Label)item.FindControl("LblID");
        Label LblCode = (Label)item.FindControl("LblCode");
        Label LblItemName = (Label)item.FindControl("LblItemName");
        Label LblQty = (Label)item.FindControl("LblQty");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblDisc = (Label)item.FindControl("LblDisc");
        Label LblTotal = (Label)item.FindControl("LblTotal");
        TextBox txtqty = (TextBox)item.FindControl("txtqty");

        string ID = lblId.Text;
        string Code = LblCode.Text;
        string ItemName = LblItemName.Text;
        string Qty = txtqty.Text; // LblQty.Text;
        decimal QtyStock = Convert.ToDecimal(LblQty.Text);
        string Price = LblPrice.Text;
        string Disc = LblDisc.Text;
        // string Total = LblTotal.Text; 
        decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);


        //Check Item Quantity less than 1 
        if (Convert.ToDecimal(Qty) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Increase Item Qty')", true);
        }
        if (Convert.ToDecimal(Qty) > Convert.ToDecimal(QtyStock))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your given quantity is Greater than Stock Quantity')", true);
        }
        else
        {
            //Code	ItemsName	Available_Qty	Price	Disc%	Total
            DataTable dt = (DataTable)Session["value"];
            dt.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
            grdSelectedItem.DataSource = dt;
            grdSelectedItem.DataBind();

            // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            // lbldisc.Text =  pricetotal - 
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
    }

    //Category Bind on dropdown list
    public void CategoryDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CategoryDDL_SR", cn);
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

    // Customer Name Data bind into dropdown list 
    public void CustomerNameDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers_name", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCustname.DataSource = dt;
            DDLCustname.DataTextField = "Name";
            DDLCustname.DataValueField = "Name";
            DDLCustname.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
        

    ////Fix Row Width 
    //protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        e.Row.Cells[0].Width = 10;
    //        e.Row.Cells[2].Width = 310;           
    //    } 
    //}
 
    protected void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = ((DropDownList)sender).NamingContainer as GridViewRow;
        if (gvr != null)
        {
            decimal price = 0.00M;
            int quantity = 0;
            //We can find all the controls in this row and do operations on them
            var ddlQuantity = gvr.FindControl("ddlQuantity") as DropDownList;
            var LblPrice = gvr.FindControl("LblPrice") as Label;
            var LblTotal = gvr.FindControl("LblTotal") as Label;
            if (ddlQuantity != null && LblPrice != null && LblTotal != null)
            {
                int.TryParse(ddlQuantity.SelectedValue, out quantity);
                decimal.TryParse(LblPrice.Text, out price);

                LblTotal.Text = (price * quantity).ToString();
            }

        }
    }

    //Fix Row Width  and Sum of total cost
   // double pricetotal = 0;
    double total = 0;
    double Disc = 0;
    double Qty = 0;
    protected void grdSelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
            total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            Disc += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
            Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 210;
            e.Row.Cells[6].Font.Bold = true;

            //var ddl = e.Row.FindControl("ddlQuantity") as DropDownList;
            //if (ddl != null)
            //{
            //    ddl.DataSource = new List<string>() {"1", "2", "3", "4" };
            //    ddl.DataBind();
            //}
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lblsubTotal.Text = total.ToString();
            lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
        }
         
    }


    //Click add to cart menu  -- Discard 
    protected void LinkPlus_addtoCart(object sender, EventArgs e)
    {        
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;


        //Check Item Quantity less than 1 
        if (Convert.ToInt32(gvrow.Cells[3].Text) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Increase your Item Quantity')", true);
        }
        else
        {
            //Code	ItemsName	Available_Qty	Price	Disc%	Total
            DataTable dt = (DataTable)Session["value"];
            dt.Rows.Add(gvrow.Cells[1].Text, gvrow.Cells[2].Text, "1", gvrow.Cells[4].Text, gvrow.Cells[5].Text, gvrow.Cells[6].Text);
            grdSelectedItem.DataSource = dt;
            grdSelectedItem.DataBind();


           // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            // lbldisc.Text =  pricetotal - 
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
  
     }

   

    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["value"];
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();

        ////////Show Popup dialogbox /////////////////////// 
        //LinkButton LbDelete;
        //foreach (GridViewRow rowItem in grdSelectedItem.Rows)
        //{
        //    LbDelete = (LinkButton)(rowItem.Cells[0].FindControl("LbDelete"));
        //    LbDelete.OnClientClick = "return confirm('are you sure to delete')";            
        //}

        if (grdSelectedItem.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lblVat.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
        }
        else
        {
           // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
      
        
    }


    //AutoComplete  AutoCompleteExtender  ////////////////////////////////////////////   AutoCompleteExtender
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMDN(string prefixText)
    {

        string constr = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT TOP 5 * from    tbl_Item where ItemCode like '%'+ @Value +'%' or ItemName like '%'+ @Value +'%'  and [Status] = 1", con);

        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][1].ToString() + " " + dt.Rows[i][2].ToString();
            MDN.Add(dt.Rows[i][2].ToString());
            con.Close();
        }
        return MDN;
    }


    ////// Barcode scan search box  
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemBarCodeSearch", cn);
            cmd.Parameters.AddWithValue("@ItemCode", txtItemSearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();


            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dtable = new DataTable();
            dtable.Load(sdr);
           // cmd.ExecuteNonQuery();

            ////grdSelectedItem.DataSource = cmd.ExecuteReader();
            ////grdSelectedItem.EmptyDataText = "No Records Found";
            ////grdSelectedItem.DataBind();
 
                string Code = dtable.Rows[0].ItemArray[0].ToString();
                string itemName = dtable.Rows[0].ItemArray[1].ToString();
                string disc = dtable.Rows[0].ItemArray[3].ToString();
                string Price = dtable.Rows[0].ItemArray[2].ToString();
                string Total = dtable.Rows[0].ItemArray[4].ToString();


                DataTable dt = (DataTable)Session["value"];
                dt.Rows.Add(Code, itemName, "1", Price, disc, Total);
                grdSelectedItem.DataSource = dt;
                grdSelectedItem.DataBind();
                cn.Close();

                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();

                txtItemSearch.Text = string.Empty;
                txtItemSearch.Focus();
              //  btnPayment.Focus();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }    
    }

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
        //Session.Remove("value");
        //DataTable dt = (DataTable)Session["value"];
        //dt.Rows.Remove("value");
        //grdSelectedItem.DataSource = dt;
        //grdSelectedItem.DataBind();

        grdSelectedItem.DataSource = null;        
        grdSelectedItem.DataBind();
        lblsubTotal.Text = "0";
        lblVat.Text = "0";
        lbltotal.Text = "0";
        lblTotalQty.Text = "0";
    }

    // Open Payment Popup window 
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        //LinkButton Linkdetails = sender as LinkButton;
        //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        if (grdSelectedItem.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add at least one item')", true);
          //  lbltotal.Text = "Record is empty";
        }
        else
        {
            txtPaid.Text = string.Empty;
            lblChange.Text = "-";
            lblDue.Text = "-";

            lbltotalpay.Text = lbltotal.Text;
            txtPaid.Focus();
            this.ModalPopupPayment.Show();
        }
    }
    
    //// Paid Amount from customer   - Popup Panel
    protected void txtPaid_TextChanged(object sender, EventArgs e)
    {
        double changeAmt  = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotalpay.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotalpay.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotalpay.Text)),2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotalpay.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        this.ModalPopupPayment.Show();
        bntPay.Focus();
    }

    //Inset Multiple Row in single trXID  - Function method
    protected void SaveSaleItem()
    {
        try
        {             
             for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
             {
                GridViewRow row = grdSelectedItem.Rows[i];

                string Code     = grdSelectedItem.Rows[i].Cells[1].Text;
                string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
                string Qty      = grdSelectedItem.Rows[i].Cells[3].Text;  
                string Price    = grdSelectedItem.Rows[i].Cells[4].Text;  
                string Disc     = grdSelectedItem.Rows[i].Cells[5].Text;                 
                string Total    = grdSelectedItem.Rows[i].Cells[6].Text;  

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_SalesItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code",        Code);
                cmd.Parameters.AddWithValue("@ItemName",    ItemName);
                cmd.Parameters.AddWithValue("@Qty",         Qty);
                cmd.Parameters.AddWithValue("@Price",       Price);
                cmd.Parameters.AddWithValue("@Disc",        Disc);
                cmd.Parameters.AddWithValue("@Total",       Total);

                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;             
                cmd.ExecuteNonQuery();
                cn.Close();

                Session["InvoiceNoOutPut"] = cmd.Parameters["@InvoiceNoOutPut"].Value.ToString();                
             } 
           
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    //Insert One Row sales payment info every one trXID
    protected void SaveSalePaymentInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_SRsalesPayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@SalesQty",    lblTotalQty.Text);
            cmd.Parameters.AddWithValue("@Subtotal",    lblsubTotal.Text);
            cmd.Parameters.AddWithValue("@Vat",         lblVat.Text);
            cmd.Parameters.AddWithValue("@totalpayable",lbltotalpay.Text);
            cmd.Parameters.AddWithValue("@payType",     DDLPaidBy.Text);
            cmd.Parameters.AddWithValue("@paidAmount",  txtPaid.Text);
            cmd.Parameters.AddWithValue("@changeAmount", lblChange.Text);
            cmd.Parameters.AddWithValue("@dueAmount",   lblDue.Text);            
            cmd.Parameters.AddWithValue("@note",        txtNote.Text);

            cmd.Parameters.AddWithValue("@ShopId",      Request.Cookies["POSCookies"]["ShopID"].ToString());
            cmd.Parameters.AddWithValue("@CustID",      lblCustID.Text);
            cmd.Parameters.AddWithValue("@CustName",    DDLCustname.Text);
            cmd.Parameters.AddWithValue("@CustContact", lblCustContact.Text);
            cmd.Parameters.AddWithValue("@ServedBy",    Request.Cookies["POSCookies"]["UserID"].ToString());
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
    

    //END Point - Sales Completed and Move to ----------------- >>>>>>>>>>> Print Page
    protected void bntPay_click(object sender, EventArgs e)
    {
        SaveSaleItem();
        SaveSalePaymentInfo();

        Session["totalPayable"] = lbltotalpay.Text;
        Session["vat"]          = lblVat.Text;
        Session["vatRate"]      = lblVatRate.Text;
        Session["PaidBy"]       = DDLPaidBy.Text;
        Session["PaidAmt"]      = txtPaid.Text;
        Session["ChangeAmt"]    = lblChange.Text;
        Session["DueAmt"]       = lblDue.Text;
        Session["Note"]         = txtNote.Text;

        Session["CustName"]     = DDLCustname.Text;
        Session["CustID"]       = lblCustID.Text;
        Session["Contact"]      = lblCustContact.Text;
        Session["TotalQty"]     = lblTotalQty.Text;
        Session["InvoiceNo"]    = Session["InvoiceNoOutPut"].ToString();
        Session["servedby"]     = Request.Cookies["POSCookies"]["UserID"].ToString();
        Session["ShopID"]       = Request.Cookies["POSCookies"]["ShopID"].ToString();

        DataTable table = new DataTable();

        table.Columns.Add("Code", typeof(string));
        table.Columns.Add("Items", typeof(string));
        table.Columns.Add("Qty", typeof(string));
        table.Columns.Add("Price", typeof(string));
        table.Columns.Add("Disc%", typeof(string));
        table.Columns.Add("Total", typeof(string));

        // Select the all row  from the GridView control
        for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
        {
            string Code = grdSelectedItem.Rows[i].Cells[1].Text;
            string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
            string Qty = grdSelectedItem.Rows[i].Cells[3].Text;
            string Price = grdSelectedItem.Rows[i].Cells[4].Text;
            string Disc = grdSelectedItem.Rows[i].Cells[5].Text;
            string Total = grdSelectedItem.Rows[i].Cells[6].Text;

            table.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
        }       
        table.AcceptChanges();
        Session.Add("Stable", table);

        Response.Redirect("~/Sales/POS_printPage.aspx");
    }


    //Get customer information from customer table 
    protected void DDLCustname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CustomersEvent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@CustName",DDLCustname.Text);

            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            lblCustContact.Text = dt.Rows[0].ItemArray[1].ToString();
            lblCustID.Text =   dt.Rows[0].ItemArray[0].ToString();
            cn.Close();

            this.ModalPopupPayment.Show();
            bntPay.Focus();
        }
        catch
        {
            lblCustContact.Text = "";
            lblCustID.Text = "";             
        }
    }


    //Items filter by category  | Categoy list from store items Table
    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ItemsListDataBind(DDLCategory.Text);
        BindData(DDLCategory.Text);
       
    }

    ////////////////////////////////// Plesae Rate Us  ***** http://codecanyon.net/downloads ///
}