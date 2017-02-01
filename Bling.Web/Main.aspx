<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Bling.Web.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/js/Main.js" type="text/javascript"></script>    
    <script type="text/javascript">
        <%=AllowedApplicationScript %>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
    <%=ApplicationList%>    
    
</asp:Content>

