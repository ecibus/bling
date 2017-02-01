<%@ Page Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="UploadMCM.aspx.cs" Inherits="Bling.Web.Secondary.UploadMCM" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/MCM.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">    
    <h1>MCM File Generator</h1>
    Select type of file you want to generate:<br />
    <ul>
        <li><asp:CheckBox ID="chkLockedLoan" runat="server" Checked="true" />Locked Loan</li>
        <li><asp:CheckBox ID="chkClosedLoan" runat="server" Checked="true" />Closed Loan</li>
        <li><asp:CheckBox ID="chkFalloutLoan" runat="server" Checked="true" />Fallout</li>
        <li><asp:CheckBox ID="chkTrades" runat="server" Checked="true" />Trades</li>
    </ul>
    <br />
        
    <asp:CheckBox ID="chkSend" runat="server" />FTP files to MCM?<br />

    <br />
    <asp:Button ID="btnSend" runat="server" Text="Submit" onclick="btnSend_Click" />

    <%--<br />
    <div>
    <br />
    <br />
    <b>Note for Byte Pro:</b>
    <br />
    <br />
    The following are now ready for Byte testing, click the Include Byte checkbox to include Byte Loans on the CSV.
    <span>
    <ul>
        <li>Locked Loans</li>
        <li>Closed Loans</li>
        <li>Fallout</li>
    </ul> 
    </span>
    </div>--%>
</div>
</asp:Content>
