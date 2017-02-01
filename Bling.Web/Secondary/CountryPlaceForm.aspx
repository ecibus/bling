<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CountryPlaceForm.aspx.cs" Inherits="Bling.Web.Secondary.CountryPlaceForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container showgrid1 inline">
        <h1>Country Place</h1>
        1. Select the Country Place file converted to csv.<br /> 
        <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" /><br /><br />
        2. Click the Generate button to create the flat file for Optimal Blue.<br />
        <asp:Button ID="btnUpload" runat="server" Text="Generate" onclick="btnUpload_Click" />    
        <br />
        <br />

        <%= CountryPlaceData%>
    </div>

</asp:Content>
