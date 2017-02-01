<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="StampPayDateForm.aspx.cs" Inherits="Bling.Web.HR.StampPayDateForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />  
    <link href="../Styles/StampPayDateForm.css" rel="stylesheet" type="text/css" />  
    <script src="js/StampPayDateForm.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">

        <h1>Stamp Pay Date</h1>


        <div class="span-24">
            <span class="span-3">Period Ending Date:</span>
            <span class="span-5">
                <input id="txtEndDate" type="text" value="<%=EndDate %>" />
            </span>            
        </div>

        <div class="span-24">
            <span class="span-3">Pay Date:</span>
            <span class="span-5">
                <input id="txtPayDate" type="text" value="<%=PayDate %>" />
            </span>            
        </div>



        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">                
                <input id="isWeekly" type="radio" value="1" name="PaySchedule" checked="checked" />Weekly
                <input id="isSalary" type="radio" value="0" name="PaySchedule" />Salary
            </span>            
        </div>

 
        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-20">                
            <br />
            <input type="button" id="btnLoad"  value="Load Loan" />&nbsp;&nbsp;
            <input type="button" id="btnStamp"  value="Stamp Pay Date" />
            </span>            
        </div>

        <div class="span-24">
            <span class="span-3">&nbsp;</span>
            <span class="span-15">                
        
            </span>            
        </div>


        <br /><br />
        <br /><br />

        <div  class="span-24" id="list"></div>
    </div>
</asp:Content>
