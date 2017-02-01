<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ExpiredLicense.aspx.cs" Inherits="Bling.Web.HR.ExpiredLicense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script src="js/ExpiredLicense.js" type="text/javascript"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <h2>Expired License</h2>
  Deadline: 
  <asp:TextBox ID="txtDeadline" runat="server"></asp:TextBox>
  <asp:Button ID="btnSend" runat="server" Text="Send Email" 
    onclick="btnSend_Click" />
  <br />  <br />
  <%= m_List %>
</asp:Content>
