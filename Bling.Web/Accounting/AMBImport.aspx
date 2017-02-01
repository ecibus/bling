<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AMBImport.aspx.cs" Inherits="Bling.Web.Accounting.AMBImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/AMBImportForm.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>AMB  Import</h1>


        <div class="span-24 last">
            <label class="span-3">Report:</label>
            <span class="span-5 last">
                <select id="optReportType">
                    <option value="1">AMB Borrower Import</option>
                    <%--<option value="2">Additional Borrower Import Funding</option>
                    <option value="3">Additional Borrower Import Purchase</option>--%>
                    <option value="4">Loan Payment Posting</option>
                    <option value="5">Funding Extract</option>
                    <option value="6">Secondary Gain Receivable</option>
                    <option value="7">Hedge Loan Revenue Extract</option>
                    <option value="8">Purchase Extract</option>
                    <option value="9">Brokered Loan Extract</option>
                    <option value="10">Commission Accrual</option>
                    <option value="11">Cancelled Denied Report</option>
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
            <label class="span-3">Include Byte Loans?</label>
            <span class="span-5 last">
                <input id="chkIncludeByteLoans" type="checkbox" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-3">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
                                                  
            </span>
        </div>

        <div class="span-24 last">
            <br /><br />
            <label class="span-3">&nbsp;</label>
            <span class="span-15 last">
                <b>Note for Byte Pro</b>
                <br />
                <br />
                The following are now ready for Byte testing, click the Include Byte checkbox to include Byte Loans on the CSV.
                <ul>
                    <li>AMB Borrower Import</li>
                    <li>Funding Extract</li>
                    <li>Brokered Loan Extract</li>
                    <li>Secondary Gain Receivable</li>
                    <li>Purchase Extract</li>
                </ul>   
            </span>
        </div>
        
        <br /><br /><br /><br />

        <div id="csvLink"></div>

    </div>

</asp:Content>
