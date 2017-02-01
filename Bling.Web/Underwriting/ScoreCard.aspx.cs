using System;
using Bling.Presenter.Underwriting;

namespace Bling.Web.Underwriting
{
    public partial class ScoreCard : BasePage, IScoreCardView
    {
        protected ScoreCardPresenter m_Presenter;
        protected string m_Html;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new ScoreCardPresenter(this);
            base.OnInit(e);
        }

        public string ScoreHtml
        {
            set { m_Html = value; }
        }
    }
}
