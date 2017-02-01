<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" 
    CodeBehind="ScoreCard.aspx.cs" Inherits="Bling.Web.Underwriting.ScoreCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="/Styles/ScoreCard.css" rel="stylesheet" type="text/css" />
  <script src="js/ScoreCard.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <img alt="Print Preview" title="Print Preview" id="btnPrintPreview" src="/images/PrintPreview.jpg" />
  <h2>Score Card &nbsp; <img alt="busy" id="busy" src="/images/busy.gif" /></h2>
  Loan Number : <input id="txtLoanNumber" type="text" />
  <input id="btnLoad" type="button" value="Load" />
  <div id="loaninfo">
    <fieldset>
        <label>Loan Number:</label>
        <span id="LoanNumber"></span><br />        
        <label>Borrower:</label>
        <span id="Borrower"></span><br />
        <label>Underwriter:</label>
        <span id="Underwriter"></span><br />
        <label>Loan Officer:</label>
        <span id="LoanOfficer"></span><br />
        <label>Processor:</label>
        <span id="Processor"></span><br />
        <label>203K:</label>
        <span id="203K"></span><br />
        <!--<<label>Perfect:</label>
        span id="Perfect"></span><br />-->
        
    </fieldset>
  </div>
  
  
  
  <%=m_Html %>

    
</asp:Content>

