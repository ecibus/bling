<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Bling.Web.IT.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Add User</h1> 
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
        <strong>Allowed User</strong><br />
        <asp:ListBox ID="lbCurrentUser" runat="server" Width="17em" Rows="10"></asp:ListBox>
    </div>
    
    
    <div style="clear:both">
        <br />
        To be listed in the Available User
        <ul>
            <li>User must have a hire date.</li>
            <li>User must not have a termination date.</li>
            <li>User is a licensed user in datatrac.</li>
            <li>Exclude is not check.</li>
        </ul>
    </div>
    
</asp:Content>
