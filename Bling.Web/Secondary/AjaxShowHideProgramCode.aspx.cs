using System;
using Bling.Domain.Extension;
using Bling.Presenter.Secondary;
using log4net;

namespace Bling.Web.Secondary
{
    public partial class AjaxShowHideProgramCode : AjaxBasePage
    {
        private AjaxShowHideProgramCodePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                    return;

                switch (Request["Type"].ToString().ToLower())
                {
                    case "getprogramcodebyinvestor":
                        m_Presenter.GetProgramCodeByInvestor(Request["t"].ToString().ToInteger());
                        break;

                    case "showhidebyinvestor":
                        m_Presenter.ShowHideByInvestor(Request["investor"].ToString(), 
                            Request["hide"].ToString(),
                            Request["t"].ToString().ToInteger(),
                            CurrentUser.UserName);
                        break;

                    case "getprogrambyprogramcode":
                        m_Presenter.GetProgramByProgramCode(Request["code"].ToString());
                        break;

                    case "updateprogramcode":
                        m_Presenter.UpdateProgramCode( 
                            Request["id"].ToString().ToInteger(), 
                            Request["hide"].ToString().ToBoolean(),
                            CurrentUser.UserName);
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
            m_subTitle = "AjaxShowHideProgramCode";
            m_logger = LogManager.GetLogger(typeof(AjaxShowHideProgramCode));
            m_Presenter = new AjaxShowHideProgramCodePresenter(this);
            base.OnInit(e);
        }
    }
}
