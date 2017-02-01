using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class BenefitNotification : BasePage, IBenefitNotificationView
    {
        public string From { get; set; } 
        public string To { get; set; }
        private BenefitNotificationPresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime now = DateTime.Now;
                DateTime nextMonth = now.AddMonths(1);
                nextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1);
                From = String.Format("{0}/01/{1}", now.Month, now.Year);
                To = nextMonth.AddDays(-1).Date.ToShortDateString();
            }
            else
            {
                From = Request.Form["txtFrom"];
                To = Request.Form["txtTo"];
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {            
            try
            {
                string report = "Report/BenefitNotification.rpt";
         
                _presenter.ViewReport(Server.MapPath(report));

                string popupScript =
                    String.Format("$(function () {{ window.open('{0}?r={1}', 'Report'); }});",
                    report.Replace(".rpt", ".pdf"), new Random().Next(1, 1000).ToString());

                ClientScript.RegisterStartupScript(GetType(), "Report", popupScript, true);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            _presenter = new BenefitNotificationPresenter(this);
            base.OnLoad(e);
        }


    }
}