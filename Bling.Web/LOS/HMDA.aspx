<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="HMDA.aspx.cs" Inherits="Bling.Web.LOS.HMDA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Generate HMDA Data and APR/Denial Workbook</h1> 
    <p>
        Year: <asp:DropDownList ID="ddlReportYear" runat="server" Width="70px"></asp:DropDownList>
        <br />
    </p>
    
    <p>
        <asp:CheckBox ID="chkIncludeMonth" runat="server"  /><%=m_CurrentMonthMessage%>
        <br />        
        <asp:CheckBox ID="chkAPRDenial" Text="" runat="server" />Generate APR and Denial Workbook?
        <br />    
    </p>
    
    <p>        
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" onclick="btnGenerate_Click" />
    </p>
    
    <p><%=m_HmdaLink %></p>
    <p><%=m_APRAndDenialLink %></p>
    <p><%=m_ValidationMessage %></p>
</asp:Content>
