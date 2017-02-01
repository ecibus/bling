<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="BranchScoreCard.aspx.cs" Inherits="Bling.Web.Underwriting.BranchScoreCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/ScoreCard.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h2>Branch Score Card Report</h2>
<div id="loaninfo">
<fieldset>
    <label>Month:</label><%=m_MonthHtml %><br />
    <label>Year:</label><%=m_YearHtml %><br />
    <label>Branch:</label><%=m_BranchHtml %>
</fieldset>
</div>
</asp:Content>

