<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdateSettings">
<div class="col-lg-8 col-lg-offset-2"> 
    <div class="panel panel-primary" style="text-align:left" >       
          <div class="panel-body">
           <asp:Label ID="Label8" class="label label-warning" Font-Size="12px" runat="server" Text="POS Settings"></asp:Label>  
           <asp:Label ID="Label7" runat="server" Font-Size="11px" Text="Please Update the information below"></asp:Label>        
              <p></p>
                <br />  
            <div class="col-lg-6">           
                <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Company Name"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtCompanyName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                <asp:TextBox ID="txtCompanyName"  BackColor="#D7FDF9" ToolTip="Please Enter Company Name" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                           
                <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Company Address"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtCompanyAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCompanyAddress"  TextMode="MultiLine"  Height="120px" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Font-Size="12px"  Text="Phone"></asp:Label>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtPhone" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>  
                <asp:TextBox ID="txtPhone" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                  <asp:Label ID="Label5" runat="server" Font-Size="12px"  Text="Email Address"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtEmailAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtEmailAddress" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

            </div>
            <div class="col-lg-6"> 
              
                <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Web Address"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtWebAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtWebAddress" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label10" runat="server" Font-Size="12px"  Text="VAT %"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtVatRate" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtVatRate" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label1" runat="server" Font-Size="12px"  Text="VAT Registration No"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtvatRegiNo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtvatRegiNo" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label6" runat="server" Font-Size="12px"  Text="Footer Message"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtFooterMessage" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                              
                <asp:TextBox ID="txtFooterMessage"  BackColor="#D7FDF9"  TextMode="MultiLine" Font-Size="11px" ToolTip="Please Enter Receipt Footer message" class="form-control" ValidationGroup="vlpg43" runat="server"></asp:TextBox>
              <br />
              <asp:Button ID="btnUpdateSettings" runat="server"  ValidationGroup="vlpg43"  
                  class="btn btn-primary btn-sm" Text="Update Settings"     onclick="btnUpdateSettings_Click" /> 
            </div>
                 <div class="footer"></div> 
          </div>
    </div>
</div>

</asp:Panel>
</asp:Content>

