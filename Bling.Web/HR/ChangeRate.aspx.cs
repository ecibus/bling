using System;
using Bling.Presenter.HR;
using Bling.Domain.Extension;

namespace Bling.Web.HR
{
    public partial class ChangeRate : BasePage, IChangeRateView
    {
        private ChangeRatePresenter m_presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_presenter.Load(Request["Type"], Request["val"].ToDecimal());
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new ChangeRatePresenter(this);
            base.OnInit(e);
        }

        public string RateDropDown { get; set; }
    }
}
