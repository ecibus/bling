<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="HideByProgramCode.aspx.cs" Inherits="Bling.Web.Secondary.HideByProgramCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/HideByProgramCode.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Show/Hide Program by Program Code</h1>
    <p>Select Program Code from the dropdown to display the Program Description.</p>
    Program Code: <%=m_ProgramCodeDropdown%>
    <br /><br />
    
    <p>
        Put a check mark on the checkbox to hide the Program Description and remove it to show.<br />
        Clicking the checkbox will automatically update the database.
    </p>
    <div id="data">
        <div id="programcode"></div>
  
    </div>
</asp:Content>

