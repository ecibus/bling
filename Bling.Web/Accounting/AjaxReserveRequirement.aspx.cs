using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Domain.Accounting;
using Bling.Presenter.Accounting;
using Bling.Domain.Extension;

namespace Bling.Web.Accounting
{
    public partial class AjaxReserveRequirement : BasePage, IAjaxView
    {
        private AjaxReserveRequirementPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load();
                        break;

                    case "add":
                        ReserveRequirement rr = new ReserveRequirement
                        {
                            CostCenter = Request.Form["CostCenter"],
                            Recipient = Request.Form["Recipient"],
                            ReserveMinimum = Request.Form["ReserveMinimum"].Trim() == String.Empty ? (decimal?)null : Convert.ToDecimal(Request.Form["ReserveMinimum"].Trim()),
                            FixedReserve = Request.Form["FixedReserve"].Trim() == String.Empty ? (decimal?)null : Convert.ToDecimal(Request.Form["FixedReserve"].Trim())
                        };

                        m_Presenter.Save(rr);
                        break;

                    case "update":
                        m_Presenter.Update(Request.Form["id"], Request.Form["CostCenter"],
                            Request.Form["ReserveMinimum"].Trim(),
                            Request.Form["FixedReserve"].Trim(),
                            Request.Form["Recipient"].Trim());

                        break;

                    case "delete":
                        m_Presenter.Delete(Request.Form["id"].ToInteger());
                        break;

                    case "emailreport":
                        string pdfName = Server.MapPath(String.Format("Report/PLRecap.pdf"));
                        string reportName = Server.MapPath("Report/PLRecapBForEmail.rpt");
                        m_Presenter.EMailReport(Request.Form["ids"], Request.Form["month"], Request.Form["Year"], pdfName, reportName, CurrentUser.UserInfo.EMail, CurrentUser.UserInfo.FullName, Request.Form["emailtome"]);
                        break;

                    case "refreshplrecapdata":
                        m_Presenter.RefreshPLRecapData(Request.Form["month"], Request.Form["year"]);
                        break;

                    case "refreshambdata":
                        m_Presenter.RefreshAMBData(Request.Form["counter"]);
                        break;

                    case "consolidategl":
                        m_Presenter.ConsolidateGL();
                        break;

                    case "clearambdata":
                        m_Presenter.ClearAMBData();
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = String.Format("{{ \"Message\" : \"{0}\" }}", ex.Message.Replace("'", "\\'"));
                //ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxReserveRequirementPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }

    }
}