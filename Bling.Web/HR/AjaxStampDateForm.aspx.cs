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
    public partial class AjaxStampDateForm : BasePage, IAjaxView
    {
        private AjaxStampDatePresenter m_Presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load(Request.Form["payDate"], Request.Form["endDate"],
                            Convert.ToInt32(Request.Form["isWeekly"]));
                        break;

                    case "stamp":
                        m_Presenter.Stamp(Request.Form["loanNumber"], Request.Form["payDate"]);
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxStampDatePresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { get; set; }
    }
}