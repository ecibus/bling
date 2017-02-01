using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Funding;
using Bling.Domain.Funding;

namespace Bling.Presenter.Funding
{
    public class AjaxFundingExceptionSummaryPresenter : Presenter
    {
        public IAjaxView m_View;
        public IFundingExceptionSummaryDao m_Dao;

        public AjaxFundingExceptionSummaryPresenter(IAjaxView view)
            : this (view, new FundingExceptionSummaryDao(DMDDataSession()))
        {
        }

        public AjaxFundingExceptionSummaryPresenter(IAjaxView view, IFundingExceptionSummaryDao dao)
        {
            m_Dao = dao;
            m_View = view;
        }

        public void Load(int month, int year)
        {
            var list = m_Dao.GetList(month, year);

            m_View.ResponseText = FundingExceptionSummary.ToTable(list);
        }

        public void SaveComment(int month, int year, string brokerId, string comment)
        {
            m_Dao.SaveComment(month, year, brokerId, comment);

        }
    }
}
