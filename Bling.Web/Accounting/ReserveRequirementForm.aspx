<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ReserveRequirementForm.aspx.cs" Inherits="Bling.Web.Accounting.ReserveRequirementForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/ReserveRequirement.js?v=1.0.5" type="text/javascript"></script>
    <link href="../Styles/ReserveRequirement.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Reserve Requirement</h1>
        
        <div class="span-23">
            <input id="btnRefreshData" type="button" class="span-4" value="Refresh Data" />
            <input id="btnConsolidateGL" type="button" class="span-4" value="Consolidate GL" />
            <br />&nbsp;<br />
            <span id="RefreshMessage"></span>
        </div>

        <div class="span-23">
        <input id="btnSendEmail" type="button" class="span-4" value="EMail P & L Recap" />
        <span class="span-3">for the month of </span>
        <select id="optMonth" class="span-2">
           <option value="1">Jan</option>
           <option value="2">Feb</option>
           <option value="3">Mar</option>
           <option value="4">Apr</option>
           <option value="5">May</option>
           <option value="6">Jun</option>
           <option value="7">Jul</option>
           <option value="8">Aug</option>
           <option value="9">Sep</option>
           <option value="10">Oct</option>
           <option value="11">Nov</option>
           <option value="12">Dec</option>
        </select>
        <input id="txtYear" class="span-2" />&nbsp; &nbsp;
        
        </div>
        <br /><br />
        <div class="span-23"><input id="chkSendToMe" checked="checked" type="checkbox" />Send to me (Remove Checkmark to send to Recipient)</div>
        <br /><br /><br /><br />
        <div>
         <table class="t1">             
            <thead>
                <tr class='yellow'>
                    <td></td>
                    <td><input id="chkAll" type="checkbox" /></td>
                    <td>Cost Center</td>                    
                    <td>Reserve Minimum</td>
                    <td>Fixed Reserve</td>
                    <td>EMail</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td><input id="txtCostCenter" type="text" class="span-2" /></td>                    
                    <td><input id="txtReserveMinimum" type="text" class="span-3"/></td>
                    <td><input id="txtFixedReserve" type="text" class="span-3"/></td>
                    <td><input id="txtRecipient" type="text" class="span-6"/></td>
                    <td><input id="btnAdd" type="button" value="Add" /></td>
                    <td></td>

                </tr>
            </thead>
            <tbody id="data">
                
            </tbody>
        </table>
        </div>
    </div>
</asp:Content>
