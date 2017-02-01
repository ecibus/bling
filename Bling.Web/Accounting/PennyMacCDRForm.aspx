<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="PennyMacCDRForm.aspx.cs" Inherits="Bling.Web.Accounting.PennyMacCDRForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/PennyMacCDRForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>PennyMac CSV</h1><br />

        Enter Loan Number (One Loan on each line)<br />
        <textarea id="txtLoans" cols="20" rows="20"></textarea>

        <div class="span-24 last">
            <span class="span-12 last">
                <input id="btnGenerateCDR" type="button" value="PennyMac CDR CSV" />&nbsp;&nbsp;
                <input id="btnGenerateEPP" type="button" value="PennyMac EPP to AMB CSV" />
            </span>
        </div>

        <br /><br /><br /><br />

        <div id="csvLink"></div>

    </div>
</asp:Content>
