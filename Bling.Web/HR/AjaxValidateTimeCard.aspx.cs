using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.HR;

namespace Bling.Web.HR
{
    public partial class AjaxValidateTimeCard : BasePage, IAjaxView
    {
        private AjaxValidateTimeCardPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "validate":
                        m_Presenter.Validate(Request.Form["start"], Request.Form["end"]);
                        break;
                    case "validate1":
                        m_Presenter.Validate1(Request.Form["start"], Request.Form["end"]);
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = String.Format("{{ 'Error' :  '{0}' }}", ex.Message);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxValidateTimeCardPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { get; set; }

    }
}