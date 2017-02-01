using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Bling.Domain.HR
{
    public class TimeCardSubmit
    {
        public virtual int Id { get; set; }
        //public virtual string EmployeeId { get; set; }
        public virtual string Username { get; set; }
        public virtual string Manager { get; set; }
        public virtual int Month { get; set; }
        public virtual int Year { get; set; }
        public virtual int MonthPart { get; set; }
        public virtual bool Submitted { get; set; }
        public virtual bool Accepted { get; set; }
        public virtual DateTime? AcceptedOn { get; set; }
        public virtual string AcceptedBy { get; set; }

        public virtual void SendEmailToManager(string managerEmail, string employeeName, int month, int year)
        {
            string host = ConfigurationManager.AppSettings["gemhost"];
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("hr@gemcorp.com");
            //mm.To.Add(new MailAddress(managerEmail));
            mm.To.Add(managerEmail.Replace(";", ","));
            //mm.To.Add(new MailAddress("eibus@gemcorp.com"));
            if (managerEmail.ToLower() != "eibus@gemcorp.com")
            {
                mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
            }

            mm.Subject = "You have a Time Sheet to Approve";
            mm.IsBodyHtml = true;

            //mm.Body = GetMessageForManager(employeeName, month, year);

            SmtpClient client = new SmtpClient(host);

            client.Send(mm);
            mm.Dispose();
        }

        public virtual void SendEmailToEmployee(string email, int month, int year, string rejector, string rejectorEmail)
        {
            string host = ConfigurationManager.AppSettings["gemhost"];

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("hr@gemcorp.com");
            mm.To.Add(new MailAddress(email));
            mm.CC.Add(new MailAddress(rejectorEmail));
            if (email.ToLower() != "eibus@gemcorp.com")
            {
                mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
            }

            mm.Subject = "Time Sheet Rejected by HR.";
            mm.IsBodyHtml = true;

            mm.Body = GetRejectionMessageForEmployee(rejector, month, year);

            SmtpClient client = new SmtpClient(host);
            NetworkCredential netCre =
                   new NetworkCredential("eibus", "Cordero01");
            client.Credentials = netCre;
            client.Send(mm);
            mm.Dispose();
        }

        private string GetRejectionMessageForEmployee(string rejector, int month, int year)
        {
            StringBuilder message = new StringBuilder();

            message.AppendFormat("<html>");

            message.AppendFormat("<head>");
            message.AppendFormat("<style type='text/css'>");
            message.AppendFormat("body, table {{	font: 14px/24px Verdana, Arial, Helvetica, sans-serif; 	border-collapse: collapse; }}");
            message.AppendFormat("     table td {{	border-bottom: 1px solid #CCC;	padding: 0.0em 0.3em;	white-space: nowrap;	}} ");
            message.AppendFormat("  </style>");
            message.AppendFormat("</head>");

            message.AppendFormat("<body>");

            message.AppendFormat("Your Time Sheet for {0} {1} has been rejected by {2}.<br />",
                new DateTime(year, month, 1).ToString("MMMMM"), year, rejector);
            message.AppendFormat("Please edit your Time Sheet and submit it again for approval.");
            message.AppendFormat("</body>");
            message.AppendFormat("</html>");

            return message.ToString();
        }
    }
}
