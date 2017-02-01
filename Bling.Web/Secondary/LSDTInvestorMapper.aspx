<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="LSDTInvestorMapper.aspx.cs" Inherits="Bling.Web.Secondary.LSDTINvestorMapper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/LSDTInvestorMapper.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Map Loan Solution Investor</h1>
    <%=m_TableMapping %>
    <br />
    <input id="btnMap" type="button" value="Map" />
    <br />
    <p id="Mapping"></p>
</asp:Content>
