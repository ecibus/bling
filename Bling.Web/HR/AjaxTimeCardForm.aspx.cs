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
    public partial class AjaxTimeCardForm : BasePage, IAjaxView
    {
        private AjaxTimeCardPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "gettimecard":
                        m_Presenter.GetTimeCard(Convert.ToBoolean(Request.Form["accepted"]), Convert.ToInt32(Request.Form["month"]), 
                            Convert.ToInt32(Request.Form["year"]));
                        break;

                    case "rejecttimecard":
                        m_Presenter.RejectTimeCard(Convert.ToInt32(Request.Form["submitid"]), CurrentUser.UserInfo.FullName,
                            CurrentUser.UserInfo.EMail);
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
            m_Presenter = new AjaxTimeCardPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}