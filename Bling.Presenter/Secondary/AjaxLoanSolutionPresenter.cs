using System;
using Bling.Domain.Secondary;
using Bling.Repository.Secondary;

namespace Bling.Presenter.Secondary
{
    public class AjaxLoanSolutionPresenter : Presenter
    {
        private IAjaxView m_View;
        private ILoanSolutionDao m_Dao;

        public AjaxLoanSolutionPresenter(IAjaxView view)
            : this(view, new LoanSolutionDao(DMDDataSession()))
        {            
        }

        public AjaxLoanSolutionPresenter(IAjaxView view, ILoanSolutionDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void UpdateInvestor(string loanSolutionInvestor, string dataTracInvestor)
        {
            try
            {
                m_Dao.UpdateInvestor(loanSolutionInvestor, dataTracInvestor);
                GetCurrentMapping();
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
            }
        }

        public void GetCurrentMapping()
        {
            m_View.ResponseText = LSDTInvestorMapping.ToHtmlTable(m_Dao.GetInvestorMapping());
        }

        public void AddLoanSolutionProgramToGEMLock(string userid)
        {
            try
            {
                m_Dao.InsertDataInLSMap(userid);
                m_View.ResponseText = "Done.";
            }
            catch (Exception ex)
            {
                m_View.ResponseText = ex.Message;
            }
            
        }
    }
}
