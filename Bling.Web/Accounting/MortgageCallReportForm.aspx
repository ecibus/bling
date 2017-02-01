<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="MortgageCallReportForm.aspx.cs" Inherits="Bling.Web.Accounting.MortgageCallReportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/MCRForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Mortgage Call Report</h1>

        <div class="span-24 last">
            <label class="span-3">Year:</label>
            <span class="span-5 last">
                <%=YearHtml %>                                   
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">Quarter:</label>
            <span class="span-5 last">
                <%=QuarterHtml %>                                   
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate XML" />
                                                  
            </span>
        </div>

        <br /><br /><br /><br />

        <div id="xmlLink"></div>

    </div>
</asp:Content>
