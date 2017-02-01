<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEnrolee.aspx.cs" Inherits="Bling.Web.HR.AddEnrolee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="/Styles/Bling.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/InsuranceEnrollment.css" rel="stylesheet" type="text/css" />  
    <script src="js/AddEnrolee.js" type="text/javascript" rel="forceLoad" rev="shown"></script>   
</head>
<body>
    <form id="form1" runat="server">
    <div id="modal">
        <h2>Enroll Employee</h2><br />
        <div class="AddEnrolee">        
            <label for="BranchNoAdd">Branch No :</label><span id="BranchNoAdd"><%= Request.QueryString["branchNo"] %></span><br />
            <label for="optEmpName">Employee Name: </label>
            <%=EmployeeDropdown %>  
            <br />
            
            <label for="BirthDate">Birth Date :</label><span id="BirthDate"></span> <br />
            
            <label for="chkIsLO">Is LO :</label><input id="chkIsLO" type="checkbox" /><br />
            
            <div id="AddDropdown">
            <label for="EEStatus">EE Status :</label><%=EEStatusDropDown %> <br />
            
            <label for="Ins1"><%=InsuranceTitle.Title1 %> :</label><%=Rate1DropDown %> <br />
            <label for="Ins3"><%=InsuranceTitle.Title3 %> :</label><%=Rate3DropDown %> <br />
            <label for="Ins4"><%=InsuranceTitle.Title4 %> :</label><%=Rate4DropDown %> <br />
            <label for="Ins5"><%=InsuranceTitle.Title5 %> :</label><%=Rate5DropDown %> <br />
            <label for="Ins6"><%=InsuranceTitle.Title6 %> :</label><%=Rate6DropDown %> <br />
            <label for="Ins7"><%=InsuranceTitle.Title7 %> :</label><%=Rate7DropDown %> <br />
            <label for="Ins9"><%=InsuranceTitle.Title9 %> :</label><%=Rate9DropDown %> <br />
            <label for="Ins10"><%=InsuranceTitle.Title10 %> :</label><%=Rate10DropDown %> <br /> 
            <label for="Ins11"><%=InsuranceTitle.Title11 %> :</label><%=Rate11DropDown %> <br />
            <label for="Ins12"><%=InsuranceTitle.Title12 %> :</label><%=Rate12DropDown %> <br /> 
            <label for="EmpCostAdd">Employee Cost :</label> <input id="EmpCostAdd" type="text" value="0.00" /><br />  
            
            <input id="YearMonthAdd" type="hidden" value="<%= Request.QueryString["yearmonth"] %>" />             
            </div>
        </div>
        
        <br /><br />
        <a href="#" id="EnrollEmployee">Add</a>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="nyroModalClose">Cancel</a>
    </div>
    </form>
</body>
</html>
