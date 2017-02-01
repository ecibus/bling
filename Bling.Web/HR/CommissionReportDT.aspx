<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CommissionReportDT.aspx.cs" Inherits="Bling.Web.HR.CommissionReportDT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/CommissionReportDT.js?v=1.0.0.0" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">
        <h2>Commission Report DataTrac</h2>

        <div class="span-24">
            <label class="span-3">Funded From:</label>
            <span class="span-5">
                <input id="txtFundedFrom" type="text" value="<%=FundedFrom %>" />
                
            </span>            
        </div>
        <div class="span-24">
            <label class="span-3">Funded To:</label>
            <span class="span-5">
                <input id="txtFundedTo" type="text" value="<%=FundedTo %>" />
            </span>            
        </div>
        <div class="span-24">
            <label class="span-3">Loan Officer:</label>
            <span class="span-5">
                <%=LODropDown %>
            </span>            
        </div>
        <div class="span-24">
            <label class="span-3">Branch No:</label>
            <span class="span-5">
                <input id="txtBranchNo" class="span-2" type="text" />
            </span>            
        </div>
        
        <div class="span-24">
            <label class="span-3">Report Name:</label>
            <select id="optReport" size="10" class="span-7">
                <option value="crbr">Commission Report by Branch</option>
                <option value="crlo">Commission Report by Loan Officer</option>
            </select>
        </div>

        <div class="span-24">
            <label class="span-3">&nbsp;</label>
            <span class="span-5">
                <input id="btnViewReport" type="button" value="View Report" />
            </span>            
        </div>


    </div>
 </asp:Content>
