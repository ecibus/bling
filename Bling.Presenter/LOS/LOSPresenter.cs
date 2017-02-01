using System;
using System.Collections.Generic;
using Bling.Domain.LOS;
using Bling.Repository.LOS;

namespace Bling.Presenter.LOS
{
    public interface ILOSView
    {
        string ResponseText { set; }
    }

    public class LOSPresenter : Presenter
    {
        private ILOSView m_View;
        private IHMDAChanges m_Dao;

        public LOSPresenter(ILOSView view) : this(view, new HMDAChangesDao(DMDDataSession()))      
        {
        }

        public LOSPresenter(ILOSView view, IHMDAChanges dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetHMDAChangesByLoanNumber(string loanNumber)
        {
            List<HMDAChanges> changes = m_Dao.FindByLoanNumber(loanNumber);
            m_View.ResponseText = HMDAChanges.ConvertListToTable(changes);
        }

        public void AddHMDAChanges(HMDAChanges newData)
        {
            m_Dao.Add(newData);
            GetHMDAChangesByLoanNumber(newData.LoanNumber);
        }

        public void DeleteHMDAChanges(int id)
        {
            m_Dao.Delete(id);
        }
    }
}
