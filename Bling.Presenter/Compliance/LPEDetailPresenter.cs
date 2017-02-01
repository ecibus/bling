using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;

namespace Bling.Presenter.Compliance
{
    public interface ILPEDetailView
    {
        string ReadyForDocsForm { set; }
        string LoanNumber { get; }
    }

    public class LPEDetailPresenter : Presenter
    {
        private ILPEDetailView m_View;
        private ILPELoanInfoDao m_Dao;
        private ILPEReasonDao m_ReasonDao;

        public LPEDetailPresenter(ILPEDetailView view)
            : this(view, new LPELoanInfoDao(DMDDataSession()), new LPEReasonDao(DMDDataSession()))
        {
        }

        public LPEDetailPresenter(ILPEDetailView view, ILPELoanInfoDao dao, ILPEReasonDao reasonDao)
        {
            m_View = view;
            m_Dao = dao;
            m_ReasonDao = reasonDao;
        }

        public void Load()
        {
            //m_View.ReadyForDocsForm = m_Dao.GetLoanInfo(m_View.LoanNumber).ToForm(LPEReason.ToLookUp(m_ReasonDao.GetAll()));
            
        }
    }
}
