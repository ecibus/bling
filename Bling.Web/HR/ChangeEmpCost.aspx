<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeEmpCost.aspx.cs" Inherits="Bling.Web.HR.ChangeEmpCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="js/ChangeEmpCost.js" type="text/javascript" rel="forceLoad" rev="shown"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        <br /><br/>
        <h2>Change Employee Cost</h2><br />
        <input id="EmpCost" type="text" value="<%=Request.QueryString["EmpCost"] %>" />
        <input id="recid" type="hidden" value="<%=Request.QueryString["recid"] %>" />
        <br /><br /><br />
        <a href="#" id="UpdateEmpCost">Update</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
    </div>
    </form>
</body>
</html>
