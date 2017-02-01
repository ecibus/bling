<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="InsuranceEnrollment.aspx.cs" Inherits="Bling.Web.HR.InsuranceEnrollment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="/Styles/InsuranceEnrollment.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jqModal.css" rel="stylesheet" type="text/css" />
    
    <script src="js/InsuranceEnrollment.js" type="text/javascript" rel="forceLoad"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Insurance Enrollments</h1>
    <fieldset> 
    <label>Year/Month:</label><%=YearMonthDropDown%><br />
    <label>Branch:</label><%=BranchDropDown%> <br />    

    <label>New Month:</label><%=MonthDropDown %><%=YearDropDown %>
    <input id="btnAddMonth" type="button" value="Add New Month" /><br />
    <label>&nbsp;</label>
    <a href="#" id="LOEnroll">LO Enroll</a>&nbsp;&nbsp;
    <a href="#" id="InsEnroll">Ins Enroll</a>&nbsp;&nbsp;
    <a href="#" id="BranchEnroll">Branch Enroll</a><br />
    <label>&nbsp;</label>
    <a href="#" id="LOEnrollPB">LO Enroll Page Break</a>&nbsp;&nbsp;
    <a href="#" id="InsEnrollPB">Ins Enroll Page Break</a>&nbsp;&nbsp;

    </fieldset> 
    <div id="yearMonthLabel"></div>
    
    <br />
    <table id="InsuranceEnrollment">
        <thead>
            <tr class="yellow">
                <td></td>
                <td>Branch</td>
                <td class="EmpName">Name</td>
                <td>LO?</td>
                <td>Birth Date</td>
                <td id="HR_Ins8_Title" class="Title center"></td>
                <td id="HR_Ins1_Title" class="Title" align="right"></td>
                <td id="HR_Ins3_Title" class="Title" align="right"></td>
                <td id="HR_Ins4_Title" class="Title" align="right"></td>
                <td id="HR_Ins5_Title" class="Title" align="right"></td>
                <td id="HR_Ins6_Title" class="Title" align="right"></td>
                <td id="HR_Ins7_Title" class="Title" align="right"></td>
                <td id="HR_Ins9_Title" class="Title" align="right"></td>
                <td id="HR_Ins10_Title" class="Title" align="right"></td>
                <td id="HR_Ins11_Title" class="Title" align="right"></td>
                <td id="HR_Ins12_Title" class="Title" align="right"></td>
                <td align="right" class="total">Total Cost</td>
                <td align="right" class="total">Employee Cost</td>
                <td align="right" class="total">Branch Cost</td>                
            </tr>
        </thead>
        <tbody id="enrollment">
        </tbody>
    </table>
    
    
</asp:Content>
