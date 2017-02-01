<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="DocTrustAcctTransfer.aspx.cs" Inherits="Bling.Web.Accounting.DocTrustAcctTransfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/DocTrustAcctTransfer.js" type="text/javascript"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>DOC Trust Account Transfer</h1>
    <fieldset>    
        <label for="txtDate">Transfer Date </label> 
        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox><br />
        <label for="txtTo">As Of </label> 
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox><br />
        <label for="btnLoad">&nbsp;</label>
        <asp:Button ID="btnStampTransfer" runat="server" Text="Stamp Transfer" 
            onclick="btnStampTransfer_Click" />
        <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
            onclick="btnViewReport_Click" /><br />
        
    </fieldset> 
    <br /><br />
    <h2>Run History</h2>
    <%=m_RunHistory %>
</asp:Content>
