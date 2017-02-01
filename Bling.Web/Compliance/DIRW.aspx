<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="DIRW.aspx.cs" Inherits="Bling.Web.Compliance.DIRW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/DIRW.css" rel="stylesheet" type="text/css" />
    <script src="js/DIRW.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Data Integrity Review Worksheet</h2>
    Loan Number : <input id="txtLoanNumber" type="text" />
    <input id="btnLoad" type="button" value="Load" />
    <div id="loaninfo">
        <fieldset>
            <label>Loan Number:</label>
            <span id="LoanNumber"></span><br />        
            <label>Borrower:</label>
            <span id="Borrower"></span><br />
            <label>Status:</label>
            <span id="Status"></span><br />            
            <label>Loan Program:</label>
            <span id="LoanProgram"></span><br />
            <label>Underwriter:</label>
            <span id="Underwriter"></span><br />
            <label>Funder:</label>
            <span id="Funder"></span><br />
            <label>Processor:</label>
            <span id="Processor"></span><br />
            <label>Type of Review:</label>
            <span id="ReviewType"></span><br />
            <label>Reviewer:</label>
            <span id="Reviewer"></span><br />
            <label>Date Reviewed:</label>
            <span id="DateReviewed"></span><br />
            <input id="FileId" type="hidden" />
            <input id="State" type="hidden" />
        </fieldset>
    </div>

    <br />
    <div>        
        <div id="fields">
        </div>
    </div>

    
</asp:Content>
