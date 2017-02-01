<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="LPEDetail.aspx.cs" Inherits="Bling.Web.Compliance.LPEDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/LPE.css" rel="stylesheet" type="text/css" />
    <script src="js/LPE.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Loan Price Evaluator</h2>
    <%--<a href="LPE.aspx">Back to List</a>--%>
    <br />
    <br />
    <fieldset >
        Loan Number
        <input id="txtLoanNumber" name="txtLoanNumber" size="15" type="text" />
        <input id="btnLoad" type="button" value="Load Loan" />
        <input id="btnClear" type="button" value="Clear" />
    </fieldset>    

    <div>
    <label class="span-5">Loan Number:</label><div class="" id="LoanNumber">&nbsp;</div>
    <label class="span-5">Linked Loan:</label><div class="" id="LinkedLoan">&nbsp;</div>
    <label class="span-5">Borrower:</label><div class="" id="Borrower">&nbsp;</div>
    <label class="span-5">Loan Type:</label><div class="" id="LoanType">&nbsp;</div>
    <label class="span-5">Loan Amount:</label><div class="" id="LoanAmount">&nbsp;</div>
    <label class="span-5">GEM Loan Fee Charged:</label><div class="" id="GEMLoanFeeCharged">&nbsp;</div>
    <label class="span-5">Loan Origination Fee Charged:</label><div class="" id="LoanOriginationFeeCharged">&nbsp;</div>
    <label class="span-5">Branch Price:</label><div class="" id="LoanOfficerPrice">&nbsp;</div>
    <label class="span-5">Discount Points:</label><div class="" id="BorrowerPaidDiscount">&nbsp;</div>
    <label class="span-5">Lender Credit:</label><div class="" id="LenderCredit">&nbsp;</div>
    <label class="span-5">FICO Score:</label><div class="" id="FICOScore">&nbsp;</div>
    <label class="span-5">Application Date:</label><div class="" id="ApplicationDate">&nbsp;</div>
    <label class="span-5">Locked Date:</label><div class="" id="LockedDate">&nbsp;</div>
    <label class="span-5">No of Borrower:</label><div class="" id="NoOfBorrower">&nbsp;</div>
    <label class="span-5">Program Type:</label><div class="" id="ProgramType">&nbsp;</div>
    <label class="span-5">Transaction Type:</label><div class="" id="TransactionType">&nbsp;</div>
    <br />
    <label class="span-5">Loan Price Variance(%):</label><div class="" id="FinalNetPricePoint">&nbsp;</div>
     <br />
    <div id="EvaluatorMessage">&nbsp;</div><br />
    <div id="Reason">&nbsp;</div><br />
    <div id="ReviewComplete">&nbsp;</div><br />
    <input id="btnChangesAccepted" type="button" value="Changes Accepted" />
    
    </div>
</asp:Content>
