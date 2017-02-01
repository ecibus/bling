<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ComplianceEase.aspx.cs" Inherits="Bling.Web.Compliance.ComplianceEase" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="js/ComplianceEase.js?v=0.0.2" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Compliance Ease</h2>
    <fieldset>
    <label for="txtStart">Funded From : </label> 
    <input id="txtStart" type="text" class="date" /><br />
    <label for="txtEnd">Funded To : </label>
    <input id="txtEnd" type="text"  class="date" /><br />

    <br /><br />
    <div class="span-24 last">
        <label class="span-5">Specific Loan Number :</label>
        <span class="span-5 last">
            <input id="txtLoans" type="text" class="span-7" />
        </span>
    </div>

    <label for="btnGenerate">&nbsp;</label>     
    <input id="btnGenerate" type="button" value="Generate Batch File" />
    </fieldset>
    <br />

    <div id="Link"></div>
</asp:Content>
