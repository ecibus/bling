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
    public partial class AjaxTurningPoint : BasePage, IAjaxView
    {
        private AjaxTurningPointPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load();
                        break;

                    case "search":
                        m_Presenter.Search(Request.Form["crit"]);
                        break;

                    case "add":
                        m_Presenter.Add(Request.Form["username"]);
                        break;

                    case "remove":
                        m_Presenter.Remove(Request.Form["username"]);
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }

        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxTurningPointPresenter(this);
            base.OnInit(e);
        }


        public string ResponseText { get;set; }
    }
}