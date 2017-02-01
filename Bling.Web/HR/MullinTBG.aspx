<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MullinTBG.aspx.cs" Inherits="Bling.Web.HR.MullinTBG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CSV Creator</title>
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <script type="text/javascript" src="js/MTBG.js"></script>

        <link rel="Stylesheet" href="styles/jquery-ui.css" />
        <link rel="Stylesheet" href="styles/stylez.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="ui-widget-header hdrText">
            <h2>CSV Creator</h2>
        </div>
        <div class="ui-widget-content">
            <div class="dvTable">
                <div class="dvRow">
                    <div class="dvCell">
                        <select id="selOptions" class="ui-widget">
                            <option value="w">Weekly</option>
                            <option value="s">Salary</option>
                        </select>
                    </div>
                </div>
                <div class="dvRow">
                    <div class="dvCell">
                        <label for="txtPstart" class="ui-widget">Pay Period Start:</label>
                        <input id="txtPstart" class="ui-widget" type="text" />
                    </div>
                </div>
                <div class="dvRow">
                    <div class="dvCell">
                        <label for="txtPend" class="ui-widget">Pay Period End:</label>
                        <input id="txtPend" class="ui-widget" type="text" />
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
                        <input id="btnCreate" type="button" value="Create CSV" class="ui-widget" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
