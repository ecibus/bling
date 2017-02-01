using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.Funding;

namespace Bling.Web.Funding
{
    public partial class AjaxUSBForm : BasePage, IAjaxView
    {
        private AjaxUSBFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generatecsv":
                        GenerateCSV();
                        break;

                    case "previewcsv":
                        PreviewCSV();
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

        private void GenerateCSV()
        {
            ResponseText = m_Presenter.GenerateCSV(Server.MapPath("Report"), Request.Form["from"], Request.Form["to"], Request.Form["batchno"]);
        }

        private void PreviewCSV()
        {
            ResponseText = m_Presenter.PreviewCSV(Server.MapPath("Report"), Request.Form["from"], Request.Form["to"], Request.Form["batchno"]);
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxUSBFormPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText { get; set; }

    }
    
}