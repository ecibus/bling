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
    public partial class AjaxPennyMacCDRForm : BasePage, IAjaxView
    {
        private AjaxPennyMacCDRFormPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "generate":
                        Generate();
                        break;

                    //case "generatecdr":
                    //    GenerateCDR();
                    //    break;

                    //case "generateepp":
                    //    GenerateEPP();
                    //    break;
                }

            }
            catch (Exception ex)
            {
                ResponseText = ex.Message;
            }
        }

        private void Generate()
        {
            List<string> list = Request.Form["loans"].Split('\n').ToList();
            m_Presenter.Generate(Server.MapPath("Report"), list, Request.Form["csvtype"], Request.Form["targetFile"]);
        }

        //private void GenerateCDR()
        //{
        //    List<string> list = Request.Form["loans"].Split('\n').ToList();
        //    m_Presenter.Generate(Server.MapPath("Report"), list, "cdr", "PennyMacCDR.csv");
        //}

        //private void GenerateEPP()
        //{
        //    List<string> list = Request.Form["loans"].Split('\n').ToList();
        //    m_Presenter.GenerateEPP(Server.MapPath("Report"), list);
        //}

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxPennyMacCDRFormPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { get; set; }

    }
}