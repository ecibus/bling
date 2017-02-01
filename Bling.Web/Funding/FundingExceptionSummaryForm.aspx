<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="FundingExceptionSummaryForm.aspx.cs" Inherits="Bling.Web.Funding.FundingExceptionSummaryForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/FundingExceptionSummary.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/FundingExceptionSummary.js" type="text/javascript"></script>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">
        <h1>Funding Exception Summary</h1>

        <div class="span-24">
            <label class="span-3">Month</label>
            <%=MonthHtml %>
        </div>
        <div class="span-24">
            <label class="span-3">Year</label>
            <%=YearHtml %>
        </div>
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">
                <input id="btnLoadReport" type="button" value="Load Report"  />                   
            </span>            
        </div>

        <br /> <br />
        <div class="clear" />
        <div id ="list"></div>
    </div>


</asp:Content>
