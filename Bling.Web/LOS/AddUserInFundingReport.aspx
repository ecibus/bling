<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AddUserInFundingReport.aspx.cs" Inherits="Bling.Web.LOS.AddUserInFundingReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Add User in Funding Report # 94, 95, and 97</h1> 
    <div id="leftside">
        <strong>Available User</strong><br />
        <asp:ListBox ID="lbAvailableUser" runat="server" Width="17em" Rows="10"></asp:ListBox>
    </div>
    <div id="middle">
        <br /><br /><br />
        <asp:Button ID="btnAdd" Width="10em" runat="server" Text="Add >>" 
            onclick="btnAdd_Click" /><br /><br />
        <asp:Button ID="btnRemove" Width="10em" runat="server" Text="<< Remove" 
            onclick="btnRemove_Click" />
    </div>
    <div id="rightside">
        <strong>Report User</strong><br />
        <asp:ListBox ID="lbCurrentUser" runat="server" Width="17em" Rows="10"></asp:ListBox>
    </div>
</asp:Content>
