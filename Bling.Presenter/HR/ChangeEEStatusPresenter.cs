using System;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface IChangeEEStatusView
    {
        string EEStatusDropDown { set; }
    }

    public class ChangeEEStatusPresenter : Presenter
    {
        private IChangeEEStatusView m_view;
        private IInsuranceRateDao m_rateDao;

        public ChangeEEStatusPresenter(IChangeEEStatusView view)
            : this(view, new InsuranceRateDao(MWDataStoreSession()))            
        {            
        }

        public ChangeEEStatusPresenter(IChangeEEStatusView view, IInsuranceRateDao rateDao)
        {
            m_view = view;
            m_rateDao = rateDao;
        }

        public void Load(string currentData)
        {
            m_view.EEStatusDropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetEEStatus(), currentData);
        }
    }
}
