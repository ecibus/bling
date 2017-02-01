<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="MERSReconciliationForm.aspx.cs" Inherits="Bling.Web.Compliance.MERSReconciliationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline">
        <h1>MERS Reconciliation</h1>
        1. Select the MERS file to reconcile.<br /> 
        <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" /><br /><br />
        2. Click the Upload button to see the Loans with Purchased Date.<br />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" />    
        <br />
        <br />

        <%= MERSData %>
    </div>
</asp:Content>
