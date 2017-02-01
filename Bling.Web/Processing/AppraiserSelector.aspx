<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AppraiserSelector.aspx.cs" Inherits="Bling.Web.Processing.AppraiserSelector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/AppraiserSelector.css" rel="stylesheet" type="text/css" />
    <script src="js/AppraiserSelector.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Appraiser Selector</h1>
    <fieldset>    
        <label for="LoanNumber">Loan Number:</label> 
        <input id="LoanNumber" type="text" />
        <input id="Load" type="button" value="Load" />
    </fieldset>    
    
    <br /><br />
    <div id="LoanInfo">
        <label>Loan Number:</label> <span id="LoadedLoanNumber">&nbsp;</span><br />
        <label>Borrower:</label> <span id="Borrower">&nbsp;</span><br />
        <label>Loan Type:</label> <span id="LoanType">&nbsp;</span><br />
        <label>Appraiser:</label> <span id="Appraiser" >&nbsp;</span><br />
        
<%--        <label>Ticket No:</label> <input id="TicketNo" type="text" /><br />
        <label>&nbsp;</label><input id="chkUpdateDTAndPoint" type="checkbox" /><span class="updatedtandpoint">Update DataTrac and Point?</span> <br />
--%>        
        <label>&nbsp;</label><span><input id="btnSelectAppraiser" type="button" value="Select Appraiser" /></span>
    </div>
</asp:Content>
