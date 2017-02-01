using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Domain.Extension;
using Bling.Presenter.Compliance;

namespace Bling.Web.Compliance
{
    public partial class AjaxComplianceEase : BasePage, IAjaxView
    {
        private AjaxComplianceEasePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        m_Presenter.Generate(Server.MapPath("Report"), 
                            Request.Form["start"].Trim(),
                            Request.Form["end"].Trim(), Request.Form["loans"]);

                        
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                ResponseText = ex.Message.Escape();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxComplianceEasePresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { set; get; }

    }
}