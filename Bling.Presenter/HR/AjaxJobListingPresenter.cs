using System;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using Bling.Repository.HR;
using Bling.Domain.Extension;
using System.Net.Mail;
using System.Configuration;

namespace Bling.Presenter.HR
{
    public class AjaxJobListingPresenter : Presenter
    {
        private IAjaxView m_View;
        private IJobDao m_Dao;

        public AjaxJobListingPresenter(IAjaxView view) : 
            this (view, new JobDao(MWDataStoreSession()))
        {
        }

        public AjaxJobListingPresenter(IAjaxView view, IJobDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetJobAsJson(int id)
        {
            Job job = GetJob(id);
            m_View.ResponseText = job.ToJson();
        }

        public Job GetJob(int id)
        {
            Job job = m_Dao.GetById(id);

            if (job == null)
                job = new Job();           

            return job;
        }

        public void SaveJob(Job job)
        {
            m_Dao.Save(job);
        }

        public void GetOpenJob()
        {
            m_View.ResponseText = Job.ToSelectHTML(m_Dao.GetOpenJobs().ToList());
        }

        public void GetCloseJob()
        {
            m_View.ResponseText = Job.ToSelectHTML(m_Dao.GetCloseJobs().ToList());
        }

        public void PrintJob(string reportName, int jobId, string pdfName)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@id", jobId)
               .ViewReport();

            crystal.Dispose();
            crystal = null;
            //new Crystal(reportName)
            //   .ConnectToSQLBeast()
            //   .SetDestinationToPDFAndRename(reportName, pdfName)
            //   .AddParameter("@id", jobId)
            //   .ViewReport();

        }

        public void EmailJob(string fileToAttach, string referralAttachment, string additionalAttachment, string currentUserEmail)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("hr@gemcorp.com");

                if (currentUserEmail != String.Empty)
                {
                    mm.To.Add(currentUserEmail);
                    mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
                }
                else
                {
                    mm.To.Add(new MailAddress("CorporateOffice@gemcorp.com"));
                    mm.Bcc.Add(new MailAddress("global@gemcorp.com"));

                    //mm.To.Add("eibus@gemcorp.com");
                }
                mm.Subject = "Job Posting";

                mm.Body = BuildMessage3().ToString();
                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(fileToAttach));
                mm.Attachments.Add(new Attachment(referralAttachment));
                if (additionalAttachment != String.Empty)
                {
                    mm.Attachments.Add(new Attachment(additionalAttachment));
                }
                string host = ConfigurationManager.AppSettings["gemhost"];
                SmtpClient client = new SmtpClient(host);
                //SmtpClient client = new SmtpClient("GEMEX07.gem.local");

                client.Send(mm);
                m_View.ResponseText = "Email sent.";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
            }
        }

        private static StringBuilder BuildMessage3()
        {
            StringBuilder body = new StringBuilder();
            body.Append(
                @"
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML xmlns=""http://www.w3.org/TR/REC-html40"" xmlns:v = 
""urn:schemas-microsoft-com:vml"" xmlns:o = 
""urn:schemas-microsoft-com:office:office"" xmlns:w = 
""urn:schemas-microsoft-com:office:word"" xmlns:m = 
""http://schemas.microsoft.com/office/2004/12/omml""><HEAD>
<META content=""text/html; charset=us-ascii"" http-equiv=Content-Type>
<META name=GENERATOR content=""MSHTML 8.00.6001.19328"">
<STYLE>@font-face {
	font-family: Wingdings;
}
@font-face {
	font-family: Cambria Math;
}
@font-face {
	font-family: Calibri;
}
@font-face {
	font-family: Tahoma;
}
@font-face {
	font-family: Vijaya;
}
@page WordSection1 {size: 8.5in 11.0in; margin: 1.0in 1.0in 1.0in 1.0in; }
P.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
LI.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
DIV.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
A:link {
	COLOR: blue; TEXT-DECORATION: underline; mso-style-priority: 99
}
SPAN.MsoHyperlink {
	COLOR: blue; TEXT-DECORATION: underline; mso-style-priority: 99
}
A:visited {
	COLOR: purple; TEXT-DECORATION: underline; mso-style-priority: 99
}
SPAN.MsoHyperlinkFollowed {
	COLOR: purple; TEXT-DECORATION: underline; mso-style-priority: 99
}
P.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
LI.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
DIV.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
SPAN.BalloonTextChar {
	FONT-FAMILY: ""Tahoma"",""sans-serif""; mso-style-priority: 99; mso-style-link: ""Balloon Text""; mso-style-name: ""Balloon Text Char""
}
P.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
LI.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
DIV.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
SPAN.EmailStyle20 {
	FONT-FAMILY: ""Calibri"",""sans-serif""; COLOR: #1f497d; mso-style-type: personal
}
SPAN.EmailStyle21 {
	FONT-FAMILY: ""Calibri"",""sans-serif""; COLOR: #1f497d; mso-style-type: personal
}
SPAN.EmailStyle23 {
	FONT-FAMILY: ""Calibri"",""sans-serif""; COLOR: #1f497d; mso-style-type: personal-reply
}
.MsoChpDefault {
	FONT-SIZE: 10pt; mso-style-type: export-only
}
DIV.WordSection1 {
	page: WordSection1
}
OL {
	MARGIN-BOTTOM: 0in
}
UL {
	MARGIN-BOTTOM: 0in
}
</STYLE>
<!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=""edit"" spidmax=""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=""edit"">
<o:idmap v:ext=""edit"" data=""1"" />
</o:shapelayout></xml><![endif]--></HEAD>
<BODY lang=EN-US link=blue vLink=purple>
<DIV dir=ltr align=left><STRONG><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt"">The 
attached job opening has recently been posted. Please read the guidelines below 
for internal transfers:</SPAN></STRONG>&nbsp;<o:p></o:p></DIV>
<DIV class=WordSection1>
<UL type=disc>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo1"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Open positions will 
  typically be posted on bulletin boards in the work or break areas or you can 
  view the website at <A 
  href=""http://www.gemcorp.com"">www.gemcorp.com</A>;</SPAN> <SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt""><o:p></o:p></SPAN>
  <LI 
  style=""COLOR: #1f497d; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo1"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: windowtext; FONT-SIZE: 10pt"">You 
  may apply by notifying your supervisor and contacting your Human Resources 
  dept. at <A href=""mailto:hr@gemcorp.com"">hr@gemcorp.com</A>;</SPAN><SPAN 
  style=""COLOR: windowtext""> </SPAN><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt""><o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo1"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">You may not apply 
  for a new position<SPAN style=""COLOR: #1f497d"">:</SPAN></SPAN> <SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt""><o:p></o:p></SPAN></LI></UL>
<P 
style=""TEXT-INDENT: -0.25in; MARGIN-LEFT: 1in; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level2 lfo1"" 
class=MsoNormal><![if !supportLists]><SPAN 
style=""FONT-FAMILY: Wingdings; FONT-SIZE: 10pt""><SPAN 
style=""mso-list: Ignore"">v<SPAN style=""FONT: 7pt 'Times New Roman'"">&nbsp; 
</SPAN></SPAN></SPAN><![endif]><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">If you are in a 
probationary period</SPAN> <SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt""><o:p></o:p></SPAN></P>
<P 
style=""TEXT-INDENT: -0.25in; MARGIN-LEFT: 1in; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level2 lfo1"" 
class=MsoNormal><![if !supportLists]><SPAN 
style=""FONT-FAMILY: Wingdings; FONT-SIZE: 10pt""><SPAN 
style=""mso-list: Ignore"">v<SPAN style=""FONT: 7pt 'Times New Roman'"">&nbsp; 
</SPAN></SPAN></SPAN><![endif]><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Have been in your 
current job less than six months, or</SPAN> <SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt""><o:p></o:p></SPAN></P>
<P 
style=""TEXT-INDENT: -0.25in; MARGIN-LEFT: 1in; mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level2 lfo1"" 
class=MsoNormal><![if !supportLists]><SPAN 
style=""FONT-FAMILY: Wingdings; FONT-SIZE: 10pt""><SPAN 
style=""mso-list: Ignore"">v<SPAN style=""FONT: 7pt 'Times New Roman'"">&nbsp; 
</SPAN></SPAN></SPAN><![endif]><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Have received a 
written warning or suspension due to poor performance or poor conduct within the 
last six months.<o:p></o:p></SPAN></P>
<P style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" 
class=MsoNormal><B><U><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt"">Employee 
Referral Program<o:p></o:p></SPAN></U></B></P>
<P class=Default><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">The Employee Referral 
Program will reward employees by paying $250 to $2,000 (depending on the type of 
position being filled) to those who refer successful candidates to fill 
corporate job openings. While payment is only made for corporate positions, all 
branch and corporate employees are eligible to refer candidates. 
<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">Referral 
Compensation Structure will be defined as: </SPAN></B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt""><o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$250 &#8211; 
Standard position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$500 &#8211; 
Supervisor position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$1,000 
&#8211; Manager position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$2,000 
&#8211; Specialized position (underwriter, funder, programmer)<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt""><o:p>&nbsp;</o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt"">For full 
details to the Employee Referral Program, please read the attached 
policy.</SPAN></B><B><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: red; FONT-SIZE: 10pt""><o:p></o:p></SPAN></B></P>
<DIV 
style=""BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 1pt; PADDING-LEFT: 0in; PADDING-RIGHT: 0in; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0in"">
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P></DIV>
<DIV>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Vijaya','sans-serif'; COLOR: #5f497a; FONT-SIZE: 16pt"">Stephanie 
Harrison<o:p></o:p></SPAN></B></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Human 
Resources &amp; Payroll Manager<o:p></o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Golden 
Empire Mortgage, Inc. &#8211; Celebrating 25 Years of 
Excellence<o:p></o:p></SPAN></B></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Ph: 
661.328.1600 ext 1244<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Fax: 
661.334-3244<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt""><A 
href=""mailto:sharrison@gemcorp.com"">sharrison@gemcorp.com</A><o:p></o:p></SPAN></P></DIV>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P></DIV></BODY></HTML>

");
            return body;
        }

        private static StringBuilder BuildMessage2()
        {
            StringBuilder body = new StringBuilder();

            body.Append(
            @"
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML xmlns=""http://www.w3.org/TR/REC-html40"" xmlns:v = 
""urn:schemas-microsoft-com:vml"" xmlns:o = 
""urn:schemas-microsoft-com:office:office"" xmlns:w = 
""urn:schemas-microsoft-com:office:word"" xmlns:m = 
""http://schemas.microsoft.com/office/2004/12/omml""><HEAD>
<META content=""text/html; charset=us-ascii"" http-equiv=Content-Type>
<META name=GENERATOR content=""MSHTML 8.00.6001.18702"">
<STYLE>@font-face {
	font-family: Wingdings;
}
@font-face {
	font-family: Cambria Math;
}
@font-face {
	font-family: Calibri;
}
@font-face {
	font-family: Tahoma;
}
@font-face {
	font-family: Vijaya;
}
@font-face {
	font-family: Papyrus;
}
@page WordSection1 {size: 8.5in 11.0in; margin: 1.0in 1.0in 1.0in 1.0in; }
P.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
LI.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
DIV.MsoNormal {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; FONT-SIZE: 12pt
}
A:link {
	COLOR: blue; TEXT-DECORATION: underline; mso-style-priority: 99
}
SPAN.MsoHyperlink {
	COLOR: blue; TEXT-DECORATION: underline; mso-style-priority: 99
}
A:visited {
	COLOR: purple; TEXT-DECORATION: underline; mso-style-priority: 99
}
SPAN.MsoHyperlinkFollowed {
	COLOR: purple; TEXT-DECORATION: underline; mso-style-priority: 99
}
P.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
LI.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
DIV.MsoAcetate {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Tahoma"",""sans-serif""; FONT-SIZE: 8pt; mso-style-priority: 99; mso-style-link: ""Balloon Text Char""
}
SPAN.EmailStyle17 {
	FONT-FAMILY: ""Calibri"",""sans-serif""; COLOR: #1f497d; mso-style-type: personal
}
SPAN.BalloonTextChar {
	FONT-FAMILY: ""Tahoma"",""sans-serif""; mso-style-priority: 99; mso-style-link: ""Balloon Text""; mso-style-name: ""Balloon Text Char""
}
SPAN.EmailStyle20 {
	FONT-FAMILY: ""Calibri"",""sans-serif""; COLOR: #1f497d; mso-style-type: personal-reply
}
P.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
LI.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
DIV.Default {
	MARGIN: 0in 0in 0pt; FONT-FAMILY: ""Times New Roman"",""serif""; COLOR: black; FONT-SIZE: 12pt; mso-style-name: Default
}
.MsoChpDefault {
	FONT-SIZE: 10pt; mso-style-type: export-only
}
DIV.WordSection1 {
	page: WordSection1
}
OL {
	MARGIN-BOTTOM: 0in
}
UL {
	MARGIN-BOTTOM: 0in
}
</STYLE>
<!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=""edit"" spidmax=""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=""edit"">
<o:idmap v:ext=""edit"" data=""1"" />
</o:shapelayout></xml><![endif]--></HEAD>
<BODY lang=EN-US link=blue vLink=purple>
<DIV dir=ltr align=left><SPAN><SPAN class=449144520-12102012><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt""><STRONG>The 
attached job opening has recently been posted. Please read the guidelines below 
for internal transfers:</STRONG></SPAN>&nbsp;</SPAN><o:p></o:p></SPAN></DIV>
<DIV class=WordSection1>
<UL type=disc>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Open positions will 
  typically be posted on bulletin boards in the work or break areas or you can 
  view the website at <A 
  href=""http://www.gemcorp.com"">www.gemcorp.com</A>;<o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">You may apply by 
  notifying your supervisor and contacting your Human Resources dept. at <A 
  href=""mailto:hr@gemcorp.com"">hr@gemcorp.com</A>;<o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">You may not apply 
  for a new position<o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">If you are in a 
  probationary period<o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Have been in your 
  current job less than six months, or<o:p></o:p></SPAN>
  <LI 
  style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; mso-list: l0 level1 lfo3"" 
  class=MsoNormal><SPAN 
  style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">Have received a 
  written warning or suspension due to poor performance or poor conduct within 
  the last six months.<o:p></o:p></SPAN></LI></UL>
<P style=""mso-margin-top-alt: auto; mso-margin-bottom-alt: auto"" 
class=MsoNormal><B><U><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt"">Employee 
Referral Program<o:p></o:p></SPAN></U></B></P>
<P class=Default><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 10pt"">The Employee Referral 
Program will reward employees by paying $250 to $2,000 (depending on the type of 
position being filled) to those who refer successful candidates to fill 
corporate job openings. While payment is only made for corporate positions, all 
branch and corporate employees are eligible to refer candidates. 
<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">Referral 
Compensation Structure will be defined as: </SPAN></B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt""><o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$250 &#8211; 
Standard position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$500 &#8211; 
Supervisor position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$1,000 
&#8211; Manager position <o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt"">$2,000 
&#8211; Specialized position (underwriter, funder, programmer)<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: black; FONT-SIZE: 10pt""><o:p>&nbsp;</o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Arial','sans-serif'; COLOR: red; FONT-SIZE: 10pt"">For full 
details to the Employee Referral Program, please read the attached 
policy.</SPAN></B><B><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: red; FONT-SIZE: 10pt""><o:p></o:p></SPAN></B></P>
<DIV 
style=""BORDER-BOTTOM: windowtext 1pt solid; BORDER-LEFT: medium none; PADDING-BOTTOM: 1pt; PADDING-LEFT: 0in; PADDING-RIGHT: 0in; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0in; mso-element: para-border-div"">
<P 
style=""BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; PADDING-BOTTOM: 0in; PADDING-LEFT: 0in; PADDING-RIGHT: 0in; BORDER-TOP: medium none; BORDER-RIGHT: medium none; PADDING-TOP: 0in"" 
class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P></DIV>
<DIV>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Vijaya','sans-serif'; COLOR: #5f497a; FONT-SIZE: 16pt"">Stephanie 
Harrison<o:p></o:p></SPAN></B></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Human 
Resources &amp; Payroll Manager<o:p></o:p></SPAN></P>
<P class=MsoNormal><B><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Golden 
Empire Mortgage, Inc. &#8211; Celebrating 25 Years of 
Excellence<o:p></o:p></SPAN></B></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Ph: 
661.328.1600 ext 1244<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">Fax: 
661.334-3244<o:p></o:p></SPAN></P>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 10pt"">sharrison@gemcorp.com<o:p></o:p></SPAN></P></DIV>
<P class=MsoNormal><SPAN 
style=""FONT-FAMILY: 'Calibri','sans-serif'; COLOR: #1f497d; FONT-SIZE: 11pt""><o:p>&nbsp;</o:p></SPAN></P></DIV></BODY></HTML>
            
        ");

            return body;

        }
        private static StringBuilder BuildMessage()
        {
            StringBuilder body = new StringBuilder();
            body.Append("<font face='Arial' size='2'>");
            body.Append("<font color='#0000ff'>");
            body.Append("The attached job opening has recently been posted. Please read the guidelines below for internal transfers:");
            body.Append("</font>");
            body.Append("<br/><br/><ul>");
            body.Append("<li>Open positions will typically be posted on bulletin boards in the work or break areas or you can view the website at <a href='http://www.gemcorp.com'>www.gemcorp.com</a>;</li>");
            body.Append("<li>You may apply by notifying your supervisor and contacting your Human Resources dept. at <a href='mailto:hr@gemcorp.com'>hr@gemcorp.com</a>;</li>");
            body.Append("<li>You may not apply for a new position</li>");
            body.Append("<li>If you are in a probationary period</li>");
            body.Append("<li>Have been in your current job less than six months, or</li>");
            body.Append("<li>Have received a written warning or suspension due to poor performance or poor conduct within the last six months.</li>");
            body.Append("</ul>");
            body.Append("</font>");
            body.Append("<FONT face=Papyrus color=#0000ff>Stephanie Harrison</FONT></SPAN> <BR><SPAN lang=en-us><B><FONT face=Arial color=#0000ff size=2>GEM Mortgage, Inc.</FONT></B></SPAN> <BR><SPAN lang=en-us><FONT face=Arial size=2>Human Resources</FONT></SPAN> <FONT face=Arial size=2>Manager</FONT><BR><SPAN lang=en-us><FONT face=Arial size=2>Ph 661-328-1600 ext 1244</FONT></SPAN> <BR><SPAN lang=en-us><FONT face=Arial size=2>Fax 661-334-3244</FONT></SPAN> <BR><SPAN lang=en-us><FONT face=Arial size=2>sharrison@gemcorp.com</FONT></SPAN>");
            return body;
        }
    }
}
