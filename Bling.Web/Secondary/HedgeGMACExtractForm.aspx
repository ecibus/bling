<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="HedgeGMACExtractForm.aspx.cs" Inherits="Bling.Web.Secondary.HedgeGMACExtractForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/HedgeGMACExtractForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>GMAC Hedge Loan Extract</h1>

        <div class="span-24 last">
            <label class="span-3">Funded From:</label>
            <input type="text" class="span-3" id="txtFundedFrom" value="<%=FundedFrom %>" />
        </div>

        <div class="span-24 last">
            <label class="span-3">Funded To:</label>
            <input type="text" class="span-3" id="txtFundedTo" value="<%=FundedTo %>" />
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate" />
                                                  
            </span>
        </div>

        <br /><br />
        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="last" id="linkToFile"></span>
        </div>

    </div>
</asp:Content>
