<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AuditScoreCardForm.aspx.cs" Inherits="Bling.Web.Compliance.AuditScoreCardForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/AuditScoreCardForm.css" rel="stylesheet" type="text/css" />
    <script src="js/AuditScoreCardForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">
        <h1>Audit Score Card</h1>
        Loan Number : <input id="txtLoanNumber" type="text" value="" />
        <input id="btnLoad" type="button" value="Load" />
        <%--<input id="btnTest" type="button" value="Test" />--%>

        <br /><br />

        <div class="LoanInfo box span-24">

            <div class="span-24">
                <label class="span-3">Loan Number : </label> 
                <span class="span-5" id="LoanNumber">1234567890</span>

                <label class="span-3">Linked Loan # : </label> 
                <span class="span-3" id="LinkedLoanNumber">1234567890</span>

                <label class="span-4">Borrower : </label> 
                <span class="span-5" id="Borrower">TheFirstName, TheLastName</span>
            </div>

            <div class="span-24">
                <label class="span-3">LO Rep : </label> 
                <span class="span-5" id="LORep">Officer, Loan</span>

                <label class="span-3">Processor :</label> 
                <span class="span-3" id="Processor">LastName, FirstName</span>

                <label class="span-4">Status : </label> 
                <span class="span-5" id="Status">Status</span>
            </div>

            <div class="span-24">
                <label class="span-3">Program : </label> 
                <span class="span-5" id="Program">FGA203L</span>

                <label class="span-3">Loan Amount :</label> 
                <span class="span-3" id="LoanAmount">1,234,567.00</span>

                <label class="span-4">Interest Rate : </label> 
                <span class="span-5" id="InterestRate">4.5</span>
            </div>

            <div class="span-24">
                <label class="span-3">Locked : </label> 
                <span class="span-5" id="Locked">11/11/2011</span>

                <label class="span-3">Expires :</label> 
                <span class="span-3" id="Expires">11/11/2011</span>

                <label class="span-4">Days : </label> 
                <span class="span-5" id="Days">30</span>
            </div>

            <div class="span-24">
                <label class="span-3">Initial Auditor : </label> 
                <span class="span-5">
                    <%= InitialAuditorDropdown %>
                
                </span>

                <label class="span-3">Audit Date :</label> 
                <span class="span-3">
                    <input type="text" class="span-3" id="AuditDate"/>
                </span>

                <label class="span-4">Submitted Date :</label> 
                <span class="span-3">
                    <input type="text" class="span-3" id="SubmittedDate"/>
                </span>

            </div>

            <div class="span-24">
                <label class="span-3">&nbsp;</label> 
                <span class="span-5" id="Span1"><input id="btnSaveAuditor" type="button" class="span-9" value="Save Initial Auditor, Audit Date and Submitted Date" /></span>

            </div>
        
        </div>
        
        <%= ScoreHtml %>

    </div>
</asp:Content>
