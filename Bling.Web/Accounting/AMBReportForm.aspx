<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AMBReportForm.aspx.cs" Inherits="Bling.Web.Accounting.AMBReportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/AMBReportForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container showgrid1 inline2">

        <h1>Appraisal Fees Payable Report</h1>


        <div class="span-24 last">
            <label class="span-3">Report:</label>
            <span class="span-5 last">
                <select id="optReportType">
                    <option value="1">Appraisal Balance by Funding Date</option>
                    <option value="2">Appraisal Balance by Application Date</option>
                    <option value="3">Appraisal Balance by Denied/Cancelled Date</option>
                </select>
                
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">From:</label>
            <span class="span-5 last">
                <input id="txtFrom" value="<%=From %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">To:</label>
            <span class="span-5 last">
                <input id="txtTo" value="<%=To %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnViewReport" type="button" value="View Report" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;<br /></label>
            <span class="span-5 last">
                <br /><input id="btnExportReport" type="button" value="Export to CSV" />
            </span>
        </div>

        <br /><br /><br /><br />

        <div id="csvLink"></div>

    </div>
</asp:Content>
