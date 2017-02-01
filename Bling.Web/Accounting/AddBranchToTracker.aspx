<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AddBranchToTracker.aspx.cs" Inherits="Bling.Web.Accounting.AddBranchToTracker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/AddBranch.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Add Branch in Tracker</h1>
    Sort Branch by  
    <asp:RadioButton ID="BranchCode" runat="server" AutoPostBack="true" 
        GroupName="SortOrder" Checked="true" 
        oncheckedchanged="SortOrder_CheckedChanged" />Branch Code
    <asp:RadioButton ID="BranchName" runat="server" AutoPostBack="true" 
        GroupName="SortOrder" oncheckedchanged="SortOrder_CheckedChanged" />Branch Name
    <br /><br />
    
    <div id="leftside">
        <strong>Available Branch</strong><br />
        <%=m_AvailableBranch %>
    </div>
    <div id="middle">
        <br /><br /><br />
        <asp:Button ID="btnAdd" Width="10em" runat="server" Text="Add >>" 
            onclick="btnAdd_Click" /><br /><br />
        <br /><br />
        
    </div>
    <div id="rightside">
        <strong>Included Branch</strong><br />
        <%=m_TBranch %>
    </div>
    
</asp:Content>

