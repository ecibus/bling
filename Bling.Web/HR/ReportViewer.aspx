<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Bling.Web.HR.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="display:table-row">
        <div style="display:table-cell; border:thin solid black;">
            <CR:CrystalReportViewer ID="crvOne" runat="server" HasToggleParameterPanelButton="false" ShowAllPageIds="true" EnableParameterPrompt="true"
                AutoDataBind="True" GroupTreeImagesFolderUrl=""  Height="1202px" HasCrystalLogo="False" HasGotoPageButton="True"
                HasToggleGroupTreeButton="true" HasRefreshButton="True" EnableDatabaseLogonPrompt="true"
                 ReportSourceID="crsOne" HasDrilldownTabs="True" ToolbarImagesFolderUrl="" 
                ToolPanelWidth="200px" DisplayStatusbar="true" Width="1104px" />
            <CR:CrystalReportSource ID="crsOne"  runat="server">
                <Report FileName="Report/BranchBPHistory2.rpt">
                </Report>
            </CR:CrystalReportSource>
        </div>
        <div style="display:table-cell; border:thin solid transparent; padding-left:10px;">
<%--            <div>
                <h3 style="margin:0; padding:0">Reports</h3>
            </div>
            <div style="padding-top:5px">
                <asp:DropDownList ID="ddlReports" runat="server" Font-Size="Small" Width="200px"
                    onselectedindexchanged="ddlReports_SelectedIndexChanged" AutoPostBack="true" style="padding-top:3px; font-size:medium">
                </asp:DropDownList>
            </div>--%>
            
        </div>
    </div>
    </form>
</body>
</html>
