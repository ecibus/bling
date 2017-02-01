<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="QCExportForm.aspx.cs" Inherits="Bling.Web.Processing.QCExportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/QCExportForm.js?v=1.2" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>QC Export</h1>

        <div class="span-24 last">
            <label class="span-5">From:</label>
            <span class="span-5 last">
                <input id="txtFrom" value="<%=From %>" type="text" class="date" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">To:</label>
            <span class="span-5 last">
                <input id="txtTo" value="<%=To %>" type="text"  class="date" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Include DataTrac Loans?</label>
            <span class="span-5 last">
                <input id="chkIncludeDataTracLoans" checked="checked" type="checkbox" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Include Byte Loans?</label>
            <span class="span-5 last">
                <input id="chkIncludeByteLoans" checked="checked" type="checkbox" />
            </span>
        </div>
        <div class="span-24 last">
            <label class="span-5">Date Type</label>
            <span class="span-5 last">
                <select id="dateType">
                    <option value="1">Funded</option>
                    <option value="2">Approved</option>
                </select>
            </span>
        </div>

        <br /><br />
        <div class="span-24 last">
            <label class="span-5">Specific Loan Number</label>
            <span class="span-5 last">
                <input id="txtLoans" type="text" class="span-7" />
            </span>
        </div>

        <br /><br />

        <div class="span-24 last">
            <label class="span-5">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
                                                  
            </span>
        </div>
        
        <br /><br /><br /><br />

        <div id="csvLink"></div>

</asp:Content>
