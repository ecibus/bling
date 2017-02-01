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
    public partial class AjaxCommissionAnalysis : BasePage, IAjaxView
    {
        private AjaxCommissionAnalysisPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "loadawaitingapproval":
                        LoadAwaitingApproval();
                        break;

                    case "loadloan":
                        LoadLoan();
                        break;

                    case "savecommissionanalysis":
                        SaveCommissionAnalysis();
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

        public void SaveCommissionAnalysis()
        {
            m_Presenter.Save(Request.Form["loannumber"], Request.Form["status"],
                Request.Form["approvedlo"], Request.Form["comment"], Request.Form["payDate"]);
        }

        public void LoadAwaitingApproval()
        {
            m_Presenter.LoadAwaitingApproval(); 
        }

        public void LoadLoan()
        {
            m_Presenter.LoadLoan(Request.Form["loannumber"]);
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxCommissionAnalysisPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }



    
}