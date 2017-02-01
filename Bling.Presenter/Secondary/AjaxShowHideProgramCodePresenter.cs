using System;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public class AjaxShowHideProgramCodePresenter : Presenter
    {
        private IAjaxView m_View;
        private IProgramCodeByInvestorDao m_Dao;
        private ILSMapDao m_LSMapDao;

        public AjaxShowHideProgramCodePresenter(IAjaxView view) 
            : this(view, new ProgramCodeByInvestorDao(DMDDataSession()), new LSMapDao(DMDDataSession()))            
        {            
        }

        public AjaxShowHideProgramCodePresenter(IAjaxView view, IProgramCodeByInvestorDao dao, ILSMapDao lsmapdao)
        {
            m_View = view;
            m_Dao = dao;
            m_LSMapDao = lsmapdao;
        }

        public void GetProgramCodeByInvestor(int t)
        {            
            m_View.ResponseText = ProgramCodeByInvestor.ToHtmlTable(m_Dao.GetProgramCodeByInvestor(t));
        }

        public void ShowHideByInvestor(string investor, string hide, int t, string updatedby)
        {
            m_Dao.ShowHideByInvestor(investor, hide, updatedby);
            m_View.ResponseText = ProgramCodeByInvestor.ToHtmlTable(m_Dao.GetProgramCodeByInvestor(t));
        }

        public void GetProgramByProgramCode (string code)
        {
            m_View.ResponseText = LSMap.ToHtmlTable(m_LSMapDao.GetByProgramCode(code));
        }

        public void UpdateProgramCode (int id, bool hide, string updatedby)
        {
            try
            {
                LSMap lsmap = m_LSMapDao.GetById(id);
                if (lsmap.Exclude != hide)
                {
                    lsmap.Exclude = hide;
                    lsmap.UpdatedBy = updatedby;
                    lsmap.UpdatedOn = DateTime.Now;
                    m_LSMapDao.Save(lsmap);
                }
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
            }
        }
    }
}
