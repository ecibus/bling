<%@ Page Title="" Language="C#" MasterPageFile="~/Bling.Master" AutoEventWireup="true" CodeBehind="CashDepositEntry.aspx.cs" Inherits="Bling.Web.Accounting.CashDepositEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/blueprint/grid.css" rel="stylesheet" type="text/css" />
    <script src="js/CashDepositEntry.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container showgrid1 inline2">

        <h1>Cash Deposit Entry</h1>
        

        <div class="span-24">
            Deposit Date
            <input id="txtDepositDate" value="<%=DepositDate %>" type="text" />
            <input id="btnLoad" type="button" value="Load Data" />
        </div>
        <br />
        <table class="t1">
            <thead>
                <tr class='yellow'>
                    <td>Loan Number</td>
                    <td>Branch No</td>                    
                    <td>Account No</td>
                    <td>Amount</td>
                    <td>Bank Account</td>
                    <td></td>
                </tr>
                <tr>
                    <td ><input id="txtLoanNo" type="text" class="span-3" /></td>                    
                    <td ><input id="txtBranchNo" type="text" class="span-2"/></td>
                    
                    <td>
                        <%=Account %>                       
                    </td>
                    <td><input id="txtDollarAmount" type="text"  class="span-2" /></td>
                    <td>
                        <select id="ddlBankAccount">
                            <option>1001</option>
                            <option>1011</option>
                            <option>1002 R Trust</option>                            
                            <option selected="selected"">1003 R General</option>
                            <option>1004 Trust</option>
                            <option>1005 General</option>
                            <option>1008-990 Discovery</option>
                            <option>Credit Card</option>
                            <option>1012 Impounds</option>
                            <option>1013 MIP/PMI/VAFF</option>
                            <option>1014 Other</option>
                        </select>
                    </td>
                    <td>
                        <input id="btnAdd" type="button" value="Add" /></td>
                </tr>
  
            </thead>
            <tbody id="cashdeposit">
                
            </tbody>
        </table>
        <br />
        <div class="span-24">
            <span class="span-4 number">Number of Entries:</span>
            <span class="span-2 number" id="count"></span>            
        </div>
        
        <br /><br />
        <div class="span-24">
            <span class="span-4 number">Total:</span>
            <span class="span-2 number" id="Total"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1001 Total:</span>
            <span class="span-2 number" id="Total1001"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1011 Total:</span>
            <span class="span-2 number" id="Total1011"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1003 Total:</span>
            <span class="span-2 number" id="Total1003"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1002 Total:</span>
            <span class="span-2 number" id="Total1002"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1008-990 Total:</span>
            <span class="span-2 number" id="Total1008"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">Credit Card:</span>
            <span class="span-2 number" id="TotalCreditCard"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1012 Total:</span>
            <span class="span-2 number" id="Total1012"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1013 Total:</span>
            <span class="span-2 number" id="Total1013"></span>            
        </div>
        <div class="span-24">
            <span class="span-4 number">1014 Total:</span>
            <span class="span-2 number" id="Total1014"></span>            
        </div>
    </div>
</asp:Content>
