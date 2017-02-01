<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CustomersBankForm.aspx.cs" Inherits="Bling.Web.Funding.CustomersBankForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CustomersBankForm.css" rel="stylesheet" type="text/css" />
    <script src="js/CustomersBank.js" type="text/javascript"></script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">
        <h1>Customers Bank</h1>

        <div class="span-24">
            <label for="txtFrom" class="span-3">Funded From </label> 
            <input type="text" id="txtFrom" class="span-3 fundedDate" value="<%= FundedFrom %>"/><br />
        </div>

        <div class="span-24">
            <label for="txtTo"  class="span-3">Funded To </label> 
            <input type="text" id="txtTo" class="span-3 fundedDate" value="<%= FundedTo %>" /><br />
        </div>

        <div class="span-24">
            <label for="txtBatchNo"  class="span-3">Batch No </label> 
            <input type="text" id="txtBatchNo" class="span-1" /><br />
        </div>

        <div class="span-24">
            <label for="txtBatchNo"  class="span-3">Include Byte?</label> 
            <input type="checkbox" id="chkIncludeByteLoans" />
        </div>
            <br />
            <br />

        <div class="span-24">
            <label for="txtBatchNo"  class="span-3">&nbsp;</label> 
            <i>Obligation No must be 13 to be included in the result.</i>
        </div>
        <br /><br />
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">
                <input id="btnPreview" type="button" value="Preview CSV"  />                   
                <input id="btnGenerate" type="button" value="Generate CSV"  />                   
            </span>            
        </div>
        <br /><br />
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15" id="csvLink">
            </span>            
        </div>
    </div>
</asp:Content>
