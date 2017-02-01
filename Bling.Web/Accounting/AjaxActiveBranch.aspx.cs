using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Domain.Extension;
using Bling.Presenter.Accounting;
using Bling.Domain.Accounting;

namespace Bling.Web.Accounting
{
    public partial class AjaxActiveBranch : BasePage, IAjaxView
    {
        private AjaxActiveBranchPresenter m_Presenter;

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

                    case "add":
                        ActiveBranch  rr = new ActiveBranch
                        {
                            MonthEnd = Request.Form["MonthEnd"],
                            CurrentMonth = Request.Form["CurrentMonth"].ToInteger(),
                            CurrentMonthMinus1 = Request.Form["CurrentMonthMinus1"].ToInteger(),
                            CurrentMonthMinus2 = Request.Form["CurrentMonthMinus2"].ToInteger(),
                            FYTD = Request.Form["FYTD"].ToInteger()
                        };

                        m_Presenter.Save(rr);
                        break;

                    case "update":
                        m_Presenter.Update(Request.Form["id"], Request.Form["MonthEnd"],
                            Request.Form["CurrentMonth"],
                            Request.Form["CurrentMonthMinus1"],
                            Request.Form["CurrentMonthMinus2"],
                            Request.Form["FYTD"]);

                        break;

                    case "delete":
                        m_Presenter.Delete(Request.Form["id"].ToInteger());
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
            m_Presenter = new AjaxActiveBranchPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}