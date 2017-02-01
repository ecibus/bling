using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class LOBasisPoints : BasePage, ILOBasisPointsView
    {
        private LOBasisPointsPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_Presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new LOBasisPointsPresenter(this);
            base.OnInit(e);
        }

        public string LODropDown { get; set; }
    }
}