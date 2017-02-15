<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="TurningPoint.aspx.cs" Inherits="Bling.Web.Accounting.TurningPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <link href="styles/TurningPoint.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container">
        <div>
            <h1>Turning Point</h1>
        </div>
        
        <div>
            Search
            <input type="text" id="txtSearch"  />
            <input type="button" id="btnSearch" value="Search" />
        </div>

        <table class="t1" id="searchresult">
        </table>

        <h4>Loan Officer Enrolled in Turning Point</h4>
        <table class="t1">
            <thead>
                <tr class="yellow">
                    <td></td>
                    <td>Loan Officer</td>
                    <td>Username</td>
                    <td>&nbsp;</td>
                </tr>
            </thead>
            <tbody id="tpuser" ></tbody>
        </table>
    </div>
    
    <script src="js/TurningPoint.js" type="text/javascript"></script>
</asp:Content>
