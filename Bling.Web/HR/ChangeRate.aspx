<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeRate.aspx.cs" Inherits="Bling.Web.HR.ChangeRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link href="/Styles/Bling.css" rel="stylesheet" type="text/css" />    
    <script src="js/ChangeRate.js" type="text/javascript" rel="forceLoad" rev="shown"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        <h2>Change Rate</h2><br />
        <span id="plan"><%=Request.QueryString["plan"] %></span>
        <%=RateDropDown %>
        <br /><br />
        <a href="#" id="Update">Update</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
        <br /><br />
        
        <h2>Add New Rate</h2><br />
        <input id="newRate" type="text" />
        <br /><br />
        <a href="#" id="AddNewRate">Add</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
        
        <input id="recid" type="hidden" value="<%=Request.QueryString["recid"] %>" />
        <input id="fieldNo" type="hidden" value="<%=Request.QueryString["type"] %>" />
    </div>
    </form>
</body>
</html>
