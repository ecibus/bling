<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="EmployeeCount.aspx.cs" Inherits="Bling.Web.HR.EmployeeCount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/EmployeeCount.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Employee Count</h1>
    <fieldset>    
        <label for="txtFrom">Beginning Started Date </label> 
        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><br />
        <label for="txtTo">Ending Started Date </label> 
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox><br />
        
        <label for="btnViewReport">&nbsp;</label>
        <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
            onclick="btnViewReport_Click" />    
    </fieldset> 
</asp:Content>
