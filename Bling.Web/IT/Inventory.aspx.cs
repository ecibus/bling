using System;
using Bling.Presenter.IT;

namespace Bling.Web.IT
{
    public partial class Inventory : BasePage, IInventoryView
    {
        private InventoryPresenter m_Presenter;
        protected string m_UserDropDown;

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
            m_Presenter = new InventoryPresenter(this);
            base.OnInit(e);
        }

        public string InventoryUserDropDown
        {
            set { m_UserDropDown = value; }
        }
    }
}
