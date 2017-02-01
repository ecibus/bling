using System;
using Bling.Domain.Extension;
using Bling.Presenter;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class AjaxInsuranceEnrollment : BasePage, IAjaxView
    {
        public string ResponseText { get; set; }
        private AjaxInsuranceEnrollmentPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.LoadData();
                        break;

                    case "getenrollment":
                        m_Presenter.GetEnrollment(Request.Form["YearMonth"], Request.Form["BranchNo"]);
                        break;

                    case "updatetitle":
                        m_Presenter.UpdateTitle(Request.Form["YearMonth"], Request.Form["Column"], Request.Form["NewTitle"]);
                        break;

                    case "updateenrollment":
                        m_Presenter.UpdateEnrollment(Request.Form["recid"].ToInteger(), Request.Form["fieldNo"].ToInteger(), Request.Form["newValue"].ToDecimal());
                        break;

                    case "addnewrate":
                        m_Presenter.AddNewRate(Request.Form["insurancetype"], Request.Form["newRate"].ToDecimal());
                        break;

                    case "updateeestatus":
                        m_Presenter.UpdateEEStatus(Request.Form["recid"].ToInteger(), Request.Form["newValue"]);
                        break;

                    case "updateislo":
                        m_Presenter.UpdateIsLO(Request.Form["recid"].ToInteger(), Request.Form["islo"].ToInteger());
                        break;

                    case "updateempcost":
                        m_Presenter.UpdateEmpCost(Request.Form["recid"].ToInteger(), Request.Form["empCost"].ToDecimal());
                        break;

                    case "enrollemployee":
                        EnrollEmployee();
                        break;

                    case "deleteenrollmentbyid":
                        m_Presenter.RemoveEnrollment(Request.Form["recid"].ToInteger());
                        break;

                    case "addnewmonth":
                        m_Presenter.AddNewMonth(Request.Form["yearmonth"], Request.Form["year"], Request.Form["month"]);
                        break;

                    case "displayreport":
                        string report = Server.MapPath("Report/InsEnrol.rpt");
                        string pdfName = Server.MapPath("Report/InsEnrol.pdf");
                        m_Presenter.DisplayReport(report, pdfName, Request.Form["yearmonth"], Request.Form["branch"], Request.Form["islo"].ToInteger());
                        break;

                    case "displayreportpb":
                        string report2 = Server.MapPath("Report/InsEnrolPB.rpt");
                        string pdfName2 = Server.MapPath("Report/InsEnrolPB.pdf");
                        m_Presenter.DisplayReport(report2, pdfName2, Request.Form["yearmonth"], Request.Form["branch"], Request.Form["islo"].ToInteger());
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = ex.Message;
            }
        }

        private void EnrollEmployee()
        {
            string yearMonth = Request.Form["ym"];
            string empName = Request.Form["empName"];
            string branchNo = Request.Form["branchNo"];
            DateTime birthDate = Request.Form["birthDate"].ToDateTime();
            bool isLO = Request.Form["isLO"].ToBoolean();
            string eeStatus = Request.Form["eestatus"];
            decimal rate1 = Request.Form["rate1"].ToDecimal();
            decimal rate3 = Request.Form["rate3"].ToDecimal();
            decimal rate4 = Request.Form["rate4"].ToDecimal();
            decimal rate5 = Request.Form["rate5"].ToDecimal();
            decimal rate6 = Request.Form["rate6"].ToDecimal();
            decimal rate7 = Request.Form["rate7"].ToDecimal();
            decimal rate9 = Request.Form["rate9"].ToDecimal();
            decimal rate10 = Request.Form["rate10"].ToDecimal();
            decimal rate11 = Request.Form["rate11"].ToDecimal();
            decimal rate12 = Request.Form["rate12"].ToDecimal();
            decimal empCost = Request.Form["empCost"].ToDecimal();

            m_Presenter.EnrollEmployee(yearMonth, empName, branchNo, birthDate, rate1, rate3, rate4, rate5, rate6, rate7,
                rate9, rate10, rate11, rate12, empCost, eeStatus, isLO);

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxInsuranceEnrollmentPresenter(this);
            base.OnInit(e);
        }
    }
}
