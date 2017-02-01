<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="BenefitNotification.aspx.cs" Inherits="Bling.Web.HR.BenefitNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/CensusDateRange.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>Benefit Notification Report</h1>
     
     <fieldset>
         <label for="txtFrom">Hired From: </label> <input id="txtFrom" name="txtFrom" value="<%= From %>" type="text" /><br />
         <label for="txtTo">Hired To: </label> <input id="txtTo" name="txtTo" type="text" value="<%= To %>"  /><br />
         <label for="btnUpdate">&nbsp;</label> <asp:Button ID="btnViewReport" 
             runat="server" Text="ViewReport" onclick="btnViewReport_Click" />       
     </fieldset>
    
</asp:Content>
