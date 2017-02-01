using System;
using Bling.Presenter.Secondary;

namespace Bling.Web.Secondary
{
    public partial class HideByProgramCode : BasePage, IHideByProgramCodeView
    {
        protected string m_ProgramCodeDropdown;
        private HideByProgramCodePresenter m_Presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                m_Presenter.Load();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            m_Presenter = new HideByProgramCodePresenter(this);
            base.OnInit(e);
        }

        public string ProgramCodeDropdown
        {
            set { m_ProgramCodeDropdown = value; }
        }
    }
}
