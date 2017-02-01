<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeEEStatus.aspx.cs" Inherits="Bling.Web.HR.ChangeEEStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="/Styles/Bling.css" rel="stylesheet" type="text/css" />    
    <script src="js/ChangeEEStatus.js" type="text/javascript" rel="forceLoad" rev="shown"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        <h2>Change EE Status</h2><br />        
        <%=EEStatusDropDown%>
        <br /><br />
        <a href="#" id="UpdateEEStatus">Update</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
        <br /><br />
        
        <input id="recid" type="hidden" value="<%=Request.QueryString["recid"] %>" />
    </div>
    </form>
</body>
</html>
