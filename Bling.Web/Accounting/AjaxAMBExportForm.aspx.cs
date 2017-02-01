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
    public partial class AjaxAMBExportForm : BasePage, IAjaxView
    {
        private AjaxAMBExportFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["fundedFrom"], Request.Form["fundedTo"]);
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
            m_Presenter = new AjaxAMBExportFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
        
    }
}