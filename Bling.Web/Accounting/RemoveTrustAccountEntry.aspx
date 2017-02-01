<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="RemoveTrustAccountEntry.aspx.cs" Inherits="Bling.Web.Accounting.RemoveDocTrustAccountLogEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/RemoveTrustAccountEntry.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>DOC Loan Lookup</h1>
    <fieldset>    
        <label for="txtApplicationNumber">Application Number</label> <input id="txtApplicationNumber" type="text" /><br />
        <label for="btnLoad">&nbsp;</label><input id="btnLoad" type="button" value="Load" />
    </fieldset> 
    <br /><br />
    <div id="TrustAccount"></div>
</asp:Content>
