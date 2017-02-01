<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ActiveBranchForm.aspx.cs" Inherits="Bling.Web.Accounting.ActiveBranchForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/ActiveBranch.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Active Branch</h1>

         <table class="t1">
            <thead>
                <tr class='yellow'>
                    <td>Month End</td>                    
                    <td>Current Month</td>
                    <td>Current Month - 1</td>
                    <td>Current Month - 2</td>
                    <td>FYTD</td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td><input id="txtMonthEnd" type="text" class="span-3 date" /></td>                    
                    <td><input id="txtCurrentMonth" type="text" class="span-3"/></td>
                    <td><input id="txtCurrentMonthM1" type="text" class="span-3"/></td>
                    <td><input id="txtCurrentMonthM2" type="text" class="span-3"/></td>
                    <td><input id="txtFYTD" type="text" class="span-3"/></td>
                    <td><input id="btnAdd" type="button" value="Add" /></td>
                    <td></td>

                </tr>
            </thead>
            <tbody id="data">
                
            </tbody>
        </table>

    </div>

</asp:Content>
