using System;
using System.Collections.Generic;
using Bling.Presenter;
using Bling.Presenter.HR;
using Bling.Domain.Extension;
using System.Threading;

namespace Bling.Web.HR
{
    public partial class AjaxProcessLOCommission : BasePage, IAjaxView
    {
        private AjaxProcessLOCommissionPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "lo":
                        m_Presenter.LogProcessCommission(CurrentUser.UserInfo.EmployId, Request.Form["PayDate"], Request.Form["fundedAsOf"]);
                        m_Presenter.CreateLOCommission(Request.Form["PayDate"], Request.Form["fundedAsOf"], Request.Form["IsWeekly"].ToLower());
                        break;

                    case "lo_oldloans":
                        m_Presenter.CreateLOCommissionForOldLoans(Request.Form["PayDate"], Request.Form["fundedAsOf"]);
                        break;

                    case "manager":
                        m_Presenter.CreateManagerCommission(Request.Form["PayDate"], Request.Form["fundedAsOf"]);
                        break;

                    case "override":
                        m_Presenter.CreateManagerOverride(Request.Form["PayDate"], Request.Form["fundedAsOf"]);
                        break;

                    case "viewcommissionreport":
                        m_Presenter.RefreshCommissionDataFromTracker(Request.Form["PayDate"]);
                        GenerateCommissionReport(Request.Form["branchno"], true, Request.Form["guid"]);
                        break;

                    case "emailcommissionreport":
                        m_Presenter.RefreshCommissionDataFromTracker(Request.Form["PayDate"]);
                        EMailCommissionReport(Request.Form["branchno"]);
                        break;

                    case "emailcommissionreportlo":
                        m_Presenter.RefreshCommissionDataFromTracker(Request.Form["PayDate"]);
                        EMailCommissionReportLO(Request.Form["branchno"]);
                        break;

                    case "emailunsendreporttolo":
                        ResponseText = EmailUnsendReportToLO(CurrentUser.UserInfo.EmployId, Request.Form["IntervalId"],
                            Request.Form["PayDate"], Request.Form["BranchNo"],
                            Request.Form["fundedAsOf"], Request.Form["IsWeekly"]);
                        return;

                    case "viewsummaryreport":
                        GenerateSummaryReport();
                        break;

                    case "viewishreport":
                        GenerateISHReport(Request.Form["branchno"], Request.Form["month"], Request.Form["year"], true);
                        break;

                    case "emailishreport":
                        EMailISHReport(Request.Form["branchno"], Request.Form["month"], Request.Form["year"]);
                        break;

                    case "viewcommissionsaccrual":
                        ViewCommissionsAccrual(Request.Form["endingDate"]);
                        break;

                    case "viewcommissionsaccrualgltotals":
                        ViewCommissionsAccrualGLTotals(Request.Form["endingDate"]);
                        break;

                    case "viewoverridereport":
                        GenerateOverrideReport(Request.Form["branchno"], true);
                        break;

                    default:
                        break;

                }

                ResponseText = String.Empty;

            }
            catch (Exception ex)
            {
                LogError(ex);
                ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxProcessLOCommissionPresenter(this);
            base.OnInit(e);
        }

        private string EmailUnsendReportToLO(string employId, string intervalId, string paydate, string branchNo,
            string fundedAsOf, string isWeekly)
        {
            //if (m_Presenter.IsFirstEmailToSend(employId, intervalId))
            //{
            //    m_Presenter.RefreshCommissionDataFromTracker(paydate);
            //}
            string lo = m_Presenter.GetUnsendLOEmail(branchNo, paydate, fundedAsOf, isWeekly, employId, intervalId);

            if (lo != String.Empty)
            {
                SendReportLO(lo, branchNo);

                m_Presenter.SaveSentLOEmail(employId, intervalId, "LO", lo);
            }

            return lo.Trim().Replace("\r", "").Replace("\n", "");
            //return "{ 'lo' : '" + lo + "'}" ;

        }
        private void EMailCommissionReport(string branchno)
        {
            if (branchno.ToLower() != "all")
            {
                SendReport(branchno);
                return;
            }

            var resend = Request.Form["Resend"] == "1" ? true : false;

            if (!resend)
            {
                m_Presenter.ClearBranchEmailTracking(Request.Form["PayDate"],
                        Request.Form["fundedAsOf"], Convert.ToBoolean(Request.Form["IsWeekly"]));
            }

            IList<string> branches = m_Presenter.GetBranchWithCommission(Request.Form["PayDate"],
                Request.Form["fundedAsOf"], Request.Form["IsWeekly"]);
            foreach (var branch in branches)
            {
                if (branch == "200")
                    continue;

                var sent = false;
                if (resend)
                {
                    sent = m_Presenter.IsBranchEmailSent(branch, Request.Form["PayDate"],
                        Request.Form["fundedAsOf"], Convert.ToBoolean(Request.Form["IsWeekly"]));
                }

                if (!sent)
                {
                    SendReport(branch);
                    System.Threading.Thread.Sleep(5000);
                }                
            }            
        }

        private void EMailCommissionReportLO(string branchno)
        {
            IList<string> employees = m_Presenter.GetLOWithCommission(branchno, Request.Form["PayDate"],
                Request.Form["fundedAsOf"], Request.Form["IsWeekly"]);

            var resend = Request.Form["Resend"] == "1" ? true : false;

            if (!resend)
            {
                m_Presenter.ClearLOEmailTracking(Request.Form["PayDate"],
                        Request.Form["fundedAsOf"], Convert.ToBoolean(Request.Form["IsWeekly"]));
            }

            foreach (var employId in employees)
            {
                var sent = false;
                if (resend)
                {
                    sent = m_Presenter.IsLOEmailSent(employId, Request.Form["PayDate"],
                        Request.Form["fundedAsOf"], Convert.ToBoolean(Request.Form["IsWeekly"]));
                }

                if (!sent)
                {
                    SendReportLO(employId, branchno);
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        private void SendReport(string branchno)
        {
            string payDate = Request.Form["payDate"];
            string fundedAsOf = Request.Form["fundedAsOf"];
            string guid = Guid.NewGuid().ToString();
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}-{2}.pdf", "Commission", branchno, guid));
            string deadline = Request.Form["deadline"];
            bool isWeekly = Convert.ToBoolean(Request.Form["IsWeekly"]);
            bool sendToManager = Convert.ToBoolean(Request.Form["sendToManager"]);

            if (GenerateCommissionReport(branchno, false, guid) > 0)
            {
                m_Presenter.EMailCommissionReport(branchno, pdfName, payDate, fundedAsOf, 
                    CurrentUser.UserInfo.EMail, CurrentUser.UserInfo.FullName, deadline, isWeekly, sendToManager);
            }
        }

        private int GenerateCommissionReport(string branchNo, bool generateEmpty, string guid)
        {
            string report = Server.MapPath("Report/CommissionLoginName_DT.rpt");
            string payDate = Request.Form["payDate"];
            string fundedAsOf = Request.Form["fundedAsOf"];            
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}{2}.pdf", "Commission", branchNo, (guid == string.Empty ? "" : "-") + guid));
            string isWeekly = Request.Form["IsWeekly"];
            return m_Presenter.GenerateCommissionReport(report, pdfName, payDate, fundedAsOf, branchNo, isWeekly, generateEmpty);
        }

        private void SendReportLO(string employId, string branchNo)
        {
            string payDate = Request.Form["payDate"];
            string fundedAsOf = Request.Form["fundedAsOf"];
            string guid = Guid.NewGuid().ToString();
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}-{2}.pdf", "Commission", payDate.ToDateTime().ToString("yyyy-MM-dd"), guid));
            string deadline = Request.Form["deadline"];
            bool isWeekly = Convert.ToBoolean(Request.Form["IsWeekly"]);
            bool sendToManager = Convert.ToBoolean(Request.Form["sendToManager"]);

            //employId is now the username
            if (GenerateLOCommissionReport(employId, true, guid) > 0)
            {
                m_Presenter.EMailLOCommissionReport(employId, pdfName, payDate, fundedAsOf,
                    CurrentUser.UserInfo.EMail, CurrentUser.UserInfo.FullName, deadline, isWeekly, sendToManager, branchNo);
            }
        }

        private int GenerateLOCommissionReport(string employId, bool generateEmpty, string guid)
        {
            //string report = Server.MapPath("Report/CommissionByLO_DT.rpt");
            string report = Server.MapPath("Report/CommissionByLOLoginName_DT.rpt");
            string payDate = Request.Form["payDate"];
            string fundedAsOf = Request.Form["fundedAsOf"];
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}-{2}.pdf", "Commission", payDate.ToDateTime().ToString("yyyy-MM-dd"), guid));
            string isWeekly = Request.Form["IsWeekly"];
            return m_Presenter.GenerateLOCommissionReport(report, pdfName, payDate, fundedAsOf, employId, isWeekly, generateEmpty);
        }

        private int GenerateOverrideReport(string branchNo, bool generateEmpty)
        {
            string report = Server.MapPath("Report/CommissionOverride.rpt");
            string payDate = Request.Form["payDate"];
            string fundedAsOf = Request.Form["fundedAsOf"];
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}.pdf", "CommissionOverride", branchNo));
            string isWeekly = Request.Form["IsWeekly"];
            return m_Presenter.GenerateCommissionReport(report, pdfName, payDate, fundedAsOf, branchNo, isWeekly, generateEmpty);
        }
        
        private void GenerateSummaryReport()
        {
            string guid = Request.Form["guid"];
            string report = Server.MapPath("Report/CommissionSummary.rpt");
            string payDate = Request.Form["payDate"];
            string isWeekly = Request.Form["IsWeekly"];
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}-{2}.pdf", "CommissionSummary", payDate.Replace("/", "-"), guid));
            m_Presenter.GenerateCommissionSummaryReport(report, pdfName, payDate, isWeekly);
        }

        private int GenerateISHReport(string branchNo, string month, string year, bool generateEmpty)
        {
            string report = Server.MapPath("Report/CommissionInsideSales.rpt");
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}.pdf", "InsideSalesHourly", branchNo));

            return m_Presenter.GenerateISHReport(report, pdfName, branchNo, month, year, generateEmpty);
        }

        private void EMailISHReport(string branchNo, string month, string year)
        {
            if (branchNo.ToLower() != "all")
            {
                SendISHReport(branchNo, month, year);
                return;
            }

            IList<string> branches = m_Presenter.GetBranchWithTimeCard(Request.Form["month"],
                Request.Form["year"]);
            foreach (var branch in branches)
            {
                if (branch == "113" || branch == "200")
                    continue;

                SendISHReport(branch, month, year);

            }     
        }

        private void SendISHReport(string branchno, string month, string year)
        {
           
            string pdfName = Server.MapPath(String.Format("Report/{0}-{1}.pdf", "InsideSalesHourly", branchno));
            string deadline = Request.Form["deadline"];
            bool sendToManager = Convert.ToBoolean(Request.Form["sendToManager"]);

            if (GenerateISHReport(branchno, month, year, false) > 0)
            {
                m_Presenter.EMailISHReport(branchno, pdfName, month, year,
                    CurrentUser.UserInfo.EMail, CurrentUser.UserInfo.FullName, deadline, sendToManager);
            }
        }

        private void ViewCommissionsAccrual(string endingDate)
        {
            string report = Server.MapPath("Report/Commaccr.rpt");

            string pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "CommissionsAccrual"));
            m_Presenter.ViewCommissionsAccrual(report, pdfName, endingDate);
        }

        private void ViewCommissionsAccrualGLTotals(string endingDate)
        {
            string report = Server.MapPath("Report/Caglt.rpt");

            string pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "CommissionsAccrualGLTotals"));
            m_Presenter.ViewCommissionsAccrual(report, pdfName, endingDate);
        }

        public string ResponseText { get; set; }
    }
}