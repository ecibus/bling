using System;
using Bling.Domain;
using Bling.Repository;
using Bling.Repository.Secondary;
using System.Text;
using System.Collections.Generic;

namespace Bling.Presenter.Secondary
{
    public class AjaxChangeProgramCodePresenter : Presenter
    {
        private IAjaxView m_View;
        private IGenDao m_GenDao;
        private ILoanSolutionDao m_LoanSolutionDao;

        public AjaxChangeProgramCodePresenter(IAjaxView view) :
            this(view, new GenDao(DMDDataSession()), new LoanSolutionDao(DMDDataSession()))
        {
        }

        public AjaxChangeProgramCodePresenter(IAjaxView view, IGenDao genDao, ILoanSolutionDao loanSolutionDao)
        {
            m_View = view;
            m_GenDao = genDao;
            m_LoanSolutionDao = loanSolutionDao;
        }

        public void LoadLoan(string loanNumber)
        {
            Gen gen = m_GenDao.GetByLoanNumber(loanNumber);
            m_View.ResponseText = gen.ToJson();
        }

        public void GetLoanSolutionInvestor(string programId)
        {
            List<string> investor = m_LoanSolutionDao.GetLSInvestorByProgramId(programId);

            StringBuilder json = new StringBuilder();
            json.Append("data = { \"Investor\": [");
            investor.ForEach(i => json.AppendFormat("\"{0}\",", i.ToUpper()));
            if (investor.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.Append("] }");

            m_View.ResponseText = json.ToString();
        }

        public void GetLoanSolutionProgramDescription(string investor, string programId)
        {
            List<string> programDescription = m_LoanSolutionDao.GetProgramDescriptionByInvestorAndProgramId(investor, programId);

            StringBuilder json = new StringBuilder();
            
            json.Append("data = { \"ProgramDescription\": [");
            programDescription.ForEach(i => json.AppendFormat("\"{0}\",", i.ToUpper()));
            if (programDescription.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.Append("] }");

            m_View.ResponseText = json.ToString();
        }
    }
}
