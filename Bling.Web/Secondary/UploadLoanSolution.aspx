<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="UploadLoanSolution.aspx.cs" Inherits="Bling.Web.Secondary.UploadLoanSolution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Upload Loan Solution</h1>
    1. Select the Loan Solution Program File (must be previously save as csv <br />and only contains InvestorName, InvestorProductName, InvestorProductCodeAlias).<br /> 
    <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" /><br /><br />
    2. Click the Upload button to upload the file.<br />
    <asp:Button ID="btnVerify" runat="server" Text="Upload" onclick="btnUpload_Click" />    
</asp:Content>
