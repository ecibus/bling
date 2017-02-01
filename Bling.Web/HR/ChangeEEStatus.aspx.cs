using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class ChangeEEStatus : BasePage, IChangeEEStatusView
    {
        private ChangeEEStatusPresenter m_presenter;

        public string EEStatusDropDown { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_presenter.Load(Request["val"]);
        }

        protected override void OnLoad(EventArgs e)
        {
            m_presenter = new ChangeEEStatusPresenter(this);
            base.OnLoad(e);
        }
        
    }
}
