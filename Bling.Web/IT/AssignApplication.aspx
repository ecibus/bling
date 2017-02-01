<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AssignApplication.aspx.cs" Inherits="Bling.Web.IT.AssignApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Assign Application</h1> 
    <div id="leftside">
        <strong>Application</strong><br />
        <asp:ListBox ID="lbApplication" runat="server" Width="17em" Rows="15"></asp:ListBox>
    </div>
    <div id="middle">
        <strong>Group</strong><br />
        <asp:ListBox ID="lbAvailableGroup" runat="server" Width="17em" Rows="11" AutoPostBack="True" 
            onselectedindexchanged="lbAvailableGroup_SelectedIndexChanged"></asp:ListBox>
        <br />
        <asp:Button ID="btnAdd" Width="10em" runat="server" Text="Add Application" 
            onclick="btnAdd_Click" /><br /><br />
        <asp:Button ID="btnRemove" Width="10em" runat="server" Text="Remove Application" 
            onclick="btnRemove_Click" />
    </div>
    <div id="rightside">
        <strong>Member</strong><br />
        <asp:ListBox ID="lbMember" runat="server" Width="17em" Rows="15"></asp:ListBox>
    </div>
</asp:Content>
