<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="ValidateTimeCard.aspx.cs" Inherits="Bling.Web.HR.ValidateTimeCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/ValidateTimeCard.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="container showgrid1 inline2">
        <h2>Validate Time Card</h2>

        <div class="span-24">
            <span class="span-3">Pay Start:</span>
            <span class="span-5">
                <input id="txtStart" type="text" value="" />
                
            </span>            
        </div>
        <div class="span-24">
            <span class="span-3">Pay End:</span>
            <span class="span-5">
                <input id="txtEnd" type="text" value="" />
                <input id="btnValidate" type="button" value="Validate" />                
            </span>            
        </div>

        <br /><br />
        <div  class="span-24" id="list"></div>
    </div>
</asp:Content>
