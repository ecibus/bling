<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="IncomeBreakdown.aspx.cs" Inherits="Bling.Web.Accounting.IncomeBreakdown" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/IncomeBreakdown.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Income Breakdown</h1>
    <asp:Panel ID="Panel1" runat="server">
        Application/Loan Number : 
        <input id="txtLoanNumber" type="text" />
        <input id="btnLoad" type="button" value="Load" /><br /><br />
    </asp:Panel>
    <div id="IncomeBreakdown"></div>
</asp:Content>
