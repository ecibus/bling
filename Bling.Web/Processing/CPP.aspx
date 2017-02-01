<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CPP.aspx.cs" Inherits="Bling.Web.Processing.CPP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/CPP.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Centralized Processing Pipeline</h1>
    <fieldset>    
        <label for="ddlDate">Date To Search </label> 
        <asp:DropDownList ID="ddlDate" runat="server">
            <asp:ListItem Value="0">-- Please Select --</asp:ListItem>
            <asp:ListItem Value="1">Opened Date</asp:ListItem>
            <asp:ListItem Value="2">Funded Date</asp:ListItem>
            <asp:ListItem Value="3">Denied Date</asp:ListItem>
            <asp:ListItem Value="4">Cancelled Date</asp:ListItem>
            <asp:ListItem Value="5">File Received</asp:ListItem>
            <asp:ListItem Value="6">Submitted Date</asp:ListItem>
        </asp:DropDownList><br />
        <label for="txtFrom">From </label> 
        <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><br />
        <label for="txtTo">To </label> 
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox><br />
        
        <label for="btnViewReport">&nbsp;</label>        
        <asp:Button ID="btnViewReport" runat="server" Text="View Report" 
            onclick="btnViewReport_Click" />
        
    </fieldset> 
</asp:Content>
