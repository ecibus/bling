<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="GEMAppraisal.aspx.cs" Inherits="Bling.Web.Processing.GEMAppraisal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/CPP.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>GEM Appraisal Report</h1>
    <fieldset>    
        <label for="ddlDate">Appraisal Date To Search </label> 
        <asp:DropDownList ID="ddlDate" runat="server">
            <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
            <asp:ListItem Value="1">Ordered Date</asp:ListItem>
            <asp:ListItem Value="2">Received Date</asp:ListItem>
        </asp:DropDownList><br />
        <label for="txtFrom">From </label> 
        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><br />
        <label for="txtTo">To </label> 
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox><br />
        <label for="chkGroup">Group by Branch </label> 
        <asp:CheckBox ID="chkGroup" runat="server" Checked="true" /><br />
        
        <label for="btnViewReport">&nbsp;</label>        
        <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
            onclick="btnViewReport_Click" />
        
    </fieldset> 
</asp:Content>
