<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="LPE.aspx.cs" Inherits="Bling.Web.Compliance.LPE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/LPE.css" rel="stylesheet" type="text/css" />
    <script src="js/LPE.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h2>Loan Price Evaluator List</h2>
     <br />
     <% =ReadyForDocsTable %>
</asp:Content>
