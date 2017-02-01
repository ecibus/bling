using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Accounting;

namespace Bling.Web.HR
{
    public partial class ByteReportForm : BasePage, IAjaxView
    {
        private AjaxByteReportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.GetReports(Server.MapPath("Byte"));
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxByteReportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}