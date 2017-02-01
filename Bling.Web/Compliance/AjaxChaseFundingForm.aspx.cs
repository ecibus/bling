using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Compliance;
using Bling.Domain.Extension;

namespace Bling.Web.Compliance
{
    public partial class AjaxChaseFundingForm : BasePage, IAjaxView
    {
        private AjaxChaseFundingFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.GetLoanInfo(Request["LoanNumber"].Trim());
                        break;
                    case "savegeneralinfo":
                        m_Presenter.SaveGeneralInfo(Request["fileId"].Trim(),
                            Request["PurchaseProperty"],
                            Request["Item2"],
                            Request["chkOccInvestmentYes"].Trim(),
                            Request["chkOccInvestmentNo"].Trim(),
                            Request["chkOccInvestmentNA"].Trim()
                            );
                        break;
                    case "saveatrqm":
                        m_Presenter.SaveATRQM(Request["fileId"].Trim(),
                            Request["aporPcnt"], Request["qmSafeHarbor"],
                            Request["qmRebuttablePresumption"], Request["nonQM"], Request["qmNotApplicable"],
                            Request["Item7AYes"], Request["Item7ANo"], Request["Item7BYes"], Request["Item7BNo"], 
                            Request["Item8Yes"], Request["Item8No"]);
                        break;
                    case "savehighcost":
                        m_Presenter.SaveHighCost(Request["fileId"].Trim(),
                            Request["prepaymentPenalty"]);
                        break;
                    case "savehighcostcontinued":
                        m_Presenter.SaveHighCostContinued(Request["fileId"].Trim(),
                            Request["pointsExcluded"], Request["feesImposed"], Request["HoepaAPR"].Trim());
                        break;
                    case "savespecialfeature":
                        m_Presenter.SaveSpecialFeature(Request["fileId"].Trim(),
                            Request["SFFannieMae"], Request["SFFreddieMac"], Request["MIMonthlyPremium"], Request["MISinglePremium"]);
                        break;
                    case "saveexcludebonafide":
                        m_Presenter.SaveExcludedBonafide(Request["fileId"].Trim(),
                            Request["Item15Percent"].Trim(), Request["Item15Amount"].Trim(),
                            Request["HOEPAQMPcnt"], Request["HOEPAQMAmount"],
                            Request["StatePcnt"], Request["StateAmount"]
                            );
                        break;
                    case "printpreview":
                        string report = Server.MapPath("Report/CFF.rpt");
                        string pdfName = Server.MapPath("Report/CFF.pdf");
                        m_Presenter.PrintPreview(report, pdfName, Request.Form["LoanNumber"]);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                ResponseText = ex.Message.Escape();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxChaseFundingFormPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { set; get; }

    }
}