using System;
using Bling.Domain.IT;
using Bling.Repository.IT;

namespace Bling.Presenter.IT
{
    public interface IInventoryView
    {
        string InventoryUserDropDown { set; }
    }

    public class InventoryPresenter : Presenter
    {
        private IInventoryView m_View;
        private IInventoryUserDao m_InventoryUserDao;

        public InventoryPresenter(IInventoryView view)
            : this (view, new InventoryUserDao(DMDDataSession()))            
        {            
        }

        public InventoryPresenter(IInventoryView view, IInventoryUserDao inventoryUserDao)
        {
            m_View = view;
            m_InventoryUserDao = inventoryUserDao;
        }

        public void Load()
        {
            try
            {
                m_View.InventoryUserDropDown = InventoryUser.ToHTMLDropDown(m_InventoryUserDao.GetAllUser());
            }
            catch (Exception ex)
            {
                m_View.InventoryUserDropDown = LogError(ex);
            }            
        }
    }
}
