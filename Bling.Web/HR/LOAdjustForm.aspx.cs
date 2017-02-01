using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class LOAdjustForm : BasePage, ILOAdjustFormView
    {
        private LOAdjustFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new LOAdjustFormPresenter(this);
            base.OnInit(e);
        }

        public string LODropDown { set; get; }
        public string FromPayDate { set; get; }
        public string ToPayDate { set; get; }
    }
}