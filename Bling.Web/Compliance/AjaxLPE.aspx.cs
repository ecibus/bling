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
    public partial class AjaxLPE : BasePage, IAjaxView
    {
        private AjaxLPEPresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "readyfordocs":
                        m_Presenter.UpdateReadyForDocs(Request.Form["LoanNumber"].Trim(), Request.Form["NewValue"].Trim());
                        break;

                    case "updatereasonandcomment":
                        m_Presenter.UpdateReasonAndComment(Request.Form["LoanNumber"].Trim(),
                            Request.Form["reasonId"].Trim(), Request.Form["comment"].Trim(), CurrentUser.UserName);
                        break;

                    case "initialreviewcomplete":
                        m_Presenter.InitialReviewComplete(Request.Form["LoanNumber"].Trim(),
                            CurrentUser.UserName);
                        break;

                    case "loadloan":
                        m_Presenter.LoadLoan(Request.Form["LoanNumber"].Trim());
                        break;

                    case "getlastchanges":
                        m_Presenter.GetLastChanges(Request.Form["LoanNumber"].Trim());
                        break;

                    case "addhistory":
                        m_Presenter.AddHistory(CurrentUser.UserName,
                            Request.Form["LoanNumber"].Trim(),
                            Request.Form["Borrower"].Trim(),
                            Request.Form["LoanType"].Trim(),
                            Request.Form["LoanAmount"].Trim(),
                            Request.Form["GEMLoanFeeCharged"].Trim(),
                            Request.Form["LoanOriginationFeeCharged"].Trim(),
                            Request.Form["LoanOfficerPrice"].Trim(),
                            Request.Form["BorrowerPaidDiscount"].Trim(),
                            Request.Form["LenderCredit"].Trim(),
                            Request.Form["FICOScore"].Trim(),
                            Request.Form["ApplicationDate"].Trim(),
                            Request.Form["LockedDate"].Trim(),
                            Request.Form["NoOfBorrower"].Trim(),
                            Request.Form["ProgramType"].Trim(),
                            Request.Form["TransactionType"].Trim(),
                            Request.Form["FinalNetPricePoint"].Trim());                            
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
            m_Presenter = new AjaxLPEPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText { set; get; }
    }
}