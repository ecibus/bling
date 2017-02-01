<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="TradeForm.aspx.cs" Inherits="Bling.Web.Secondary.TradeForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/TradeForm.js?v=0.0.0.2" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Trade File</h1>

        <div class="span-24 last">
            <label class="span-5">Pair Off Date From:</label>
            <span class="span-5 last">
                <input id="txtFromPairOffDate" value="<%=From %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Pair Off Date To:</label>
            <span class="span-5 last">
                <input id="txtToPairOffDate" value="<%=To %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Settle Date From:</label>
            <span class="span-5 last">
                <input id="txtFromSettleDate" value="<%=From %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Settle Date To:</label>
            <span class="span-5 last">
                <input id="txtToSettleDate" value="<%=To %>" type="text" />
            </span>
        </div>

        <!--
        <div class="span-24 last">
            <label class="span-5">From:</label>
            <span class="span-5 last">
                <input id="txtFrom" value="<%=From %>" type="text" />
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">To:</label>
            <span class="span-5 last">
                <input id="txtTo" value="<%=To %>" type="text" />
            </span>
        </div>


        <div class="span-24 last">
            <label class="span-5">Date to use for Range:</label>
            <span class="span-5 last">
                <select id="optDateForRange">
                    <option value="3">Commitment Date</option>
                    <option value="1">Settle Date</option>
                    <option value="2">Pair Off Date</option>
                </select>
            </span>
        </div>
        !-->


        <div class="span-24 last">
            <label class="span-5">Status:</label>
            <span class="span-5 last">
                <select id="optStatus">
                    <option value="2">Both</option>
                    <option value="1">Filled</option>
                    <option value="0">Open</option>
                </select>
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">Sort By:</label>
            <span class="span-5 last">
                <select id="optSortBy">
                    <option value="3">Commitment Date</option>
                    <option value="1">Settle Date</option>
                    <option value="2">Dealer</option>
                </select>
            </span>
        </div>

        <div class="span-24 last">
            <label class="span-5">&nbsp;</label>
            <span class="span-5 last">
                <input id="btnGenerate" type="button" value="Generate CSV" />
                                                  
            </span>
        </div>
        
        <br /><br /><br /><br />

    <div id="csvLink"></div>

</asp:Content>
