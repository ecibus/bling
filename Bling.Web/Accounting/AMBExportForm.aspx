<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AMBExportForm.aspx.cs" Inherits="Bling.Web.Accounting.AMBExportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/AMBExportForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container showgrid1 inline2">

        <h1>AMB Borrower Import</h1>

        <div class="span-24 last">
            <label class="span-3">Submitted From:</label>
            <span class="span-5 last">
                <input id="txtFundedFrom" value="<%=FundedFrom %>" type="text" />
                                                
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Submitted To:</label>
            <span class="span-5 last">
                <input id="txtFundedTo" value="<%=FundedTo %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
                                                  
            </span>
        </div>

        <br /><br /><br /><br />

        <div id="csvLink"></div>

    </div>
</asp:Content>
