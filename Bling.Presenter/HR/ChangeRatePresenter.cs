using System;
using Bling.Repository.HR;
using Bling.Domain.HR;
using System.Collections.Generic;

namespace Bling.Presenter.HR
{
    public interface IChangeRateView
    {
        string RateDropDown { set; }
    }

    public class ChangeRatePresenter : Presenter
    {
        private IChangeRateView m_view;
        private IInsuranceRateDao m_rateDao;

        public ChangeRatePresenter(IChangeRateView view)
            : this (view, new InsuranceRateDao(MWDataStoreSession()))
        {            
        }

        public ChangeRatePresenter(IChangeRateView view, IInsuranceRateDao rateDao)
        {
            m_view = view;
            m_rateDao = rateDao;
        }

        public void Load(string type, decimal currentValue)        
        {
            IList<InsuranceRate> rates = m_rateDao.GetRatesForType(type);
            m_view.RateDropDown = InsuranceRate.ToSelectHTML(rates, currentValue);
        }
    }
}
