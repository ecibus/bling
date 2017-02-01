using System;
using Bling.Domain.Extension;
using Bling.Presenter;
using Bling.Presenter.Underwriting;

namespace Bling.Web.Underwriting
{
    public partial class AjaxScoreCard : BasePage, IAjaxView
    {
        private AjaxScoreCardPresenter m_Presenter;
        protected string m_ResponseText;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.Load(Request.Form["LoanNumber"].ToString().Trim());
                        break;

                    case "savescore":
                        m_Presenter.SaveScore(Request.Form["FileId"], Request.Form["ScoreId"].ToInteger(), Request.Form["Score"].ToDouble(), CurrentUser.UserName);
                        break;

                    case "removescore":
                        m_Presenter.RemoveScore(Request.Form["FileId"], Request.Form["ScoreId"].ToInteger());
                        break;

                    case "savecomment":
                        m_Presenter.SaveComment(Request.Form["FileId"], Request.Form["GroupId"].ToInteger(), Request.Form["Comment"], CurrentUser.UserName);
                        break;

                    case "addperfectloan":
                        m_Presenter.AddPerfectLoan(Request.Form["FileId"], CurrentUser.UserName);
                        break;

                    case "removeperfectloan":
                        m_Presenter.RemovePerfectLoan(Request.Form["FileId"]);
                        break;

                    case "addcategorywithnofindings":
                        m_Presenter.AddCategoryWithNoFindings(Request.Form["FileId"], Request.Form["GroupId"].ToInteger(), CurrentUser.UserName);
                        break;

                    case "removecategorywithfindings":
                        m_Presenter.RemoveCategoryWithFindings(Request.Form["FileId"], Request.Form["GroupId"].ToInteger());
                        break;

                    case "printpreview":
                        string report = Server.MapPath("Report/ScoreCard.rpt");
                        string pdfName = Server.MapPath("Report/ScoreCard.pdf");
                        m_Presenter.PrintPreview(report, pdfName, Request.Form["LoanNumber"]);
                        break;


                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                m_ResponseText = ex.Message.Replace("'", "\\'");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new AjaxScoreCardPresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }
    }
}
