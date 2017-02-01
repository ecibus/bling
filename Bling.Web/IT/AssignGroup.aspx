<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AssignGroup.aspx.cs" Inherits="Bling.Web.IT.AssignGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Assign Group</h1> 
    <div id="leftside">
        <strong>Current User</strong><br />
        <asp:ListBox ID="lbCurrentUser" runat="server" Width="17em" Rows="15" 
            AutoPostBack="True" onselectedindexchanged="lbCurrentUser_SelectedIndexChanged"></asp:ListBox>
    </div>
    <div id="middle">
        <strong>Available Group</strong><br />
        <asp:ListBox ID="lbAvailableGroup" runat="server" Width="17em" Rows="11"></asp:ListBox>
        <br />
        <asp:Button ID="btnAdd" Width="10em" runat="server" Text="Add Group" 
            onclick="btnAdd_Click" /><br /><br />
        <asp:Button ID="btnRemove" Width="10em" runat="server" Text="Remove Group" 
            onclick="btnRemove_Click" />
    </div>
    <div id="rightside">
        <strong>Member Of</strong><br />
        <asp:ListBox ID="lbMemberOf" runat="server" Width="17em" Rows="15"></asp:ListBox>
    </div>
</asp:Content>
