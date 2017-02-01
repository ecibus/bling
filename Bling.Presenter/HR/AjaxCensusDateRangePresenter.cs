using System;
using Bling.Domain.HR;
using Bling.Repository.HR;

namespace Bling.Presenter.HR
{
    public class AjaxCensusDateRangePresenter : Presenter
    {
        private IAjaxView m_View;
        private ICensusDateRangeDao m_Dao;

        public AjaxCensusDateRangePresenter(IAjaxView view) :
            this(view, new CensusDateRangeDao(GEMAppSession()))
        {
        }

        public AjaxCensusDateRangePresenter(IAjaxView view, ICensusDateRangeDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void Save(DateTime from, DateTime to)
        {
            CensusDateRange census = m_Dao.GetById(1);
            census.From = from;
            census.To = to;
            m_Dao.Save(census);
        }

    }
}
