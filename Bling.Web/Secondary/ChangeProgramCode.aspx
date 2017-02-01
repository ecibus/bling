<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ChangeProgramCode.aspx.cs" Inherits="Bling.Web.Secondary.ChangeProgramCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/ChangeProgramCode.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Change Program Code</h1>
    <fieldset>    
        <label for="txtLoanNumber">Loan Number </label> <input id="txtLoanNumber" type="text" />
        <input id="btnLoad" type="button" value="Load" />
        <br /><br />
        
        <label for="lblLoanNumber">Loan Number: </label> 
        <span id="lblLoanNumber"></span>
        <br />
        
        <label for="lblBorrower">Borrower: </label> 
        <span id="lblBorrower"></span>
        <br />
        
        <label for="lblLoanAmount">Loan Amount: </label> 
        <span id="lblLoanAmount"></span>
        <br />
        
        <label for="lblProgram">Program: </label> 
        <span id="lblProgram"></span>
        <br />
        
        <label for="lblProgramId">Program Id: </label> 
        <span id="lblProgramId"></span>
        <br />
        
        <label for="lblStage">Stage: </label> 
        <span id="lblStage"></span>
        <br />
        
        <label for="lblLockExpiration">Lock Expiration: </label> 
        <span id="lblLockExpiration"></span>
        <br />
        
        <label for="lblLoanOfficer">Loan Officer: </label> 
        <span id="lblLoanOfficer"></span>
        <br />
        
        <label for="lblInvestor">Current Investor: </label> 
        <span id="lblInvestor"></span>
        <br />
        
        <label for="lblDescription">Current Program Description: <br />&nbsp;<br />&nbsp;</label> 
        <span id="lblDescription"></span>
        <br />
        
    </fieldset> 
    
    <br /><br />
    
        <label for="optInvestor">Investor: </label>         
        <select id="optInvestor">
            <option>-- Choose New Investor --</option>
        </select>
        <br />
        
        <label for="optProgramDescription">Program Description: </label> 
        <select id="optProgramDescription">
            <option>-- Choose New Program Description</option>
        </select>

        <br />
        
        <label for="lblDescription">&nbsp; </label> 
        <input id="btnUpdate" type="button" value="Update" />
        <br />
    
    
    <br /><br />
    <div id="ProgramCode"></div>
</asp:Content>
