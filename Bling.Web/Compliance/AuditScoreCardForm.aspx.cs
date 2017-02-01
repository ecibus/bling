using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Compliance;

namespace Bling.Web.Compliance
{
    public partial class AuditScoreCardForm : BasePage, IAuditScoreCardFormView
    {
        private AuditScoreCardFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AuditScoreCardFormPresenter(this);
            base.OnInit(e);
        }

        public string InitialAuditorDropdown { get; set; }
        public string ScoreHtml { get; set; }
    }
}