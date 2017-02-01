<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GreenTree.aspx.cs" Inherits="Bling.Web.Secondary.GreenTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GreenTree Extract</title>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <script type="text/javascript" src="js/GreenTree.js"></script>

        <link rel="Stylesheet" href="styles/jquery-ui.css" />
        <link rel="Stylesheet" href="styles/stylez.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="ui-widget-header hdrText">
            <h2>GreenTree Extract</h2>
        </div>
        <div class="ui-widget-content">
            <div class="dvTable">
                <div class="dvRow">
                    <div class="dvCell">
                        <label for="txtStart" class="ui-widget">Start Date:</label>
                        <input id="txtStart" class="ui-widget" type="text" style="width:120px" />
                    </div>
                </div>
                <div class="dvRow">
                    <div class="dvCell">
                        <label for="txtEnd" class="ui-widget">End Date:</label>
                        <input id="txtEnd" class="ui-widget" type="text" style="width:120px; margin-left:6px;"/>
                    </div>
                </div>
                <div class="dvRow">
                    <div class="dvCell">
                        <div id="dvResults">
                        </div>
                    </div>
                </div>
                <div class="dvRow">
                    <div class="dvCell">
                        <input id="btnCreate" type="button" value="Create Extract" class="ui-widget" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
