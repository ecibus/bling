<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="WarehouseAdvanceRecap.aspx.cs" Inherits="Bling.Web.Accounting.WarehouseAdvanceRecap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/WarehouseAdvanceRecap.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container showgrid1 inline2">

        <h1>Warehouse Advance Recap</h1>

        <div class="span-24 last">
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
            </span>
        </div>

        <br /><br /><br /><br />

        <div id="csvLink"></div>

    </div>
</asp:Content>
