<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CensusDateRange.aspx.cs" Inherits="Bling.Web.HR.CensusDateRange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/CensusDateRange.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>Census Date Range</h1>
     
     <fieldset>
         <label for="txtFrom">From: </label> <input id="txtFrom" value="<% =m_From %>" type="text" /><br />
         <label for="txtTo">To: </label> <input id="txtTo" type="text" value="<% =m_To %>"  /><br />
         <label for="btnUpdate">&nbsp;</label><input id="btnUpdate" type="button" value="Update" />         
     </fieldset>
     <br /> 
     <br />
     This will only change the date range in 401K.mdb.<br />
     <br />
     To generate the report:<br/ >
     1. Make sure you change the From/To and then Click Update<br/ >
     2. Open 401K.mdb<br/ >
     3. Click 401K Census<br/ >
     4. Click Export Data<br/ >
     5. Close application and open 401KCens.txt in Excel<br/ >
     6. Choose Delimited and click Next<br/ >
     7. Check comma and click Next<br />
     8. Click Finish<br />
     
</asp:Content>
