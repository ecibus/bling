using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Bling.Domain.HR;
using Bling.Repository;
using Bling.Repository.HR;
using Bling.Presenter.Accounting;
using System.Net;

namespace Bling.Presenter.HR
{
    public class AjaxProcessLOCommissionPresenter : Presenter
    {
        private IAjaxView m_View;
        private ILOCommissionDao m_Dao;
        private IBrokerDao m_BrokerDao;
        private IUserInfoDao m_UserInfoDao;

        public AjaxProcessLOCommissionPresenter(IAjaxView view)
            : this(view, new LOCommissionDao(DMDDataSession()), new BrokerDao(DMDDataSession()), new UserInfoDao(DMDDataSession()))
        {
        }

        public AjaxProcessLOCommissionPresenter(IAjaxView view, ILOCommissionDao dao, IBrokerDao brokerDao, IUserInfoDao userInfoDao)
        {
            m_View = view;
            m_Dao = dao;
            m_BrokerDao = brokerDao;
            m_UserInfoDao = userInfoDao;
        }

        public void LogProcessCommission(string employId, string payDate, string fundedDate)
        {
            m_Dao.LogProcessCommission(employId, payDate, fundedDate);
        }

        public void CreateLOCommission(string payDate, string fundedDate, string isWeekly)
        {
            //System.Threading.Thread.Sleep(2000);
            SaveCommission(m_Dao.GetLOCommission(payDate, fundedDate, isWeekly));
        }

        public void CreateLOCommissionForOldLoans(string payDate, string fundedDate)
        {
            //System.Threading.Thread.Sleep(2000);
            SaveCommission(m_Dao.GetLOCommissionForOldLoans(payDate, fundedDate));
        }

        public void CreateManagerCommission(string payDate, string fundedDate)
        {
            //System.Threading.Thread.Sleep(2000);
            //SaveCommission(m_Dao.GetManagerCommission(payDate, fundedDate));            
        }

        public void CreateManagerOverride(string payDate, string fundedDate)
        {
            //System.Threading.Thread.Sleep(2000);
            SaveCommission(m_Dao.GetManagerOverride(payDate, fundedDate));
        }

        public int GenerateCommissionReport(string reportName, string pdfName, string payDate, 
            string fundedAsOf, string branchno, string isWeekly, bool generateEmpty)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@paydate", payDate)
               .AddParameter("@fundedAsOf", fundedAsOf)
               .AddParameter("@branchno", branchno)
               .AddParameter("@isWeekly", Convert.ToBoolean(isWeekly))
               .ViewReport(generateEmpty);

            crystal.Dispose();

            int recCount = crystal.NumberOfRecordsSelected;

            return recCount;
        }

        public int GenerateLOCommissionReport(string reportName, string pdfName, string payDate,
            string fundedAsOf, string employId, string isWeekly, bool generateEmpty)
        {
            //CrystalForCommission c = new CrystalForCommission();

            //return c.GenerateLOCommission(reportName, pdfName, payDate, fundedAsOf, employId, isWeekly, generateEmpty);

            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@paydate", payDate)
               .AddParameter("@fundedAsOf", fundedAsOf)
               .AddParameter("@employId", employId)
               .AddParameter("@isWeekly", Convert.ToBoolean(isWeekly))
               .ViewReport(generateEmpty);

            crystal.Dispose();

            int recCount = crystal.NumberOfRecordsSelected;

            crystal = null;
            return recCount;
        }

        public void RefreshCommissionDataFromTracker(string paymentDate)
        {
            m_Dao.RefreshCommissionDataFromTracker(paymentDate);
        }

        public IList<string> GetBranchWithCommission(string paymentDate, string fundedDate, string isWeekly)
        {
            return m_Dao.GetBranchWithCommission(paymentDate, fundedDate, isWeekly);
            //IList<string> branches = new List<string>();
            //m_BrokerDao.GetActiveBroker().OrderBy(x => x.CostCenter).ToList().ForEach(x => branches.Add(x.CostCenter));            
            //return branches;
        }

        public IList<string> GetBranchWithTimeCard(string month, string year)
        {
            return m_Dao.GetBranchWithTimecard(month, year);
        }

        public IList<string> GetLOWithCommission(string branchNo, string payDate, string fundedAsOf, string isWeekly)
        {
            return m_Dao.GetLOWithCommission(branchNo, payDate, fundedAsOf, isWeekly);
        }

        public void GenerateCommissionSummaryReport(string reportName, string pdfName, string payDate, string isWeekly)
        {
            new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@paydate", payDate)
               .AddParameter("@isWeekly", Convert.ToBoolean(isWeekly))               
               .ViewReport();
        }

        public void EMailCommissionReport(string branchNo, string fileToAttach, string payDate, string fundedAsOf,
            string senderEmail, string senderFullname, string deadline, bool isWeekly, bool sendToManager)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(senderEmail, senderFullname);

                if (branchNo == "329")
                {
                    mm.To.Add(new MailAddress("pferrel@gemcorp.com"));
                }
                else
                {
                    if (sendToManager)
                    {
                        m_BrokerDao.GetBranchManagerEmailForCommission(branchNo)
                            .ForEach(x => mm.To.Add(x));
                        mm.CC.Add(senderEmail);
                    }
                    else
                    {
                        mm.To.Add(new MailAddress(senderEmail));
                        mm.Body = GetRecipient(branchNo);
                    }

                    if (senderEmail.ToLower() != "eibus@gemcorp.com")
                    {
                        //mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
                    }
                }

                //mm.Subject = isWeekly ? "Commission Report for Branch " + branchNo :
                //    payDate + " SEMI MONTHLY COMMISSION SUMMARY REVIEW";

                mm.Subject = isWeekly ? payDate + " Weekly Commission Summary Review for Branch " + branchNo :
                                      payDate + " SEMI MONTHLY COMMISSION SUMMARY REVIEW";



                if (isWeekly)
                {
                    mm.Body += m_Dao.IsBranchContainsOnHold(branchNo, fundedAsOf) ?
                        OnHoldMessage(Convert.ToDateTime(payDate), Convert.ToDateTime(deadline)) :
                        ApproveMessage(Convert.ToDateTime(payDate), Convert.ToDateTime(deadline));
                }
                else
                {
                    mm.Body += m_Dao.IsBranchContainsOnHold(branchNo, fundedAsOf) ?
                        SalaryOnHoldMessage(Convert.ToDateTime(payDate), Convert.ToDateTime(deadline)) :
                        SalaryApprovedMessage(Convert.ToDateTime(payDate), Convert.ToDateTime(deadline));
                }

                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(fileToAttach));

                string host = ConfigurationManager.AppSettings["gemhost"];
                SmtpClient client = new SmtpClient(host);

                //for testing
                //client.Credentials = CredentialCache.DefaultNetworkCredentials;

                //client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //client.PickupDirectoryLocation = @"c:\mail";

                client.Send(mm);
                mm.Dispose();

                m_Dao.SaveEmailTracking("", branchNo, payDate, fundedAsOf, isWeekly ? "1" : "0");

                m_View.ResponseText = "";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
                LogError(ex);
            }
        }

        public bool IsLOEmailSent(string lo, string paydate, string fundedAsOf, bool isWeekly)
        {
            return m_Dao.IsLoEMailSent(lo, paydate, fundedAsOf, isWeekly); ;
        }

        public bool IsBranchEmailSent(string branch, string paydate, string fundedAsOf, bool isWeekly)
        {
            return m_Dao.IsBranchEMailSent(branch, paydate, fundedAsOf, isWeekly); ;
        }

        public void ClearBranchEmailTracking(string paydate, string fundedAsOf, bool isWeekly)
        {
            m_Dao.ClearBranchEmailTracking(paydate, fundedAsOf, isWeekly);
        }

        public void ClearLOEmailTracking(string paydate, string fundedAsOf, bool isWeekly)
        {
            m_Dao.ClearLOEmailTracking(paydate, fundedAsOf, isWeekly);
        }

        public bool IsFirstEmailToSend(string employId, string intervalId)
        {
            return m_Dao.IsFirstEmailToSend(employId, intervalId);
        }

        public string GetUnsendLOEmail(string branchno, string paydate, string fundedAsOf, string isWeekly,
            string employId, string intervalId)
        {
            return m_Dao.GetUnsendLO(branchno, paydate, fundedAsOf, isWeekly, employId, intervalId); 
        }

        public void SaveSentLOEmail(string employId, string intervalId, string datakey, string datavalue)
        {
            m_Dao.SaveSentEmail(employId, intervalId, datakey, datavalue);
        }

        public void EMailLOCommissionReport(string employId, string fileToAttach, string payDate, string fundedAsOf,
            string senderEmail, string senderFullname, string deadline, bool isWeekly, bool sendToManager, string branchNo)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(senderEmail, senderFullname);

                //string recipient = m_UserInfoDao.GetById(employId).EMail;
                string recipient = m_UserInfoDao.GetEmailByLoginName(employId);

                if (branchNo == "329")
                {
                    mm.To.Add(new MailAddress("pferrel@gemcorp.com"));
                }
                else
                {
                    if (sendToManager)
                    {
                        mm.To.Add(recipient);
                        mm.CC.Add(senderEmail);
                        //mm.CC.Add("jhooker@gemcorp.com");
                    }
                    else
                    {
                        mm.To.Add(new MailAddress(senderEmail));
                        mm.Body = GetRecipientForLO(recipient);
                    }
                }

                if (senderEmail.ToLower() != "eibus@gemcorp.com")
                {
                    //mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
                }

                mm.Subject = payDate + " Final Weekly Commission Report";
                mm.Body += payDate + " Final Weekly Commission Report";

                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(fileToAttach));

                string host = ConfigurationManager.AppSettings["gemhost"];
                SmtpClient client = new SmtpClient(host);

                client.Send(mm);
                mm.Dispose();

                m_Dao.SaveEmailTracking(employId, "", payDate, fundedAsOf, isWeekly ? "1" : "0");
                m_View.ResponseText = "";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
                //LogError(ex);
                throw ex;
            }
        }


        public void EMailISHReport(string branchNo, string fileToAttach, string month, string year,
            string senderEmail, string senderFullname, string deadline, bool sendToManager)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(senderEmail, senderFullname);

                if (sendToManager)
                {
                    m_BrokerDao.GetBranchManagerEmailForCommission(branchNo)
                        .ForEach(x => mm.To.Add(x));
                    mm.CC.Add(senderEmail);
                }
                else
                {
                    mm.To.Add(new MailAddress(senderEmail));
                    mm.Body = GetRecipient(branchNo);
                }

                if (senderEmail.ToLower() != "eibus@gemcorp.com")
                {
                    //mm.Bcc.Add(new MailAddress("eibus@gemcorp.com"));
                }

                mm.Subject = "Inside Sales Hourly Report for Branch " + branchNo;

                mm.Body += "";

                mm.IsBodyHtml = true;
                mm.Attachments.Add(new Attachment(fileToAttach));

                string host = ConfigurationManager.AppSettings ["gemhost"];
                SmtpClient client = new SmtpClient(host);

                client.Send(mm);
                mm.Dispose();

                m_View.ResponseText = "";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
                LogError(ex);
            }
        }

        public int GenerateISHReport(string reportName, string pdfName, string branchNo,
            string month, string year, bool generateEmpty)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@branchno", branchNo)
               .AddParameter("@month", month)
               .AddParameter("@year", year)
               .ViewReport(generateEmpty);

            crystal.Dispose();

            int recCount = crystal.NumberOfRecordsSelected;

            return recCount;
        }

        public void ViewCommissionsAccrual(string reportName, string pdfName, string endingDate)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@end", endingDate)               
               .ViewReport();

            crystal.Dispose();
        }

        private void SaveCommission(IList<LOCommission> list)
        {
            foreach (var commission in list)
            {
                m_Dao.Save(commission);
            }
        }

        private string GetRecipient(string branchNo)
        {
            StringBuilder message = new StringBuilder();
            message.Append("Email will be send to:<br />");
            m_BrokerDao.GetBranchManagerEmailForCommission(branchNo)
                .ForEach(x => message.AppendFormat("{0}<br />", x));
            message.Append("-------------------------------<br />");
            return message.ToString();
        }

        private string GetRecipientForLO(string email)
        {
            StringBuilder message = new StringBuilder();
            message.Append("Email will be send to:<br />");
            message.AppendFormat("{0}<br />", email);
            message.Append("-------------------------------<br />");
            return message.ToString();
        }

        private string GetOrdinal(int num)
        {
            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num.ToString() + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num.ToString() + "st";
                case 2:
                    return num.ToString() + "nd";
                case 3:
                    return num.ToString() + "rd";
                default:
                    return num.ToString() + "th";
            }
        }

        private string ApproveMessageWrong(DateTime payDate, DateTime deadline)
        {

            return String.Format(@"

<html xmlns:o=""urn:schemas-microsoft-com:office:office""
xmlns:w=""urn:schemas-microsoft-com:office:word""
xmlns:m=""http://schemas.microsoft.com/office/2004/12/omml""
xmlns=""http://www.w3.org/TR/REC-html40"">

<head>
<meta http-equiv=Content-Type content=""text/html; charset=windows-1252"">
<meta name=ProgId content=Word.Document>
<meta name=Generator content=""Microsoft Word 15"">
<meta name=Originator content=""Microsoft Word 15"">
<link rel=File-List
href=""FW%20On%20Hold%20Commission%20report%20verbiage_files/filelist.xml"">
<link rel=Edit-Time-Data
href=""FW%20On%20Hold%20Commission%20report%20verbiage_files/editdata.mso"">
<link rel=themeData
href=""FW%20On%20Hold%20Commission%20report%20verbiage_files/themedata.thmx"">
<link rel=colorSchemeMapping
href=""FW%20On%20Hold%20Commission%20report%20verbiage_files/colorschememapping.xml"">
<!--[if gte mso 9]><xml>
 <w:WordDocument>
  <w:Zoom>0</w:Zoom>
  <w:TrackMoves/>
  <w:TrackFormatting/>
  <w:ValidateAgainstSchemas/>
  <w:SaveIfXMLInvalid>false</w:SaveIfXMLInvalid>
  <w:IgnoreMixedContent>false</w:IgnoreMixedContent>
  <w:AlwaysShowPlaceholderText>false</w:AlwaysShowPlaceholderText>
  <w:DoNotPromoteQF/>
  <w:LidThemeOther>EN-US</w:LidThemeOther>
  <w:LidThemeAsian>X-NONE</w:LidThemeAsian>
  <w:LidThemeComplexScript>X-NONE</w:LidThemeComplexScript>
  <w:Compatibility>
   <w:DoNotExpandShiftReturn/>
   <w:BreakWrappedTables/>
   <w:SplitPgBreakAndParaMark/>
   <w:EnableOpenTypeKerning/>
  </w:Compatibility>
  <w:BrowserLevel>MicrosoftInternetExplorer4</w:BrowserLevel>
  <m:mathPr>
   <m:mathFont m:val=""Cambria Math""/>
   <m:brkBin m:val=""before""/>
   <m:brkBinSub m:val=""&#45;-""/>
   <m:smallFrac m:val=""off""/>
   <m:dispDef/>
   <m:lMargin m:val=""0""/>
   <m:rMargin m:val=""0""/>
   <m:defJc m:val=""centerGroup""/>
   <m:wrapIndent m:val=""1440""/>
   <m:intLim m:val=""subSup""/>
   <m:naryLim m:val=""undOvr""/>
  </m:mathPr></w:WordDocument>
</xml><![endif]--><!--[if gte mso 9]><xml>
 <w:LatentStyles DefLockedState=""false"" DefUnhideWhenUsed=""false""
  DefSemiHidden=""false"" DefQFormat=""false"" DefPriority=""99""
  LatentStyleCount=""371"">
  <w:LsdException Locked=""false"" Priority=""0"" QFormat=""true"" Name=""Normal""/>
  <w:LsdException Locked=""false"" Priority=""9"" QFormat=""true"" Name=""heading 1""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 2""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 3""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 4""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 5""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 6""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 7""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 8""/>
  <w:LsdException Locked=""false"" Priority=""9"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""heading 9""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 6""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 7""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 8""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index 9""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 1""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 2""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 3""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 4""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 5""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 6""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 7""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 8""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""toc 9""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Normal Indent""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""footnote text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""annotation text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""header""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""footer""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""index heading""/>
  <w:LsdException Locked=""false"" Priority=""35"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""caption""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""table of figures""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""envelope address""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""envelope return""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""footnote reference""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""annotation reference""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""line number""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""page number""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""endnote reference""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""endnote text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""table of authorities""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""macro""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""toa heading""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Bullet""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Number""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Bullet 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Bullet 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Bullet 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Bullet 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Number 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Number 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Number 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Number 5""/>
  <w:LsdException Locked=""false"" Priority=""10"" QFormat=""true"" Name=""Title""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Closing""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Signature""/>
  <w:LsdException Locked=""false"" Priority=""1"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""Default Paragraph Font""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text Indent""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Continue""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Continue 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Continue 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Continue 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""List Continue 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Message Header""/>
  <w:LsdException Locked=""false"" Priority=""11"" QFormat=""true"" Name=""Subtitle""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Salutation""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Date""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text First Indent""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text First Indent 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Note Heading""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text Indent 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Body Text Indent 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Block Text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Hyperlink""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""FollowedHyperlink""/>
  <w:LsdException Locked=""false"" Priority=""22"" QFormat=""true"" Name=""Strong""/>
  <w:LsdException Locked=""false"" Priority=""20"" QFormat=""true"" Name=""Emphasis""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Document Map""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Plain Text""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""E-mail Signature""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Top of Form""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Bottom of Form""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Normal (Web)""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Acronym""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Address""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Cite""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Code""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Definition""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Keyboard""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Preformatted""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Sample""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Typewriter""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""HTML Variable""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Normal Table""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""annotation subject""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""No List""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Outline List 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Outline List 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Outline List 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Simple 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Simple 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Simple 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Classic 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Classic 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Classic 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Classic 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Colorful 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Colorful 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Colorful 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Columns 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Columns 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Columns 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Columns 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Columns 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 6""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 7""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Grid 8""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 4""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 5""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 6""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 7""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table List 8""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table 3D effects 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table 3D effects 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table 3D effects 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Contemporary""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Elegant""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Professional""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Subtle 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Subtle 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Web 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Web 2""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Web 3""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Balloon Text""/>
  <w:LsdException Locked=""false"" Priority=""39"" Name=""Table Grid""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" UnhideWhenUsed=""true""
   Name=""Table Theme""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" Name=""Placeholder Text""/>
  <w:LsdException Locked=""false"" Priority=""1"" QFormat=""true"" Name=""No Spacing""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 1""/>
  <w:LsdException Locked=""false"" SemiHidden=""true"" Name=""Revision""/>
  <w:LsdException Locked=""false"" Priority=""34"" QFormat=""true""
   Name=""List Paragraph""/>
  <w:LsdException Locked=""false"" Priority=""29"" QFormat=""true"" Name=""Quote""/>
  <w:LsdException Locked=""false"" Priority=""30"" QFormat=""true""
   Name=""Intense Quote""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""60"" Name=""Light Shading Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""61"" Name=""Light List Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""62"" Name=""Light Grid Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""63"" Name=""Medium Shading 1 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""64"" Name=""Medium Shading 2 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""65"" Name=""Medium List 1 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""66"" Name=""Medium List 2 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""67"" Name=""Medium Grid 1 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""68"" Name=""Medium Grid 2 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""69"" Name=""Medium Grid 3 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""70"" Name=""Dark List Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""71"" Name=""Colorful Shading Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""72"" Name=""Colorful List Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""73"" Name=""Colorful Grid Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""19"" QFormat=""true""
   Name=""Subtle Emphasis""/>
  <w:LsdException Locked=""false"" Priority=""21"" QFormat=""true""
   Name=""Intense Emphasis""/>
  <w:LsdException Locked=""false"" Priority=""31"" QFormat=""true""
   Name=""Subtle Reference""/>
  <w:LsdException Locked=""false"" Priority=""32"" QFormat=""true""
   Name=""Intense Reference""/>
  <w:LsdException Locked=""false"" Priority=""33"" QFormat=""true"" Name=""Book Title""/>
  <w:LsdException Locked=""false"" Priority=""37"" SemiHidden=""true""
   UnhideWhenUsed=""true"" Name=""Bibliography""/>
  <w:LsdException Locked=""false"" Priority=""39"" SemiHidden=""true""
   UnhideWhenUsed=""true"" QFormat=""true"" Name=""TOC Heading""/>
  <w:LsdException Locked=""false"" Priority=""41"" Name=""Plain Table 1""/>
  <w:LsdException Locked=""false"" Priority=""42"" Name=""Plain Table 2""/>
  <w:LsdException Locked=""false"" Priority=""43"" Name=""Plain Table 3""/>
  <w:LsdException Locked=""false"" Priority=""44"" Name=""Plain Table 4""/>
  <w:LsdException Locked=""false"" Priority=""45"" Name=""Plain Table 5""/>
  <w:LsdException Locked=""false"" Priority=""40"" Name=""Grid Table Light""/>
  <w:LsdException Locked=""false"" Priority=""46"" Name=""Grid Table 1 Light""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark""/>
  <w:LsdException Locked=""false"" Priority=""51"" Name=""Grid Table 6 Colorful""/>
  <w:LsdException Locked=""false"" Priority=""52"" Name=""Grid Table 7 Colorful""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""Grid Table 1 Light Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""Grid Table 2 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""Grid Table 3 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""Grid Table 4 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""Grid Table 5 Dark Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""Grid Table 6 Colorful Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""Grid Table 7 Colorful Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""46"" Name=""List Table 1 Light""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark""/>
  <w:LsdException Locked=""false"" Priority=""51"" Name=""List Table 6 Colorful""/>
  <w:LsdException Locked=""false"" Priority=""52"" Name=""List Table 7 Colorful""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 1""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 2""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 3""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 4""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 5""/>
  <w:LsdException Locked=""false"" Priority=""46""
   Name=""List Table 1 Light Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""47"" Name=""List Table 2 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""48"" Name=""List Table 3 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""49"" Name=""List Table 4 Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""50"" Name=""List Table 5 Dark Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""51""
   Name=""List Table 6 Colorful Accent 6""/>
  <w:LsdException Locked=""false"" Priority=""52""
   Name=""List Table 7 Colorful Accent 6""/>
 </w:LatentStyles>
</xml><![endif]-->
<style>
<!--
 /* Font Definitions */
 @font-face
	{{font-family:""Cambria Math"";
	panose-1:2 4 5 3 5 4 6 3 2 4;
	mso-font-charset:1;
	mso-generic-font-family:roman;
	mso-font-format:other;
	mso-font-pitch:variable;
	mso-font-signature:0 0 0 0 0 0;}}
@font-face
	{{font-family:Calibri;
	panose-1:2 15 5 2 2 2 4 3 2 4;
	mso-font-charset:0;
	mso-generic-font-family:swiss;
	mso-font-pitch:variable;
	mso-font-signature:-536870145 1073786111 1 0 415 0;}}
@font-face
	{{font-family:Tahoma;
	panose-1:2 11 6 4 3 5 4 4 2 4;
	mso-font-charset:0;
	mso-generic-font-family:swiss;
	mso-font-pitch:variable;
	mso-font-signature:-520081665 -1073717157 41 0 66047 0;}}
@font-face
	{{font-family:""Arabic Typesetting"";
	panose-1:3 2 4 2 4 4 6 3 2 3;
	mso-font-charset:0;
	mso-generic-font-family:script;
	mso-font-pitch:variable;
	mso-font-signature:-1610604433 -1073741824 8 0 211 0;}}
 /* Style Definitions */
 p.MsoNormal, li.MsoNormal, div.MsoNormal
	{{mso-style-unhide:no;
	mso-style-qformat:yes;
	mso-style-parent:"""";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	font-size:11.0pt;
	font-family:""Calibri"",sans-serif;
	mso-fareast-font-family:Calibri;
	mso-fareast-theme-font:minor-latin;
	mso-bidi-font-family:""Times New Roman"";}}
a:link, span.MsoHyperlink
	{{mso-style-noshow:yes;
	mso-style-priority:99;
	color:blue;
	text-decoration:underline;
	text-underline:single;}}
a:visited, span.MsoHyperlinkFollowed
	{{mso-style-noshow:yes;
	mso-style-priority:99;
	color:purple;
	text-decoration:underline;
	text-underline:single;}}
p.MsoAcetate, li.MsoAcetate, div.MsoAcetate
	{{mso-style-noshow:yes;
	mso-style-priority:99;
	mso-style-link:""Balloon Text Char"";
	margin:0in;
	margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	font-size:8.0pt;
	font-family:""Tahoma"",sans-serif;
	mso-fareast-font-family:Calibri;
	mso-fareast-theme-font:minor-latin;}}
span.BalloonTextChar
	{{mso-style-name:""Balloon Text Char"";
	mso-style-noshow:yes;
	mso-style-priority:99;
	mso-style-unhide:no;
	mso-style-locked:yes;
	mso-style-link:""Balloon Text"";
	font-family:""Tahoma"",sans-serif;
	mso-ascii-font-family:Tahoma;
	mso-hansi-font-family:Tahoma;
	mso-bidi-font-family:Tahoma;}}
span.EmailStyle19
	{{mso-style-type:personal;
	mso-style-noshow:yes;
	mso-style-unhide:no;
	font-family:""Arabic Typesetting"";
	mso-ascii-font-family:""Arabic Typesetting"";
	mso-hansi-font-family:""Arabic Typesetting"";
	mso-bidi-font-family:""Arabic Typesetting"";
	color:windowtext;}}
span.EmailStyle20
	{{mso-style-type:personal;
	mso-style-noshow:yes;
	mso-style-unhide:no;
	font-family:""Calibri"",sans-serif;
	mso-ascii-font-family:Calibri;
	mso-hansi-font-family:Calibri;
	color:#1F497D;}}
.MsoChpDefault
	{{mso-style-type:export-only;
	mso-default-props:yes;
	font-size:10.0pt;
	mso-ansi-font-size:10.0pt;
	mso-bidi-font-size:10.0pt;}}
@page WordSection1
	{{size:8.5in 11.0in;
	margin:1.0in 1.0in 1.0in 1.0in;
	mso-header-margin:.5in;
	mso-footer-margin:.5in;
	mso-paper-source:0;}}
div.WordSection1
	{{page:WordSection1;}}
-->
</style>
<!--[if gte mso 10]>
<style>
 /* Style Definitions */
 table.MsoNormalTable
	{{mso-style-name:""Table Normal"";
	mso-tstyle-rowband-size:0;
	mso-tstyle-colband-size:0;
	mso-style-noshow:yes;
	mso-style-priority:99;
	mso-style-parent:"""";
	mso-padding-alt:0in 5.4pt 0in 5.4pt;
	mso-para-margin:0in;
	mso-para-margin-bottom:.0001pt;
	mso-pagination:widow-orphan;
	font-size:10.0pt;
	font-family:""Times New Roman"",serif;}}
</style>
<![endif]-->
</head>

<body lang=EN-US link=blue vlink=purple style='tab-interval:.5in'>

<div class=WordSection1>


<p class=MsoNormal><strong><u><span style='font-family:""Calibri"",sans-serif'><o:p><span
 style='text-decoration:none'>&nbsp;</span></o:p></span></u></strong></p>

<p class=MsoNormal><strong><u><span style='font-family:""Calibri"",sans-serif'>Please
submit any LO Adjustments and Bonuses to me NO LATER than&nbsp;</span></u></strong><em><b><u><span
style='font-family:""Calibri"",sans-serif;color:green'>{1:dddd},&nbsp;{1:MMMM} {2} </span></u></b></em><em><b><u><span
style='font-family:""Calibri"",sans-serif'>,</span></u></b></em><strong><u><span
style='font-size:13.5pt;font-family:""Calibri"",sans-serif'>&nbsp;at 2:00 p.m.</span></u></strong></p>

<p class=MsoNormal>&nbsp;</p>

<p class=MsoNormal><b><u><span style='font-size:14.0pt;font-family:""Courier New"";
color:red'>***ALL FE'S MUST BE SCANNED IN AT LEAST 24 HOURS IN ADVANCE IN
ORDER&nbsp;FOR FUNDING TO&nbsp;CLEAR OUT BY THE DEADLINE DATE***</span></u></b><b><span
style='font-size:14.0pt;font-family:""Courier New"";color:red'><o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:14.0pt;font-family:""Courier New"";
color:red'>&nbsp;<o:p></o:p></span></b></p>

<p class=MsoNormal><b><u><span style='font-size:10.0pt;font-family:""Courier New"";
color:black'>You will need to clear any </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:blue'>In-House loans </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:black'>that are on </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:red'>&quot;HOLD&quot; </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:black'>with&nbsp;Corporate
Funding in&nbsp;Funding&nbsp;and </span></u></b><b><u><span style='font-size:
10.0pt;font-family:""Courier New"";color:blue'>Brokered loans&nbsp;</span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:black'>that are on </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:red'>&quot;HOLD&quot; </span></u></b><b><u><span
style='font-size:10.0pt;font-family:""Courier New"";color:black'>with&nbsp;Jennifer Hooker before the deadline date in order to pay on&nbsp;{0:MM/dd/yyyy}.</span></u></b><b><span
style='font-size:14.0pt;font-family:""Courier New"";color:red'><o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:14.0pt;font-family:""Courier New"";
color:red'>&nbsp;<o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:14.0pt;font-family:""Courier New"";
color:red'>&nbsp;<o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:10.0pt;font-family:""Courier New"";
color:red'>Thank You!!!</span></b><b><span style='font-size:14.0pt;font-family:
""Courier New"";color:red'><o:p></o:p></span></b></p>

<DIV>&nbsp;</DIV>
<DIV align=left><FONT face=""Californian FB"" color=#0000ff size=4>Jennifer Hooker 
</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">Golden Empire Mortgage</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">HR/Payroll Department</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~283~1203 Direct 
Line</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~379~6632 Fax</FONT></DIV>

<p class=MsoNormal><span style='font-size:16.0pt;font-family:""Arabic Typesetting""'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

</div>

</body>

</html>

", payDate, deadline, GetOrdinal(deadline.Day));
        }

        private string ApproveMessage(DateTime payDate, DateTime deadline)
        {
            return String.Format(@"
            <!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML><HEAD>
<META http-equiv=Content-Type content=""text/html; charset=us-ascii"">
<META content=""MSHTML 6.00.6000.17095"" name=GENERATOR></HEAD>
<BODY>
<DIV><FONT face=""Californian FB"" size=4><STRONG><U><FONT 
face=Arial><SPAN><STRONG><U><SPAN><SPAN class=795370320-16112010><FONT 
size=4><EM><FONT color=#ff0000>
<DIV align=left><STRONG><U><FONT face=Arial><SPAN>{0:MM/dd/yyyy}&nbsp;Weekly 
Commission&nbsp;Summary 
Review</SPAN></FONT></U></STRONG></DIV></FONT></EM></FONT></SPAN></SPAN></U></STRONG></SPAN></FONT></U></STRONG>
<DIV align=left><STRONG><U><FONT face=Arial><SPAN>Please submit any 
</SPAN><SPAN>LO Adjustments</SPAN><SPAN> to me </SPAN><SPAN>NO LATER 
than<SPAN class=197485419-26112008>&nbsp;<EM><FONT 
color=#008000>{1:dddd},&nbsp;{1:MMMM} {2} </FONT><SPAN class=445215723-16122009><SPAN 
class=085231522-30122009><SPAN 
class=181134021-24062010>,</SPAN></SPAN></SPAN></EM></SPAN></SPAN><SPAN><FONT 
size=4>&nbsp;at 2:00 p.m.</FONT></SPAN></FONT></U></STRONG></DIV>
<DIV align=left><STRONG><U><FONT face=Arial 
size=4><SPAN></SPAN></FONT></U></STRONG>&nbsp;</DIV>
<DIV align=left><STRONG><FONT face=Arial size=4><SPAN>Thank You 
!!</SPAN></FONT></STRONG></DIV></FONT></DIV>
<DIV>&nbsp;</DIV>
<DIV align=left><FONT face=""Californian FB"" color=#0000ff size=4>Jennifer Hooker 
</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">Golden Empire Mortgage</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">HR/Payroll Department</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~283~1203 Direct 
Line</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~379~6632 Fax</FONT></DIV>
<DIV>&nbsp;</DIV></BODY></HTML>", payDate, deadline, GetOrdinal(deadline.Day));

        }

        private string OnHoldMessage(DateTime payDate, DateTime deadline)
        {

            return String.Format(@"
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML><HEAD>
<META http-equiv=Content-Type content=""text/html; charset=us-ascii"">
<META content=""MSHTML 6.00.6000.17095"" name=GENERATOR></HEAD>
<BODY>
<DIV><FONT face=""Californian FB"" size=4>
<DIV align=left>
<DIV align=left><STRONG><U><SPAN>{0:MM/dd/yyyy} Weekly&nbsp;Commission Summary 
Review</SPAN></U></STRONG></DIV>
<DIV align=left><STRONG><U><SPAN>Please submit any </SPAN><SPAN>LO Adjustments </SPAN><SPAN>to me </SPAN><SPAN>NO LATER than<SPAN 
class=197485419-26112008>&nbsp;<EM><FONT color=#008000>{1:dddd},&nbsp;{1:MMMM} {2} 
at 2 pm</FONT></EM></SPAN></SPAN></U></STRONG><STRONG><U><SPAN><FONT 
size=4></FONT></SPAN></U></STRONG></DIV>
<DIV><STRONG><SPAN><FONT size=4><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'"">
<DIV><FONT size=2><FONT size=4><FONT size=+0><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
class=241095519-19022009><U>***ALL FE'S MUST BE SCANNED IN AT LEAST 24 HOURS IN 
ADVANCE IN ORDER&nbsp;FOR FUNDING TO&nbsp;CLEAR OUT BY THE DEADLINE 
DATE***</U></SPAN></SPAN></FONT></FONT></FONT></DIV>
<DIV><FONT face=Arial size=2><FONT color=red><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Courier New'""><FONT 
color=#000000></FONT></SPAN></SPAN></SPAN></FONT></FONT>&nbsp;</DIV>
<DIV><FONT color=red><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Courier New'""><FONT color=#000000><U>You 
will need to clear any <SPAN style=""COLOR: blue"">In-House loans </SPAN>that are 
on <SPAN style=""FONT-WEIGHT: bold; COLOR: red"">""HOLD"" </SPAN>with&nbsp;<SPAN 
class=070442621-09102008>Patricia Tongate in&nbsp;Funding</SPAN>&nbsp;and <SPAN 
style=""COLOR: blue"">Brokered loans&nbsp;</SPAN>that are on <SPAN 
style=""FONT-WEIGHT: bold; COLOR: red"">""HOLD"" </SPAN>with&nbsp;<SPAN>Jennifer Hooker
on or before before the deadline date of <FONT color=#ff0000>{1:MM/dd/yyyy} 
</FONT>in order to pay on&nbsp;<SPAN class=842153623-16092010><SPAN 
class=009361323-23092010><SPAN class=756224422-28102010><FONT 
color=#008000>{0:MM/dd/yyyy}</FONT></SPAN></SPAN></SPAN></SPAN>.</U></FONT></SPAN></SPAN></SPAN></FONT></DIV>
<DIV><FONT color=#000000><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Courier New'""><U></U></SPAN></SPAN></SPAN></FONT>&nbsp;</DIV>
<DIV><FONT color=#000000><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Courier New'""><U></U></SPAN></SPAN></SPAN></FONT>&nbsp;</DIV>
<DIV><FONT color=#000000><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-WEIGHT: bold; FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Courier New'"">Thank 
You!!!</SPAN></SPAN></SPAN></FONT></DIV></SPAN></FONT></SPAN></STRONG></DIV></DIV></FONT></DIV>
<DIV>&nbsp;</DIV>
<DIV align=left><FONT face=""Californian FB"" color=#0000ff size=4>Jennifer Hooker 
</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">Golden Empire Mortgage</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">HR/Payroll Department</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~283~1203 Direct 
Line</FONT></DIV>
<DIV align=left><FONT face=""Californian FB"">661~379~6632 Fax</FONT></DIV>
<DIV>&nbsp;</DIV></BODY></HTML>", payDate, deadline, GetOrdinal(deadline.Day));

        }

        private string SalaryApprovedMessage(DateTime payDate, DateTime deadline)
        {
            return String.Format(@"
            <!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML><HEAD>
<META http-equiv=Content-Type content=""text/html; charset=us-ascii"">
<META content=""MSHTML 6.00.6000.17097"" name=GENERATOR>

<!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=""edit"" spidmax=""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=""edit"">
<o:idmap v:ext=""edit"" data=""1"" />
</o:shapelayout></xml><![endif]--></HEAD>
<BODY lang=EN-US vLink=purple link=blue>
<DIV dir=ltr align=left><FONT face=Verdana color=#0000ff 
size=2></FONT>&nbsp;</DIV>
<DIV class=WordSection1>
<P class=MsoNormal><STRONG><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Arial','sans-serif'"">Please submit any 
</SPAN></STRONG><STRONG><SPAN style=""COLOR: red; FONT-FAMILY: 'Courier New'"">LO 
Adjustments </SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'""></SPAN></STRONG><STRONG><SPAN style=""COLOR: red; FONT-FAMILY: 'Courier New'""></SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Arial','sans-serif'""> to me 
</SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 14pt; COLOR: blue; FONT-FAMILY: 'Courier New'"">NO LATER 
</SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">than&nbsp;</SPAN></STRONG><EM><B><SPAN 
style=""FONT-SIZE: 10pt; COLOR: green; FONT-FAMILY: 'Courier New'"">{1:dddd}</SPAN></B></EM><EM><B><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">,&nbsp;{1:MMMM}
{2}</SPAN></B></EM><STRONG><SPAN 
style=""FONT-SIZE: 13.5pt; FONT-FAMILY: 'Arial','sans-serif'"">,&nbsp;at 2:00 
p.m.</SPAN></STRONG>
<br />
<br /><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: #5f497a; FONT-FAMILY: 'Bodoni MT','serif'"">Jennifer Hooker</SPAN></B><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: #5f497a""><o:p></o:p></SPAN></B>
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">Golden Empire 
Mortgage</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">Commission Payroll Specialist<o:p></o:p></SPAN></I></B>
<!--<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">NMLS 
Licensing</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>-->
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">661-283-1203 
Phone</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
<B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">661-379-6632 
Fax</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
<P class=MsoNormal><o:p>&nbsp;</o:p></P></DIV></BODY></HTML>", payDate, deadline, GetOrdinal(deadline.Day));

        }

        private string SalaryOnHoldMessage(DateTime payDate, DateTime deadline)
        {
            return String.Format(@"
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<HTML><HEAD>
<META http-equiv=Content-Type content=""text/html; charset=us-ascii"">
<META content=""MSHTML 6.00.6000.17097"" name=GENERATOR>

<!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=""edit"" spidmax=""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=""edit"">
<o:idmap v:ext=""edit"" data=""1"" />
</o:shapelayout></xml><![endif]--></HEAD>
<BODY lang=EN-US vLink=purple link=blue>
<DIV dir=ltr align=left><STRONG><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Arial','sans-serif'"">Please submit any 
</SPAN></STRONG><STRONG><SPAN style=""COLOR: red; FONT-FAMILY: 'Courier New'"">LO 
Adjustments </SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'""></SPAN></STRONG><STRONG><SPAN style=""COLOR: red; FONT-FAMILY: 'Courier New'""></SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Arial','sans-serif'""> to me 
</SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 14pt; COLOR: blue; FONT-FAMILY: 'Courier New'"">NO LATER 
</SPAN></STRONG><STRONG><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">than&nbsp;</SPAN></STRONG><EM><B><SPAN 
style=""FONT-SIZE: 10pt; COLOR: green; FONT-FAMILY: 'Courier New'"">{1:dddd}</SPAN></B></EM><EM><B><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">,&nbsp;{1:MMMM}
{2}</SPAN></B></EM><STRONG><SPAN 
style=""FONT-SIZE: 13.5pt; FONT-FAMILY: 'Arial','sans-serif'"">,&nbsp;at 2:00 
p.m.</SPAN></STRONG><B><U><SPAN 
style=""FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'"">&nbsp;<o:p></o:p></SPAN></U></B></DIV>
<DIV class=WordSection1>
<P class=MsoNormal><B><U><SPAN 
style=""FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'"">***ALL FE'S MUST 
BE SCANNED IN AT LEAST 24 HOURS IN ADVANCE IN ORDER&nbsp;FOR FUNDING 
TO&nbsp;CLEAR OUT BY THE DEADLINE DATE***</SPAN></U></B><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><o:p></o:p></SPAN></B></P>
<P class=MsoNormal><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">You will need 
to clear any </SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: blue; FONT-FAMILY: 'Courier New'"">In-House loans 
</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">that are on 
</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: 'Courier New'"">""HOLD"" 
</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">with 
Corporate&nbsp;Funding&nbsp;and </SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: blue; FONT-FAMILY: 'Courier New'"">Brokered 
loans&nbsp;</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">that are on 
</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: red; FONT-FAMILY: 'Courier New'"">""HOLD"" 
</SPAN></U></B><B><U><SPAN 
style=""FONT-SIZE: 10pt; COLOR: black; FONT-FAMILY: 'Courier New'"">with&nbsp;Jennifer Hooker before the deadline date in order to pay 
on&nbsp; {0}.</SPAN></U></B><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: red; FONT-FAMILY: 'Courier New'""><o:p></o:p></SPAN></B></P>

<br /><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: #5f497a; FONT-FAMILY: 'Bodoni MT','serif'"">Jennifer Hooker</SPAN></B><B><SPAN 
style=""FONT-SIZE: 14pt; COLOR: #5f497a""><o:p></o:p></SPAN></B>
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">Golden Empire 
Mortgage</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">HR/Payroll 
Department<o:p></o:p></SPAN></I></B>
<!--<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">NMLS 
Licensing</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>-->
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">661-283-1203 
Phone</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
<br /><B><I><SPAN 
style=""FONT-SIZE: 10pt; FONT-FAMILY: 'Bookman Old Style','serif'"">661-379-6632 
Fax</SPAN></I></B><B><I><SPAN 
style=""FONT-FAMILY: 'Bookman Old Style','serif'""><o:p></o:p></SPAN></I></B>
</DIV></BODY></HTML>", payDate.ToShortDateString(), deadline, GetOrdinal(deadline.Day));

        }
    }
}
