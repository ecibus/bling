using System;
using Bling.Presenter;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class AjaxChangeProgramCode : BasePage, IAjaxView
    {
        protected string m_ResponseText;
        private AjaxChangeProgramCodePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "loadloan":
                        m_Presenter.LoadLoan(Request["LoanNumber"]);
                        break;

                    case "getlsinvestor":
                        m_Presenter.GetLoanSolutionInvestor(Request["ProgramId"]);
                        break;

                    case "getlsprogramdescription":
                        m_Presenter.GetLoanSolutionProgramDescription(Request["Investor"], Request["ProgramId"]);
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {                
                m_ResponseText = ex.Message;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_subTitle = "AjaxChangeProgramCode";
            m_Presenter = new AjaxChangeProgramCodePresenter(this);
            base.OnInit(e);
        }

        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

    }
}
