using System;
using Bling.Domain.HR;
using Bling.Repository.HR;

namespace Bling.Presenter.HR
{
    public interface ICensusDateRangeView
    {
        DateTime From { set; }
        DateTime To { set; }
    }

    public class CensusDateRangePresenter : Presenter
    {
        private ICensusDateRangeView m_View;
        private ICensusDateRangeDao m_Dao;

        public CensusDateRangePresenter(ICensusDateRangeView view) :
            this(view, new CensusDateRangeDao(GEMAppSession()))
        {

        }

        public CensusDateRangePresenter(ICensusDateRangeView view, ICensusDateRangeDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Load()
        {
            CensusDateRange census = m_Dao.GetById(1);
            m_View.From = census.From;
            m_View.To = census.To;
        }
    }
}
