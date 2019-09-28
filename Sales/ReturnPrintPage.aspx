<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="ReturnPrintPage.aspx.cs" Inherits="ReturnPrintPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
             
            color:Black;
           width: 280px;
          /*  font-family:SimSun; */
        }
    </style>
    <style type="text/css" media="all">
        body 
        {    
            color:#000; 
            font-family: Arial, Helvetica, sans-serif; 
            
        }
        #wrapper 
        {  
            width: 280px;
            margin: 0 auto; 
        } 

        .style2
        {
            width: 100%;
            font-size:10px;
        }

    </style>       
    <script src="../Scripts/PrintPosCopy.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><br />

<div class="col-lg-3 col-lg-offset-4">
 
            <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />
 
          <br />

    <div  id="wrapper" style="text-align:left">       
 
            <table class="style1">
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="lblshopTitle" Font-Size="23px" runat="server" Text="-"  Font-Names="High Tower Text"></asp:Label> <br />
                        <asp:Label ID="lblshopAddress"  Font-Size="11px" runat="server" Text="-"></asp:Label>  <br />
                        <asp:Label ID="lblwebAddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />

                        <asp:Label ID="Label16" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                        <asp:Label ID="lblPhone" Font-Size="11px" runat="server" Text=""></asp:Label>  
                     <br /> <br />  </td>
                </tr>  
                  <tr>
                    <td>                       

                        <asp:Label ID="Label1" Font-Size="11px" runat="server" Text="Date:"></asp:Label>
                        <asp:Label ID="lblDatetime" Font-Size="11px" runat="server" Text=""></asp:Label> <br />                                               
                       
                        <table class="style2">
                            <tr>
                                <td>
                        <asp:Label ID="Label3" runat="server"  Font-Size="11px" Text="VAT REG:"></asp:Label>
                        <asp:Label ID="lblVATRegiNo"  Font-Size="11px" runat="server" Text=""></asp:Label> 
                                </td>
                                <td>

                        <asp:Label ID="Label4" runat="server"  Font-Size="11px" Text="Shop ID:"></asp:Label> 
                        <asp:Label ID="lblShopID" runat="server"  Font-Size="11px" Text=""></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td>

                       
                        <asp:Label ID="Label2" runat="server"  Font-Size="11px" Text="Customer ID:"></asp:Label>
                        <asp:Label ID="lblCustID" runat="server"  Font-Size="11px" Text=""></asp:Label> 
                                </td>
                                <td>

                          <asp:Label ID="Label6"  Font-Size="11px" runat="server" Text="Contact No:"></asp:Label> 
                        <asp:Label ID="lblCustContactNo" Font-Size="11px"  runat="server" Text=""></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td>

                       
                        <asp:Label ID="Label5"  Font-Size="11px" runat="server" Text="Name:"></asp:Label> 
                        <asp:Label ID="lblCustName"  Font-Size="11px" runat="server" Text=""></asp:Label> 
                                </td>
                                <td>

                        <asp:Label ID="Label7" Font-Size="11px"   runat="server" Text="ServedBy:"></asp:Label> 
                        <asp:Label ID="lblServedBy" Font-Size="11px"   runat="server" Text=""></asp:Label> 
                                </td>
                            </tr>
                        </table> <br />
                        
                        <asp:Label ID="Label15" Font-Size="11px"  runat="server" Text="Invoice #"></asp:Label>  
                        <asp:Label ID="lblInvoice" Font-Size="11px"  runat="server" Text="1003"></asp:Label> 
                        <br />                       
                        <asp:GridView ID="grdItemList" Font-Size="11px" Width="100%" runat="server"  
                            OnRowDataBound="grdItemList_RowDataBound" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Horizontal"> </asp:GridView> 
                    </td>
                </tr>

                <tr>
                    <td>
                        <table class="style1">
                           
                            <tr>    
                                <td > <asp:Label ID="Label17"  Font-Size="11px" runat="server" Text="Total Sales Qty:"></asp:Label> <br /></td>
                                <td  style="text-align: right;" class="text-right">  <asp:Label ID="lblTotalQty" Font-Bold="true"  Font-Size="11px" runat="server" Text="0"></asp:Label> <br /> </td>                                                            
                             </tr>
                            <tr>    
                                <td> <asp:Label ID="Label14"  Font-Size="11px" runat="server" Text="Total Amount:"></asp:Label>  </td>
                                <td  class="text-right" style="text-align: right">  <asp:Label ID="lblsubTotal" Font-Bold="true"  Font-Size="11px" runat="server" Text="0"></asp:Label>   </td>                                                            
                            </tr>


                            <tr>
                                 <td style="border-bottom-style: solid; border-bottom-width: thin;" >
                                 <asp:Label   Font-Size="11px" ID="Label13" runat="server" Text="Vat"></asp:Label></td>
                                 <td style="border-bottom-style: solid; border-bottom-width: thin;text-align: right;" class="text-right"><asp:Label Font-Size="11px"  ID="lblvat" runat="server" Text="-"></asp:Label></td>
                            </tr>
                            <tr> 
                                <td ><asp:Label ID="Label9"  Font-Size="12px" runat="server" Text="Net Amount:"></asp:Label>  <br /> <br />     </td>
                                <td style="text-align: right;" class="text-right"> 
                                <asp:Label ID="lbltotalpay"  Font-Size="12px"  Font-Bold="true" runat="server" Text="0"></asp:Label>   <br /> <br />   </td>
                            </tr>
                            <tr>    
                                <td><asp:Label ID="Label8"  Font-Size="11px" runat="server" Text="Pay Type:"></asp:Label>  <br />   </td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblpaidby" Font-Size="11px" runat="server" Text="0"></asp:Label> <br />    </td>                                                            
                            </tr>

                            <tr>
                                <td><asp:Label ID="Label10"  Font-Size="11px" runat="server" Text="Return Amount"></asp:Label> </td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblPaidAmt" Font-Size="11px" runat="server" Text="0"></asp:Label></td>                               
                            </tr>

                            <tr>
                                <td><asp:Label ID="Label11"  Font-Size="11px" runat="server" Text="Changed Amount:"></asp:Label> <br /> <br /></td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblChange" Font-Size="11px" runat="server" Text="0"></asp:Label> <br /> <br /></td>                               
                            </tr>

                             <tr>
                                <td style="border-bottom-style: solid; border-bottom-width: thin;">
                                <asp:Label ID="Label12"  Font-Size="11px" runat="server" Text="Due Amount:"></asp:Label> </td>
                               <td style="border-bottom-style: solid; border-bottom-width: thin;text-align: right">
                                <asp:Label ID="lblDue" Font-Size="11px" runat="server" Text="0"></asp:Label> </td>                               
                            <div style="border-top:1px solid #000; padding-top:10px;"></tr>
                        </table>
                       
                     <span style="text-align:center"> 
                           <center>  <asp:Label  ID="lblFooterMessage"  Font-Underline="true" Font-Size="15px" runat="server" Text="Return"></asp:Label> </center>
                          </span>                 
                        
                  </td>
                </tr>

            </table>
          
     </div>
 </div> <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://codecanyon.net/downloads" >Please Don't Forget to Rate Us</asp:HyperLink>
   
            
</asp:Content>

