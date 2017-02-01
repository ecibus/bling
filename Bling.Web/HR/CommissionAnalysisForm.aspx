<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CommissionAnalysisForm.aspx.cs" Inherits="Bling.Web.HR.CommissionAnalysisForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />  
    <link href="../Styles/CommissionAnalysisForm.css" rel="stylesheet" type="text/css" />  
    <script src="js/CommissionAnalysisForm.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">

        <h1>Commission Analysis</h1>
    
        <a href="CommissionAnalysisList.aspx">Loans for Approval</a>
        <br /><br />
        <br />
        <div class="span-24">
            <label class="span-3">Loan Number:</label>
            <input type="text" class="span-3" id="txtLoanNumber" value="<%=LoanNumber %>" />
            <input id="btnLoad" type="button" value="Load" />&nbsp;&nbsp;
            <input id="btnClear" type="button" value="Clear" />
        </div>


        <br /><br />

        

        <div class="span-24 last">
            <label class="span-3">Loan Number:</label>
            <span class="span-3" id="LoanNumber"></span>
        </div>


        <div class="span-24 last">
            <label class="span-3">Borrower:</label>
            <span class="span-5" id="BorrowerName"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Loan Officer:</label>
            <span class="span-3" id="LoanOfficer"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">App Date:</label>
            <span class="span-3" id="ApplicationDate"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Funded Date:</label>
            <span class="span-3" id="FundedDate"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Hold Date:</label>
            <span class="span-3" id="HoldDate"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Released Date:</label>
            <span class="span-3" id="ReleasedDate"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Loan Amount:</label>
            <span class="span-3" id="LoanAmount"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Base Commission:</label>
            <span class="span-3" id="Commission"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Brokered Loan:</label>
            <span class="span-3" id="BrokeredLoan"></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Pay Date:</label>
            <span class="span-3">
                <input id="txtPayDate" type="text" class="span-3"/>
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Status:</label>
            <span class="span-3">
                <select id="Status">
                    <option></option>
                    <option>Approved</option>
                    <option>NoComm</option>
                    <option>OnHold</option>
                </select>
                
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Approved LO:</label>
            <span class="span-3" id="ApprovedLO"></span>
        </div>


        <div class="span-24 last">
            <label class="span-3">Comment:</label>
            <span class="span-10"><%--<input id="Comment1" type="text" class="span-10"/>--%>
                <textarea id="Comment" cols="20" style="width:350px;height:100px;padding:5px;"
                    rows="2"></textarea></span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-10">
                <input id="btnSave" type="button" value="Approved Loan" />
            </span>
        </div>

    </div>
</asp:Content>
