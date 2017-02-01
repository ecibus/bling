using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class Ajax401K : BasePage, IAjaxView
    {
        private Ajax401KPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "loaddates":
                        m_Presenter.LoadDates(Request.Form["reportType"]);
                        break;

                    case "view401kreport":
                        m_Presenter.View401KReport(Server.MapPath("Report/401k.rpt"), Server.MapPath("Report/401k.pdf"),
                            Request.Form["start"], Request.Form["end"], Request.Form["isWeekly"]);
                        break;

                    case "generatecsv":
                        m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["start"], Request.Form["end"], Request.Form["isWeekly"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                ResponseText = String.Format("{{ 'Error' :  '{0}' }}", ex.Message);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new Ajax401KPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { get; set; }

    }
}