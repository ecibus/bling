using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;

namespace Bling.Presenter.Compliance
{
    public interface ILPEView
    {
        string ReadyForDocsTable { set; }
    }

    public class LPEPresenter : Presenter
    {
        private ILPEView m_View;
        private ILPELoanInfoDao m_Dao;

        public LPEPresenter(ILPEView view)
            : this (view, new LPELoanInfoDao(DMDDataSession()))
        {
        }

        public LPEPresenter(ILPEView view, ILPELoanInfoDao dao)
        {
            m_View = view;
            m_Dao = dao;            
        }

        public void Load()
        {
            m_View.ReadyForDocsTable = LPELoanInfo.ToTable(m_Dao.GetReadyForDocs());
        }
    }
}
