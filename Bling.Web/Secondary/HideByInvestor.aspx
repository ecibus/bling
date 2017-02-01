<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="HideByInvestor.aspx.cs" Inherits="Bling.Web.Secondary.HideByInvestor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/HideByInvestor.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Show/Hide Program by Investor</h1>
    <p>
        Investors <%=m_InvestorDropdown%>
        <input id="btnShow" type="button" value="Show" />
        <input id="btnHide" type="button" value="Hide" />
    </p>
    <p>Investor Summary Table:</p>
    <p>
        <input id="Radio1" type="radio" name="radio" value="1" checked="checked" />All
        <input id="Radio2" type="radio" name="radio" value="2" />Displayed
        <input id="Radio3" type="radio" name="radio" value="3" />Hidden
    </p>
    
    <div id="summary"><%=m_Summary %></div>
    
</asp:Content>
