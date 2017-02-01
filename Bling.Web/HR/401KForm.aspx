<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="401KForm.aspx.cs" Inherits="Bling.Web.HR._401KForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />  
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/401k.js" type="text/javascript"></script>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">
        <h2>401K Report</h2>

        <div class="span-24">
            <span class="span-3">Weekly/Salary</span>
            <span class="span-5">
                <select id="optWS">
                    <option value="w">Weekly</option>
                    <option value="s">Salary</option>
                </select>
            </span>            
        </div>
        <div class="span-24">
            <span class="span-3">Start:</span>
            <span class="span-5">
                <input id="txtStart" type="text" value="" />
                
            </span>            
        </div>
        <div class="span-24">
            <span class="span-3">End:</span>
            <span class="span-5">
                <input id="txtEnd" type="text" value="" />
            </span>            
        </div>

        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-7">
                <input id="btnViewReport" type="button" value="View Report" />                
                <input id="btnGenerateCSV" type="button" value="Generate CSV" />                
            </span>            
        </div>

        <br /><br />&nbsp;<br />
        <div class="span-24 status">  
            <span class="span-3">&nbsp;</span>          
            <span id="Message"></span>
        </div>
    </div>
</asp:Content>
