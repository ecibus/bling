using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public class AjaxCommissionAnalysisPresenter : Presenter
    {
        private IAjaxView m_View;
        private ICommissionAnalysisDao m_Dao;

        public AjaxCommissionAnalysisPresenter(IAjaxView view)
            : this(view, new CommissionAnalysisDao(DMDDataSession()))
        {
        }

        public AjaxCommissionAnalysisPresenter(IAjaxView view, ICommissionAnalysisDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void LoadAwaitingApproval()
        {
            IList<CommissionAnalysis> list = m_Dao.GetAwaitingApproval();
            m_View.ResponseText = CommissionAnalysis.ToHTMLTable(list);
        }

        public void Save(string loanNumber, string status, string approvedLO, string comment, string payDate)
        {
            try
            {
                var dao = new CommissionAnalysisDao(MWDataStoreSession());
                dao.Save(loanNumber, status, approvedLO, comment, payDate);

                m_View.ResponseText = String.Format(" {{ \"Message\" : \"{0}\"}}","");
            }
            catch (Exception e)
            {
                m_View.ResponseText = String.Format(" {{ \"Message\" : \"{0}\"}}", e.Message);
            }
        }

        public void LoadLoan(string loanNumber)
        {
            try
            {
                var loan = m_Dao.GetLoan(loanNumber);

                if (loan == null)
                {
                    m_View.ResponseText = String.Format(" {{ \"Message\" : \"The Loan {0} does not exist.\"}}", loanNumber);

                    return;
                }

                if (String.IsNullOrEmpty(loan.FundedDate))
                {
                    m_View.ResponseText = String.Format(" {{ \"Message\" : \"The Loan {0} is not yet Funded.\"}}", loanNumber);
                    return;
                }

                m_View.ResponseText = loan.ToJson();
            }
            catch (Exception e)
            {
                m_View.ResponseText = String.Format(" {{ \"Message\" : \"{0}\"}}", e.Message);
            }
        }
    }
}
