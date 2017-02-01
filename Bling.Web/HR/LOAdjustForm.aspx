<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="LOAdjustForm.aspx.cs" Inherits="Bling.Web.HR.LOAdjustForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />    
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/LOAdjustForm.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>LO Adjustment</h1>
    
        <br />
        <div class="span-24">
            <span class="span-3">Loan Officer </span>
            <%= LODropDown %>
        </div>

        <br /><br />
        <hr />
        <div class="span-24">
            <span class="span-3">From Pay Date </span>
            <input id="txtFrom" type="text" value="<%=FromPayDate %>" />
        </div>
        <div class="span-24">
            <span class="span-3">To Pay Date </span>
            <input id="txtTo" type="text" value="<%=ToPayDate %>" />
        </div>
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <input id="btnViewReport" type="button" value="View Report" />
        </div>
        
        <%--<div class="span-24">
            <span class="span-2">LO Name </span>
            <span id="lblLOName" />
        </div>--%>

        <br /><br />
        <br />
        <br />
        <%--<table class='t1'>
            <tr class='yellow'><td>Description</td><td>Loan Number</td><td>Pay Period Ending</td><td class='number'>Amount</td><td>Comment</td><td></td></tr>
            <tr>
                <td>
                <select id='optDescription'>
                    <option>Appraisal/Inspection</option>
                    <option>Credit Report</option>
                    <option>Other</option>
                    <option>ChgOff Appr/Insp</option>
                    <option>ChgOff CrdRpt</option>
                </select>
                </td>
                <td><input type='text' class='span-3' id='txtLoanNumber' /></td>
                <td><input type='text' class='span-3' id='txtPayDate' /></td>
                <td class='number'><input type='text' class='span-2' id='txtAmount' /></td>
                <td><input type='text' class='span-5' id='txtComment' /></td>
                <td></td>
            </tr>  
            
        </table>--%>

        <div id="list" class="span-24"></div>
    </div>
</asp:Content>
