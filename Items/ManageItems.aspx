<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="ManageItems.aspx.cs" Inherits="ManageItems" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-lg-10 col-lg-offset-1"> 
            <div class="well well-sm" >
               <p>
                     <asp:Button ID="btnItems"   CssClass="btn btn-warning btn-xs" ToolTip="Add new Item"  runat="server" Text="Add new Item"   PostBackUrl="~/Items/AddItem.aspx" />  |            
                    <asp:Button ID="btnprint" runat="server" ToolTip="Print Stock Report" Text="Print" class="btn btn-info btn-xs" PostBackUrl="~/Report/StockItemReport.aspx" />
               </p>                

                    <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100" 
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>   
                              
            </div>
        </div> 

<div class="col-lg-10 col-lg-offset-1"> 
        <div class="panel panel-default" > 
            <div class="panel-heading">
          <%--  <span  class="glyphicon glyphicon-list"> </span> Items/Products list --%>
             <div class="input-group">
               <span class="input-group-addon"> <span class="glyphicon glyphicon-search"> </span> </span>
               <asp:TextBox ID="txtsearch" class="form-control"   
                        ToolTip="Search by : Code, Item Name, Purchase Price, Retail Price,Category" 
                        Placeholder="Search" runat="server" AutoPostBack="True" 
                        ontextchanged="txtsearch_TextChanged"></asp:TextBox>
            </div>
        </div>
                <div class="panel-body">
                           
                    <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="350px">
                        <asp:GridView ID="grdItemList" runat="server"   class="table table-striped table-hover" Font-Size="11px">
                          <Columns>
                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="180px">
                                    <ItemTemplate> 
                                      <asp:LinkButton    ID="LinkEdit" runat="server"  Font-Size="15px"   ToolTip="Edit" class="glyphicon glyphicon-edit"  OnClick="LinkEdit_Click" />  |
                                        <asp:LinkButton     ID="linkBarcode" runat="server" ForeColor="Red"  Font-Size="15px"     ToolTip="Create Bar-code" class="glyphicon glyphicon-barcode" OnClick="LinkBarcode_Click"  /> |                                  
                                        <asp:LinkButton ID="btnDelete" runat="server" ForeColor="Red"   Font-Size="15px"   ToolTip="Delete" class="glyphicon glyphicon-trash"  OnClick="LinkItemsDelete_Click"  />
                                       <%-- <asp:Image ID="imgItemPic"  Height="40px" Width="40px"  runat="server"  ImageUrl='<%# Eval("Photo")%>' /> --%>
                                     </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                              </asp:TemplateField>                             
                              </Columns>                           
                                              
                        </asp:GridView>
                    </asp:Panel>
                </div>
         </div>
</div>

<%--<<<<<<<<<<<<<<<<<<<<< --------------- Edit Item Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="MpeEditItemShow" runat="server" TargetControlID="btnShowPopup" 
        PopupControlID="pnlpopupEditItemView"  CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:UpdatePanel ID="UpdatePanelImageUpload" runat="server"  UpdateMode="Conditional">
            <ContentTemplate> 

<asp:Panel ID="pnlpopupEditItemView"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="470px"> 
      <div class="panel-heading" style="text-align:left"> 
          <asp:Label ID="lblItemID" runat="server" Text="Label"></asp:Label>:     
            <asp:Label ID="lblitemName" runat="server" Text="Label"></asp:Label>                             
        </div>
               
    <div class="panel-body" style="text-align:left">        
               
               <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Font-Size="11px" Text=""></asp:Label> 
              <br />
                        <div class="col-lg-6">
                                <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="Item Code"></asp:Label><br />
                                <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtProductCode" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator>                   
                                <asp:TextBox  ReadOnly="true"  ID="txtProductCode"  ValidationGroup="vlpg43" runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="Item Name"></asp:Label><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtItemName"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label4" runat="server"  Font-Size="12px" Text="Purchase Price"></asp:Label> <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtpurchasePrice" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>    
                                <asp:TextBox ID="txtPurchasePrice"   ValidationGroup="vlpg43"  runat="server"></asp:TextBox><br />             

                                <asp:Label ID="Label6" runat="server"  Font-Size="12px" Text="Retail Price"></asp:Label><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtRetailPrice" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtRetailPrice"    ValidationGroup="vlpg43" runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label9" runat="server"  Font-Size="12px" Text="Item Qutantity"></asp:Label><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemQty" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtItemQty"   ToolTip="add Item Qutantity e.g: 10"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                               <br />
                                 
                                <asp:Label ID="Label5" runat="server"  Font-Size="12px" Text="Item Discount Rate"></asp:Label>  <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemDiscRate" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtItemDiscRate"   ToolTip="Disc Rate without % sign" placeholder="Disc Rate without % sign"   ValidationGroup="vlpg43" Text="0.00"  runat="server"></asp:TextBox>
                                <br />
                        

                        </div>
                        <div class="col-lg-6">       

 
                             <asp:Label ID="Label7" runat="server"  Font-Size="12px" Text="Item Category"></asp:Label> <br />
                            <p><asp:DropDownList ID="DDLCategory"    ValidationGroup="vlpg43" runat="server"></asp:DropDownList></p>
                       
                            <asp:Image ID="imgItemPhoto" Height="120px" Width="150px"   runat="server"   /><br /> 
                            .jpg and .png  only 
                            <br />  <asp:FileUpload ID="FUpimg"   class="btn btn-success btn-sm"  runat="server"  Width="190px" />
                            <br />
                            <asp:Label ID="lblmsg" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                                                       
                         
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Please add decimal value e.g: 20.11 or 20" ForeColor="Red"  ControlToValidate="txtpurchasePrice" ValidationGroup="vlpg43" 
                            ValidationExpression="\d{0,9}"></asp:RegularExpressionValidator>  

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ErrorMessage="Please add decimal value" ForeColor="Red"  ControlToValidate="txtRetailPrice" ValidationGroup="vlpg43" 
                            ValidationExpression="\d{0,9}"></asp:RegularExpressionValidator>  

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="Please  add  Item Discount Rate" ForeColor="Red"  ControlToValidate="txtItemDiscRate" ValidationGroup="vlpg43" 
                            Display="Dynamic" ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator>
                        </div> 

    </div>
 
         <%-- <iframe style=" width:1240px; height:440px;" id="IframeUpdateItem" src="" runat="server"></iframe> --%>  

    <div class="panel-footer">     
      <asp:Button ID="btnUpdate" runat="server" class="btn btn-success btn-sm"  Text="Update" onclick="btnUpdate_Click" />                            
        <asp:Button ID="btnClose" class="btn btn-danger btn-sm" runat="server" Text="Close" />
    </div>  
 </asp:Panel> 

   </ContentTemplate>   
                   <Triggers> <asp:PostBackTrigger   ControlID="btnUpdate"/></Triggers>                   
            </asp:UpdatePanel>

 <%--<<<<<<<<<<<<<<<<<<<<<END --------------- Edit Item Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>


 
 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Inactive user  Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopupDeleteItem" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="ModalPopupDeleteItem" runat="server" TargetControlID="btnShowPopupDeleteItem" 
        PopupControlID="pnlpopupDeleteItem"  CancelControlID="btnCloseDeleteItem" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupDeleteItem"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="300px"  DefaultButton="btnDeleteItem"> 
  <asp:Label ID="Label8" runat="server" class="label label-info" Text="Do you want to Delete this Item ?"> </asp:Label>   
  <hr /> 
   <asp:Label ID="lblCodeDeleteItem" runat="server" Text="0"></asp:Label>. 
    <asp:Label ID="lblDeleteItem_CustName" runat="server" Text="0"></asp:Label>  <br />
        <div class="panel-footer"> 
                <asp:Button ID="btnDeleteItem" class="btn btn-success btn-sm" runat="server"   Text="Yes"   onclick="btnDeleteItem_Click"  />
                <asp:Button ID="btnCloseDeleteItem" class="btn btn-danger btn-sm" runat="server" Text="No" />
        </div>
</asp:Panel>


        
 <%-- /////////////////////////// Start /////////////////////  Barcode creator ///////////////////////////////// ///////////////////// Start --%>
          <asp:Button ID="btnShowBarcodePopup" runat="server" style="display:none" />
         
        <atk:ModalPopupExtender ID="ModalPopupBarcodePanel" runat="server" TargetControlID="btnShowBarcodePopup" PopupControlID="popupBarcodePanel"
            CancelControlID="btnBarcodeCancel" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

 <asp:Panel ID="popupBarcodePanel"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="760px" >
                <div class="panel-heading">                     
                    Barcode: <asp:Label ID="lblBarcodeID" Font-Bold="true"   runat="server" Text="0"></asp:Label>              
                   
                 </div>     
                 <div class="panel-body" style="text-align:left">
                <iframe style=" width: 740px; height: 470px;" id="Iframebarcode" src="" runat="server"></iframe> 
                </div>
        <div class="panel-footer">
          <asp:LinkButton ID="btnBarcodeCancel" runat="server"   class="btn btn-danger btn-sm"   >Close</asp:LinkButton> 
        </div>
 </asp:Panel>   
 


</asp:Content>

