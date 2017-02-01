using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using System.Text;
using Bling.Presenter.Funding;

namespace Bling.Web.Funding
{
    public partial class AjaxFundingExceptionSummaryForm : BasePage, IAjaxView
    {
        protected AjaxFundingExceptionSummaryPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "loaddata":
                        LoadData(Convert.ToInt32(Request.Form["m"]), Convert.ToInt32(Request.Form["y"]));
                        break;

                    case "savecomment":
                        SaveComment(Convert.ToInt32(Request.Form["m"]), Convert.ToInt32(Request.Form["y"]),
                            Request.Form["brokerId"], Request.Form["comment"]);
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

        public void LoadData(int month, int year)
        {
            m_Presenter.Load(month, year);
        }

        public void SaveComment(int month, int year, string brokerId, string comment)
        {
            m_Presenter.SaveComment(month, year, brokerId, comment);
        }

        protected override void OnLoad(EventArgs e)
        {
            m_Presenter = new AjaxFundingExceptionSummaryPresenter(this);
            base.OnLoad(e);
        }

        public string ResponseText { get; set; }
    }
}