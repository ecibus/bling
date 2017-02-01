using System;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public interface IHideByProgramCodeView
    {
        string ProgramCodeDropdown { set; }
    }

    public class HideByProgramCodePresenter : Presenter
    {
        private IHideByProgramCodeView m_View;
        private ILSMapDao m_Dao;

        public HideByProgramCodePresenter(IHideByProgramCodeView view) : this(view, new LSMapDao(DMDDataSession()))
        {            
        }

        public HideByProgramCodePresenter(IHideByProgramCodeView view, ILSMapDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            m_View.ProgramCodeDropdown = LSMap.BuildLoanCodeDropdownHtml(m_Dao.GetProgramCode());
        }
    }
}
