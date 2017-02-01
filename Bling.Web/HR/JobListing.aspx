<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="JobListing.aspx.cs" Inherits="Bling.Web.HR.JobListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/JobListing.js?v=1.0.1" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h1>Job Listings</h1>
     <label>&nbsp;</label>
        <input id="rdoOpen" type="radio" checked="checked" name="JobType" />Open Job
        <input id="rdoClose" type="radio" name="JobType" />Close Job
     <label for="Jobs">Select Position: </label><div id="OpenJob"><%=m_OpenJobsDropdown %></div> <br /><br />
    
     
     <div id="hr1">     
         <label for="txtPosition">Position: </label> <input id="txtPosition" type="text" size="38" maxlength="100" /><br />
         <label for="txtQualification">Qualification: </label><textarea id="txtQualification" cols="30" rows="10" ></textarea><br />
         <label for="txtDescription">Description: </label><textarea id="txtDescription" cols="30" rows="10" ></textarea><br />
         <label for="txtSkills">Skills: </label><textarea id="txtSkills" cols="30" rows="10" ></textarea><br />
         <label for="txtEducation">Minimum Education: </label><textarea id="txtEducation" cols="30" rows="10" ></textarea><br />
         <label for="txtFilename">Filename: </label> <input id="txtFilename" type="text" size="38" maxlength="50" /><br /><br />
         
     </div>
     
     <div id="hr2">     
         <label for="txtLocationCity">Location City: </label> <input id="txtLocationCity" type="text" size="38" maxlength="50" /><br />
         <label for="txtLocationBranch">Location Branch: </label> <input id="txtLocationBranch" type="text" size="38" maxlength="50" /><br />
         <label for="txtSalary">Salary: </label> <input id="txtSalary" type="text" size="38" maxlength="50" /><br />
         <label for="txtHourly">Minimum / Midpoint: </label> <input id="txtHourly" type="text" size="38" maxlength="50" /><br />
         <label for="txtBenefits">Benefits: </label> <textarea id="txtBenefits" cols="30" rows="10" ></textarea><br />
         <label for="txtCloseDate">Close Date: </label> <input id="txtCloseDate" class="date" type="text" size="15"  /><br />
         <label for="txtPostDate">Post Date: </label> <input id="txtPostDate" class="date" type="text" size="15"  /><br />
         <label for="txtFillDate">Fill Date: </label> <input id="txtFillDate" class="date" type="text" size="15"  /><br />
         <label for="txtStartDate">Start Date: </label> <input id="txtStartDate" class="date" type="text" size="15"  /><br />
         <label for="txtComment">Comment: </label> <textarea id="txtComment" cols="30" rows="10" ></textarea><br />
         <label for="txtComment">Choose Attachment: </label> 
    <select id="optAvailablePdf">
        <%=m_AvailablePdfs %>
    </select>
         
         <br />
     </div>
     <div id="hr3">
     <br />
        <label for="">&nbsp;</label>
        <asp:FileUpload ID="FileUpload1" runat="server" Width="40em" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Upload" />
     <br />
     <br />
     <label for="">&nbsp;</label><input id="btnSave" type="button" value="Update Position" disabled="disabled" />
            <input id="btnCreate" type="button" value="Create Position" />            
            <input id="btnPrintJob" type="button" value="Print Current Job" />
            <input id="btnEmailJob" type="button" value="EMail Job to Everyone" />
            <input id="btnEmailJobToMe" type="button" value="EMail Job to Me" />
     </div>
</asp:Content>
