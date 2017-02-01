using System;
using System.Collections.Generic;
using Bling.Domain.Accounting;
using Bling.Repository.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface IAjaxDocTrustView
    {
        string ResponseText { set; }
    }

    public class AjaxTrustAccountPresenter : Presenter
    {
        private IAjaxDocTrustView m_View;
        private ITrustAccountDao m_Dao;

        public AjaxTrustAccountPresenter(IAjaxDocTrustView view) : 
            this (view, new TrustAccountDao(MWDataStoreSession()))
        {
        }

        public AjaxTrustAccountPresenter(IAjaxDocTrustView view, ITrustAccountDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetEntryByApplicationNumber(string applicationNumber)
        {
            List<TrustAccount> logs = m_Dao.GetByApplicationNumber(applicationNumber);
            m_View.ResponseText = TrustAccount.ConvertListToTable(logs);
        }

        public void DeleteEntryById(int id, string username)
        {            
            TrustAccount trust = m_Dao.GetById(id);
            TrustAccountBackup backup = trust;
            backup.CreatedBy = username;
            m_Dao.SaveBackup(backup);
            m_Dao.RemoveEntry(trust);
        }

    }
}
