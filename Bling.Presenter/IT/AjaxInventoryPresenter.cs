using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using Bling.Domain.IT;
using Bling.Repository.IT;
using log4net;

namespace Bling.Presenter.IT
{
    public class AjaxInventoryPresenter : Presenter
    {
        private IAjaxView m_View;
        private IInventoryDao m_Dao;
        private IInventoryFilterDao m_FilterDao;

        public AjaxInventoryPresenter(IAjaxView view)
            : this (view, new InventoryDao(DMDDataSession()), new InventoryFilterDao(DMDDataSession()))
        {
        }

        public AjaxInventoryPresenter(IAjaxView view, IInventoryDao dao, IInventoryFilterDao filterDao)
        {
            m_View = view;
            m_Dao = dao;
            m_FilterDao = filterDao;
            m_logger = LogManager.GetLogger(typeof(AjaxInventoryPresenter));            
        }

        public void Add(Inventory inventory)
        {
            try
            {
                m_Dao.Add(inventory);
                m_View.ResponseText = "";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }

        public void GetAllInventoryWithPage(int page)
        {
            try
            {
                List<Inventory> inventories = m_Dao.GetAllInventory(page).ToList();
                m_View.ResponseText = Inventory.ToHTMLTable(inventories, page, m_Dao.GetInventoryCount());
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }

        public void Search(int page, string searchstring)
        {
            try
            {
                List<Inventory> inventories = m_Dao.Search(page, searchstring).ToList();
                m_View.ResponseText = Inventory.ToHTMLTable(inventories, page, m_Dao.GetSearchCount(searchstring));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }
        public void GetDistinctAssignTo()
        {
            try
            {
                List<LookUp> users = m_Dao.GetDistictAssignToInInventory().ToList();
                m_View.ResponseText = LookUp.ToHTMLDropDown(users, "ddAssignTo");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }

        public void GetDistinctBranches()
        {
            try
            {
                List<LookUp> users = m_Dao.GetDistinctBranchInInventory().ToList();
                m_View.ResponseText = LookUp.ToHTMLDropDown(users, "ddBranches");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }

        public void GetFilteredData(int page, string assignTo, string branch)
        {
            try
            {
                List<Inventory> inventories = m_FilterDao.GetFilteredData(page, assignTo, branch).ToList();
                m_View.ResponseText = Inventory.ToHTMLTable(inventories, page, m_FilterDao.GetFilteredCount(assignTo, branch));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }

        public void DeleteInventory(int id)
        {
            try
            {
                m_Dao.Delete(id);
            }
            catch (Exception ex)
            {
                m_View.ResponseText = LogError(ex);
            }
        }
    }
}
