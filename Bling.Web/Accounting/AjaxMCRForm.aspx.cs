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
    public partial class AjaxMCRForm : BasePage, IAjaxView
    {
        private AjaxMCRFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getendinginfo":
                        m_Presenter.GetEndingMCR(Request.Form["year"], Request.Form["quarter"]); 
                        break;   
                 
                    case "generate":
                        m_Presenter.GenerateXML(Server.MapPath("Report"), Request.Form["year"], Request.Form["quarter"]); 
                        //m_Presenter.GenerateXML(Server.MapPath("Report"), Request.Form["year"], Request.Form["quarter"], Request.Form["state"]); 
                        break;   

                    case "validate":
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
            m_Presenter = new AjaxMCRFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }
    }
}