using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class CommissionReport : BasePage, ICommissionReportView
    {
        protected CommissionReportPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnLoad(EventArgs e)
        {
            m_Presenter = new CommissionReportPresenter(this);
            base.OnLoad(e);
        }

        public string LODropDown { get; set; }
        public string FundedFrom { get; set; }
        public string FundedTo { get; set; }

    }
}