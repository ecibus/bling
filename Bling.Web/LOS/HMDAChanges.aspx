<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="HMDAChanges.aspx.cs" Inherits="Bling.Web.LOS.HMDAChanges" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/HMDAChanges.js" type="text/javascript"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>HMDA Changes</h1>
    <fieldset>    
        <label for="ddlReportYear">Report Year</label> <asp:DropDownList ID="ddlReportYear" runat="server" Width="70px"></asp:DropDownList><br />
        <label for="txtLoanNumber">Loan Number</label> <input id="txtLoanNumber" type="text" /><br />
        <label for="ddlFields">Field Name</label> <asp:DropDownList ID="ddlFields" runat="server" Width="13em"></asp:DropDownList><br />
        <label for="txtNewData">New Data</label> <input id="txtNewData" type="text" /><br />
        <label for="btnAdd">&nbsp;</label><input id="btnAddChanges" type="button" value="Add Changes" />
    </fieldset> 
    <br /><br />
    <div id="Changes"></div>
    
</asp:Content>
