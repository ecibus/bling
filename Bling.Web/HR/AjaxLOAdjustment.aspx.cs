using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bling.Presenter;
using Bling.Presenter.HR;
using Bling.Domain.HR;

namespace Bling.Web.HR
{
    public partial class AjaxLOAdjustment : BasePage, IAjaxView
    {
        private AjaxLOAdjustmentPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getallbylo":
                        m_Presenter.LoadAdjustment(Request.Form["locode"]);
                        break;
                    
                    case "addadjustment":
                        AddAdjustment();
                        break;

                    case "deleteadjustment":
                        DeleteAdjustment();
                        break;

                    case "viewreport":
                        ViewReport();
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
            m_Presenter = new AjaxLOAdjustmentPresenter(this);
            base.OnInit(e);
        }

        private void ViewReport()
        {
            string report = Server.MapPath("Report/LOAdjust.rpt");
            string pdfName = Server.MapPath(String.Format("Report/{0}.pdf", "LOAdjustment"));
            string lo = Request.Form["lo"];
            string from = Request.Form["from"];
            string to = Request.Form["to"];

            m_Presenter.GenerateReport(report, pdfName, lo, from, to);
        }

        private void AddAdjustment()
        {
            LOAdjustment loa = new LOAdjustment
            {
                LOCode = Request.Form["locode"],
                Description = Request.Form["description"],
                LoanNumber = Request.Form["loannumber"],
                DateToPay = Convert.ToDateTime(Request.Form["paydate"]),
                Amount = Convert.ToDecimal(Request.Form["amount"]),
                Comment = Request.Form["comment"]
            };

            m_Presenter.AddAdjustment(loa);
        }

        private void DeleteAdjustment()
        {
            int id = Convert.ToInt32(Request.Form["id"]);
            m_Presenter.RemoveAdjustment(id);
        }
    
        public string ResponseText { get; set; }
    }
}