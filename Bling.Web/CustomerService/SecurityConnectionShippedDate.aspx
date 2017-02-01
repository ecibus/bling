<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="SecurityConnectionShippedDate.aspx.cs" Inherits="Bling.Web.CustomerService.SecurityConnectionShippedDate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/SecurityConnectionShippedDate.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Security Connection</h1>
    1. Select the Security Connection File (must be previously save as Tab delimited).<br /> 
    <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" /><br /><br />
    2. Click the Upload button and verify the data displayed.<br />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" />    
    <br />
    <br />
    <%=m_data%>
    <br />
    
    <asp:Literal ID="Literal1" runat="server" Visible="False">3. Click the Update button to Update Shipped Date in DataTrac and Byte.</asp:Literal>        
    <br />
    <asp:Button ID="btnUpdateDataTrac" runat="server" Text="Update" 
        onclick="btnUpdateDataTrac_Click" Visible="False" />
    
</asp:Content>
