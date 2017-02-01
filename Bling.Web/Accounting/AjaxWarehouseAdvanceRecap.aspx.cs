using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AjaxWarehouseAdvanceRecap : BasePage, IAjaxView
    {
        private AjaxWarehouseAdvanceRecapPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.GenerateCSV(Server.MapPath("Report"));
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxWarehouseAdvanceRecapPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}