<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="TimeCardForm.aspx.cs" Inherits="Bling.Web.HR.TimeCardForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TimeCardForm.css" rel="stylesheet" type="text/css" />
    <script src="js/TimeCardForm.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Time Card</h1>
    
        <br />
        <div class="span-24">
            <span class="span-3">Month</span>
            <%=MonthHtml %>
        </div>
        <div class="span-24">
            <span class="span-3">Year</span>
            <%=YearHtml %>
        </div>
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <input type="checkbox" id="chkAccepted" checked="checked" />
            Show Only Accepted by Manager 
        </div>

        <br /><br /><br />
        <div class="span-24 prepend-top">
            <table border="0">
                <colgroup>                    
                </colgroup>
                <thead id='calendarHead'>
                </thead>
                <tbody id='calendarBody'>
                </tbody>
            </table>
            
        </div>
    </div>
</asp:Content>
