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
    public partial class AjaxAuditScoreCard : BasePage, IAjaxView
    {
        private AjaxAuditScoreCardPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "load":
                        m_Presenter.GetLoanInfo(Request.Form["LoanNumber"].Trim());
                        break;

                    case "savescore":
                        m_Presenter.SaveScore(Request.Form["FileId"], Request.Form["ScoreId"].ToInteger(), Request.Form["Score"].ToDouble(), CurrentUser.UserName);
                        break;

                    case "removescore":
                        m_Presenter.RemoveScore(Request.Form["FileId"], Request.Form["ScoreId"].ToInteger());
                        break;

                    case "saveauditor":
                        m_Presenter.SaveInitialAuditorAndDate(Request.Form["LoanNumber"].Trim(), Request.Form["InitialAuditor"].Trim(),
                            Request.Form["InitialAuditorValue"].Trim(),
                            Request.Form["AuditDate"].Trim(), Request.Form["SubmittedDate"].Trim(), CurrentUser.ActorId);
                        break;

                    case "addcategorywithnofindings":
                        m_Presenter.AddCategoryWithNoFindings(Request.Form["FileId"], Request.Form["GroupId"].ToInteger(), CurrentUser.UserName);
                        break;

                    case "removecategorywithfindings":
                        m_Presenter.RemoveCategoryWithFindings(Request.Form["FileId"], Request.Form["GroupId"].ToInteger());
                        break;

                    case "savecommentanditemtype":
                        m_Presenter.SaveCommentAndItemType(Request.Form["FileId"], Request.Form["ItemId"].ToInteger(),
                            Request.Form["comment"], Request.Form["itemtype"], CurrentUser.UserName);
                        break;

                    case "createnote":
                        m_Presenter.PushNotesToDataTrac(Request.Form["FileId"], CurrentUser.ActorId);
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
            m_Presenter = new AjaxAuditScoreCardPresenter(this);

            base.OnInit(e);
        }
        public string ResponseText { get; set; }
    }
}