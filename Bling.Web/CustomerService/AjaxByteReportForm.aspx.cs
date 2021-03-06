﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Accounting;
using Bling.Presenter;

namespace Bling.Web.CustomerService
{
    public partial class AjaxByteReportForm : BasePage, IAjaxView
    {
        private AjaxByteReportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getparameters":
                        string reportFileName = Server.MapPath("Byte\\" + Request["reportName"].ToString() + ".rpt");

                        m_Presenter.GetParameters(reportFileName);
                        break;

                    case "getreports":
                        m_Presenter.GetReports(Server.MapPath("Byte"));
                        break;

                    case "viewreport":
                        m_Presenter.ViewReport(Request["ReportName"], Request["PdfName"], Request["Parameters"]);
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

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxByteReportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }

    }
}