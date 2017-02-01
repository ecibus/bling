<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CSVExportForm.aspx.cs" Inherits="Bling.Web.Processing.CSVExportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/CSVExportForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container showgrid1 inline2">

        <h1>CSV Export</h1>

        <div class="span-24 last">
            <label class="span-5">Report:</label>
            <span class="span-5 last">
                <select id="optReportType">
                    <option value="xReport_PreFunding|PreFund">Prefund Report</option>
                </select>
                
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">From:</label>
            <span class="span-5 last">
                <input id="txtFrom" value="<%=From %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">To:</label>
            <span class="span-5 last">
                <input id="txtTo" value="<%=To %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
                                                  
            </span>
        </div>
        
        <br /><br /><br /><br />

    <div id="csvLink"></div>
</asp:Content>
