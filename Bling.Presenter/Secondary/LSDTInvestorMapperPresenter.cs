using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Secondary;
using Bling.Domain;
using Bling.Domain.Secondary;
using Bling.Repository;

namespace Bling.Presenter.Secondary
{
    public interface ILSDTInvestorMapperView
    {
        string InvestorMapping { set; }
    }

    public class LSDTInvestorMapperPresenter : Presenter
    {
        private ILSDTInvestorMapperView m_View;
        private ILoanSolutionDao m_LSDao;
        private IInvestorDao m_IDao;
        private ILSDTInvestorMappingDao m_LSDTDao;

        public LSDTInvestorMapperPresenter(ILSDTInvestorMapperView view) :
            this (view, new LoanSolutionDao(DMDDataSession()), 
            new InvestorDao(DMDDataSession()), new LSDTInvestorMappingDao(DMDDataSession()))            
        {
        }

        public LSDTInvestorMapperPresenter(ILSDTInvestorMapperView view, ILoanSolutionDao lsdao, 
            IInvestorDao idao, ILSDTInvestorMappingDao lsdtdao)
        {
            m_View = view;
            m_LSDao = lsdao;
            m_IDao = idao;
            m_LSDTDao = lsdtdao;
        }

        public void LoadData()
        {
            List<string> loanSolutionInvestor = m_LSDao.GetLSInvestor();
            if (loanSolutionInvestor.Count == 0)
            {
                m_View.InvestorMapping = "";
                return;
            }

            StringBuilder tableHtml = new StringBuilder();

            List<Investor> investors = m_IDao.GetAllActiveInvestor();
            List<LSDTInvestorMapping> mapping = m_LSDTDao.GetAll().ToList();                       

            tableHtml.Append("<table>");
            tableHtml.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", "Loan Solution Investor", "DataTrac Investor");
            loanSolutionInvestor.ForEach(x => 
                tableHtml.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", x, 
                Investor.ToSelectHtml(investors, x, LSDTInvestorMapping.GetCodeFor(x, mapping))));
            tableHtml.Append("</table>");

            m_View.InvestorMapping = tableHtml.ToString();
        }
    }
}
