using System;
using Bling.Presenter;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class AjaxLoanSolution : BasePage, IAjaxView
    {
        private AjaxLoanSolutionPresenter m_Presenter;
        protected string m_ResponseText;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "updateinvestor":
                        m_Presenter.UpdateInvestor(Request["lsinvestor"].ToString(), Request["dtinvestor"].ToString());
                        break;

                    case "getcurrentmapping":
                        m_Presenter.GetCurrentMapping();
                        break;

                    case "addloansolutionprogramtogemlock":
                        m_Presenter.AddLoanSolutionProgramToGEMLock(CurrentUser.UserName);
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
            m_subTitle = "AjaxLoanSolution";
            m_Presenter = new AjaxLoanSolutionPresenter(this);
            base.OnInit(e);
        }
        public string ResponseText
        {
            set { m_ResponseText = value; }
        }

    }
}
