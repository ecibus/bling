<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ByteReportForm.aspx.cs" Inherits="Bling.Web.CustomerService.ByteReportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/underscore.js" type="text/javascript"></script>
    <script src="../js/byte.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container showgrid1 inline2">
        <h1>Byte Report</h1>
        
        <div  class="span-7 ">
        <select size="10" id="rptList">
            
        </select>
        </div>
        
        <div class="span-10  last">
            <span id="parameters">
                
            </span>

            <input type="button" value="View Report" id="btnViewReport" class="hidden" />
        </div>
    </div>

    <script>
      var reports = <%=ResponseText %>
    </script>


</asp:Content>
