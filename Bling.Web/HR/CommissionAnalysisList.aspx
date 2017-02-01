<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CommissionAnalysisList.aspx.cs" Inherits="Bling.Web.HR.CommissionAnalysisList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />  
    <link href="../Styles/CommissionAnalysisForm.css" rel="stylesheet" type="text/css" />  
    <script src="js/CommissionAnalysisList.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">

        <h1>Loans For Approval</h1>

        <div id="list"></div>

    </div>
</asp:Content>
