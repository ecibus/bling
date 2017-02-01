using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Domain;

namespace Bling.Web.Secondary
{
    public partial class CSVExportForm : BasePage
    {
        public string From { get; set; }
        public string To { get; set; }

        private CSVExportPresenter m_Presenter;
        public List<CSVExport> CSVExport;

        protected void Page_Load(object sender, EventArgs e)
        {
            From = To = DateTime.Now.ToString("MM/dd/yyyy");
            CSVExport = m_Presenter.GetByType("Secondary");
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new CSVExportPresenter();
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}