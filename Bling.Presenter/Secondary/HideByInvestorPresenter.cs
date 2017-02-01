using System;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public interface IHideByInvestorView
    {
        string InvestorDropdown { set; }
        string InvestorSummary { set; }
    }

    public class HideByInvestorPresenter : Presenter
    {
        private IHideByInvestorView m_View;
        private ILSMapDao m_LSMapDao;
        private IProgramCodeByInvestorDao m_ProgramCodeByInvestorDao;

        public HideByInvestorPresenter(IHideByInvestorView view) : 
            this(view, new LSMapDao(DMDDataSession()), new ProgramCodeByInvestorDao(DMDDataSession()))
        {
        }

        public HideByInvestorPresenter(IHideByInvestorView view, ILSMapDao lsmapdao, IProgramCodeByInvestorDao pcbidao)
        {
            m_View = view;
            m_LSMapDao = lsmapdao;
            m_ProgramCodeByInvestorDao = pcbidao;
        }

        public void Load()
        {            
            m_View.InvestorDropdown = LSMap.BuildInvestorDropdownHtml(m_LSMapDao.GetInvestor());
            m_View.InvestorSummary = ProgramCodeByInvestor.ToHtmlTable(m_ProgramCodeByInvestorDao.GetProgramCodeByInvestor(1));
        }
    }
}
