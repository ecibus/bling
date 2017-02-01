<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="Bling.Web.IT.Inventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/Inventory.js?v=0.0.1" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Inventory</h1>
    <br />
    <fieldset> 
        <label for="txtMake">Make: </label> <input id="txtMake" type="text" size="38" maxlength="100" /><br />
        <label for="txtModel">Model: </label> <input id="txtModel" type="text" size="38" maxlength="100" /><br />
        <label for="txtSerialNumber">Serial Number: </label> <input id="txtSerialNumber" type="text" size="38" maxlength="100" /><br />
        <label for="txtQuantity">Quantity: </label> <input id="txtQuantity" type="text" size="38" maxlength="100" /><br />
        <label for="txtIssuedOn">Issued On: </label>  <input id="txtIssuedOn" type="text" size="38" maxlength="100" /><br />
        <label for="txtUser">User: </label>        
        <%= m_UserDropDown%><br />
        <label for="txtBranch">Branch: </label> <div id="branch"></div> <br />
        <label for="btnAdd">&nbsp;</label>
        <input id="btnAdd" type="button" value="Add Inventory" />

    </fieldset>
    <br />    
    <br />
    Search <input id="txtSearch" type="text" size="30" /> <input id="btnSearch" type="button" value="Search Inventory" />
    <br />    
    <br />

<%--    Issued To: <span id="Users"></span>&nbsp;&nbsp;&nbsp;&nbsp;
    Branches: <span id="Branches"></span>
--%>    <br /> <br />
    <div id="data"></div>

</asp:Content>
