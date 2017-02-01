using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using Bling.Repository.HR;

namespace Bling.Presenter.HR
{
    public class AjaxStampDatePresenter : Presenter
    {
        private IAjaxView m_View;
        private ICommissionAnalysisDao m_Dao;

        public AjaxStampDatePresenter(IAjaxView view)
            : this(view, new CommissionAnalysisDao(DMDDataSession()))
        {
        }

        public AjaxStampDatePresenter(IAjaxView view, ICommissionAnalysisDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load(string payDate, string endDate, int isWeekly)
        {
            IList<CommissionAnalysis> list = m_Dao.GetLoanForStamping(
                payDate, endDate, isWeekly);
            m_View.ResponseText = CommissionAnalysis.ToHTMLTable(list);
        }

        public void Stamp(string loanNumber, string payDate)
        {
            m_Dao = new CommissionAnalysisDao(MWDataStoreSession());
            m_Dao.StampPayDate(loanNumber, payDate);

            m_View.ResponseText = String.Format("{{ \"LoanNumber\" : \"{0}\", \"StampDate\" : \"{1}\" }}", loanNumber, m_Dao.GetStampedPayDate(loanNumber));
        }

    }
}
