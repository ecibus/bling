<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeTitle.aspx.cs" Inherits="Bling.Web.HR.ChangeTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link href="/Styles/Bling.css" rel="stylesheet" type="text/css" />    
    <script src="js/ChangeTitle.js" type="text/javascript" rel="forceLoad" rev="shown"></script>    
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        <br /><br/>
        <h2>Change Title</h2><br />
        <textarea id="newTitle" cols="30" rows="3"><%=Request.QueryString["CurrentTitle"] %></textarea>
        <input id="col" type="hidden" value="<%=Request.QueryString["col"] %>" />
        <input id="YearMonth" type="hidden" value="<%=Request.QueryString["ym"] %>" />
        <br /><br /><br />
        <a href="#" id="Update">Update</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
    </div>
    </form>
</body>
</html>
