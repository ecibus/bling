using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Bling.Domain.HR
{
    public class ExpiredLicenseEmail    
    {
        private List<ExpiredLicense> m_Lists;
        private DateTime m_Deadline;
        private string m_AttachedFile;

        public ExpiredLicenseEmail(string attachedFile)
        {
            m_AttachedFile = attachedFile;
        }


        public void Send(List<ExpiredLicense> list, DateTime deadline)
        {
            m_Lists = list;
            m_Deadline = deadline;
            
            if (m_Lists[0].BranchEMail == String.Empty)
            	return;
            
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("HR@GemCorp.com");
            //mm.To.Add(new MailAddress("eibus@gemcorp.com"));

            mm.To.Add(new MailAddress(m_Lists[0].BranchEMail.ToLower()));
            m_Lists.ForEach(x => mm.CC.Add(new MailAddress(x.EMail.ToLower())));
            mm.CC.Add(new MailAddress("lmoe@gemcorp.com"));
            mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
            mm.Subject = "MANDATORY CONTINUED EDUCATION OR DRE LICENSE DEADLINE FOR LOAN ORIGINATORS";

            //StringBuilder destination = new StringBuilder();
            //destination.AppendFormat("To: {0}<br/>", m_Lists[0].BranchEMail.ToLower());
            //destination.Append("Cc: ");            
            //m_Lists.ForEach(x => destination.AppendFormat("{0} ", x.EMail.ToLower()));
            //destination.Append("<br/><br/>");
            //mm.Body = destination.ToString() + BuildMessage();

            mm.Body = BuildMessage();
            mm.IsBodyHtml = true;
            
            if (m_AttachedFile != String.Empty)
                mm.Attachments.Add(new Attachment(m_AttachedFile));

            //SmtpClient client = new SmtpClient("GEMEX07.gem.local");
            string host = ConfigurationManager.AppSettings["gemhost"];
            SmtpClient client = new SmtpClient(host);

            client.Send(mm);
            //m_View.ResponseText = "Email sent.";
        }
        
        private string BuildMessage()
        {
            StringBuilder msg = new StringBuilder();
            
            msg.Append("<font face='Arial' size='3'>");
            msg.Append("&nbsp;&nbsp;&nbsp;The following employees have been employed for longer than 30 days and currently do not have a valid DRE License OR California mandatory Agency & Ethics continued education certificates on file with Human Resources.");

            msg.Append("<br/><br/>");

            msg.Append("<table cellspacing='0' border='1' style='background: yellow; font-family:Arial; font-size=.9em'>");
            m_Lists.ForEach(x => msg.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                x.EmployeeId, x.EmployeeName, x.BranchNo, x.Branch                
                ));
            msg.Append("</table>");

            msg.Append("<br/>");
            msg.Append("&nbsp;&nbsp;&nbsp;Both DRE Licenses & the Continued Education Certificates are valid for 4 years, and must be renewed every 4 years from the date of issue.   Anytime certificates or a license is renewed, the  Branch Manager/Loan  Officer must submit a copy to Human Resources to have on file & to update personnel records.");

            msg.Append("<br/><br/>");
            msg.Append("&nbsp;&nbsp;&nbsp;Employees must submit copies of their Continued Education Certificates of 3 hours Agency & 3 hours Ethics, OR a valid DRE License ");
            msg.Append("<font color='red' style='font-weight:bold'>");
            msg.AppendFormat("no later than {0}", m_Deadline.ToLongDateString());
            msg.Append("</font>");
            msg.Append(".  If the employee is no longer with your branch, please submit the attached employee change notice.   For those whose license or certificates are expired and for those who have never funded a loan on our system (we know this because we hold all commissions until CE is completed), you have until ");
            msg.Append("<font color='red'>");
            msg.AppendFormat("{0} ", m_Deadline.ToString("MMMMM dd, yyyy"));
            msg.Append("</font>");
            msg.Append("to either complete the Continuing Ed, voluntarily quit, or be terminated by the Branch Manager. If by this date,  the Loan Officer is still employed and has not completed the requirements, they will be terminated by Corporate for failure to be in compliance.");

            msg.Append("<br/><br/>");
            msg.Append("&nbsp;&nbsp;&nbsp;If the employee is not in possession of a valid DRE license, please have them use the link below to complete the 3 hours Agency & 3 hours Ethics bundle located on Financial Strategies.  This link can also be found on the GEM website under Human Resources.");
            
            msg.Append("<br/><br/>");
            msg.Append("<a href='https://www.mymortgagetrainer.com/index.php?option=com_wrapper&Itemid=94'>https://www.mymortgagetrainer.com/index.php?option=com_wrapper&Itemid=94</a>");

            msg.Append("<br/><br/>");
            msg.Append("&nbsp;&nbsp;&nbsp;If you have any questions, please feel free to contact me, Stephanie Harrison, or David Chesney.");

            msg.Append("<br/><br/>");
            msg.Append("Thank you,");

            msg.Append("<br/><br/>");

            msg.Append("<font color='green'>Jennifer Hooker</font><br/>");
            msg.Append("<font size='2'>");
            msg.Append("Golden Empire Mortgage<br/>");
            msg.Append("HR/Payroll Department<br/>");
            msg.Append("661-283-1203 Phone<br/>");
            msg.Append("661-379-6632 Fax<br/>");
            msg.Append("</font>");
            
            msg.Append("</font>");
            
            return msg.ToString();
        }
    }
}
