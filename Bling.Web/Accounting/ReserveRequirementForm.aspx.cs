using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.Accounting;

namespace Bling.Web.Accounting
{
    public partial class ReserveRequirementForm : BasePage, IReserveRequirementView
    {
        private ReserveRequirementPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            //m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new ReserveRequirementPresenter(this);
            base.OnInit(e);
        }

        public string Data { set; get; }
    }
}