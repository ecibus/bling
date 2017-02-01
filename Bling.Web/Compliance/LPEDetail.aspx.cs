using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Compliance;

namespace Bling.Web.Compliance
{
    public partial class LPEDetail : BasePage, ILPEDetailView
    {
        private LPEDetailPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new LPEDetailPresenter(this);
            base.OnInit(e);
        }
        public string ReadyForDocsForm { get; set; }

        public string LoanNumber
        {
            get { return Request.QueryString["loanNumber"]; }
        }
    }
}