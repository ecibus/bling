<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="LOBasisPoints.aspx.cs" Inherits="Bling.Web.HR.LOBasisPoints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.busy.min.js" type="text/javascript"></script>
    <script src="js/LOBasisPoints.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
<!--

        function btnBasisPointCurrent_onclick() {

        }

// -->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <div class="container1 showgrid1 inline2">

        <h1>Loan Officer Basis Points</h1>

        <div class="span-24 last">
            <span class="span-3">Loan Officer:</span>
            <span class="span-5 last">
                <%=LODropDown%>                                
            </span>
        </div>
        <div class="span-24 last">
            <span class="span-3">Branch:</span>
            <span id="branch"></span>
        </div>
        <br /><br /><br />

        <div class="span-24 last">
            <span class="span-4">Effective Date:</span>
            <input id="txtEffectiveDate" type="text" class="span-3" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Weekly Pay:</span>
            <input id="chkWeekly" type="checkbox" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Inside Sales Rep:</span>
            <input id="chkInside" type="checkbox" />
        </div>

        

        <div class="span-24 last">
            <span class="span-4">Base Commission (%):</span>
            <input id="txtBaseCommission" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Brokered Loans (%):</span>
            <input id="txtBrokeredLoans" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Minimum ($):</span>
            <input id="txtMinimum" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Maximum ($):</span>
            <input id="txtMaximum" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 1 (%):</span>
            <input id="txtTier1" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 2 (%):</span>
            <input id="txtTier2" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 3 (%):</span>
            <input id="txtTier3" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 4 (%):</span>
            <input id="txtTier4" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 5 (%):</span>
            <input id="txtTier5" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Tier 6 (%):</span>
            <input id="txtTier6" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Branch Override (%):</span>
            <input id="txtBranchOverride" type="text" class="span-2" />
        </div>

        <div class="span-24 last">
            <span class="span-4">Manager:</span>
            <input id="chkManager" type="checkbox" />
        </div>



        <div class="span-24 last">
            <span class="span-4">&nbsp;</span>
            <input id="btnAdd" type="button" value="Add" disabled="disabled" />
        </div>
        <br /><br /><br />

        <div class="span-24 last">
            <label class="span-4">Branch No: </label>
            <input id="txtBranchNo" type="text" class="span-2"/> 
        </div>
        <div class="span-24 last">
            <span class="span-4">&nbsp;</span>
            <%--<input id="btnVBPR" type="button" value="View Basis Point Report" />--%>
            <input id="btnLOBpCurrent" type="button" value="LO BP Current" />
            <input id="btnLOBpHistory" type="button" value="LO BP History" />
            <input id="btnBasisPointHistory" type="button" value="Basis Point History" />
            <input id="btnBasisPointCurrent" type="button" value="Basis Point Current" />
            &nbsp;<input type="checkbox" id="insideSales" value="1" />Show only Inside Sales LO
        </div>

        <br /><br /><br />

        <div id="list" class="span-24 last"></div>
        <br /><br />
        <div id="msgUpdate" class="span-24 last msgFade" />
    </div>

</asp:Content>
