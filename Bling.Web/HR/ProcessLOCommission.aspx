<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ProcessLOCommission.aspx.cs" Inherits="Bling.Web.HR.ProcessLOCommission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/ProcessLOCommission.js?v=1.0.7" type="text/javascript"></script>    
    <link href="../Styles/ProcessLOCommission.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">
        <h2>Process LO Commission</h2>

        <div class="span-24">
            <span class="span-3">Pay Date:</span>
            <span class="span-5">
                <input id="txtPayDate" type="text" value="<%=PayDate %>" />
                
            </span>            
        </div>
        <div class="span-24">
            <span class="span-3">Funded As Of:</span>
            <span class="span-5">
                <input id="txtFundedAsOf" type="text" value="<%=FundedAsOf %>" />
                <input id="btnCompute" type="button" value="Compute Commission" />                
            </span>            
        </div>

        <br /><br />
        <div class="span-24 status">            
            <span id="msgCompute"></span>
        </div>

        <br /><br />
        <hr class="prepend-top" />
        <h2>Report</h2>

        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">                
                <input id="isWeekly" type="radio" name="PaySchedule" checked="checked" />Weekly
                <input id="IsSalary" type="radio" name="PaySchedule" />Salary
            </span>            
        </div>

        <div class="span-24">
            <span class="span-3">Deadline</span>
            <span class="span-15">                
                <input type="text" id="txtDeadline" value="<%=Deadline %>" />
            </span>            
        </div>

        

        <div class="span-24">
            <span class="span-3">Branch</span>
            <span class="span-15">
                <%=BranchHtml%>
                <input id="btnViewCommissionReport" type="button" value="View Commission Report"  class="span-6" />
                <input id="btnEMailCommissionReport" type="button" value="EMail Commission Rpt to Manager"  class="span-6" />                   
            </span>            
        </div>
        <div class="span-24">
            <span class="span-5">&nbsp;</span>
            <span class="span-15">
                <input id="btnViewSummaryReport" type="button" value="View Summary Report" class="span-6" />
                <input id="btnEMailCommissionReportLO" type="button" value="EMail Commission Rpt to LO"  class="span-6" /> 
                <%--<input id="btnEMailCommissionReportLO2" type="button" value="EMail Commission Rpt to LO"  class="span-6" /> --%>
            </span>            
        </div>
        <div class="span-24">
            <span class="span-5">&nbsp;</span>
            <span class="span-15">
                <input id="btnViewOverrideReport" type="button" value="View Override Report" class="span-6" />
                <input type="radio" name="resend" value="0" checked="checked" />Email Fresh Report
                <input type="radio" name="resend" value="1" />Email Missing Report
            </span>            
        </div>
        
        <br /><br /><br />
        <div class="span-24">
            <span class="span-3">Month</span>
            <%=MonthHtml %>
        </div>
        <div class="span-24">
            <span class="span-3">Year</span>
            <%=YearHtml %>
        </div>
        <div class="span-24">
            <span class="span-5">&nbsp;</span>
            <span class="span-15">
                <input id="btnViewISHReport" type="button" value="View Inside Sales Hourly Report"  class="span-6" />
                <input id="btnEMailISHReport" type="button" value="EMail Inside Sales Hourly Report"  class="span-6" />                   
            </span>            
        </div>

        <br /><br />
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">                
                <input type="checkbox" id="chkSendToManager" />When sending email, send it to Manager/Loan Officer.
            </span>            
        </div>
        <br /><br /><br /><br />
        <div class="span-24">
            <span class="span-3">Ending Date</span>
            <span class="span-15">                
                <input type="text" id="txtEndingDate" value="<%=EndingDate %>" />
            </span>            
        </div>
        <div class="span-24">
            <span class="span-5">&nbsp;</span>
            <span class="span-15">
                <input id="btnCA" type="button" value="Commissions Accrual"  class="span-6" />
                <input id="btnCAGLT" type="button" value="Commissions Accrual GL Totals"  class="span-6" />                   
            </span>            
        </div>

    </div>
</asp:Content>
