using System;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface IAddEnroleeView
    {
        string EmployeeDropdown { set; }
        InsuranceTitle InsuranceTitle { set; }
        string Rate1DropDown { set; }
        string Rate3DropDown { set; }
        string Rate4DropDown { set; }
        string Rate5DropDown { set; }
        string Rate6DropDown { set; }
        string Rate7DropDown { set; }
        string Rate9DropDown { set; }
        string Rate10DropDown { set; }
        string Rate11DropDown { set; }
        string Rate12DropDown { set; }
        string EEStatusDropDown { set; }
    }

    public class AddEnroleePresenter : Presenter
    {
        private IAddEnroleeView m_view;
        private IInsuranceEmployeeInfoDao m_empDao;
        private IInsuranceTitleDao m_titleDao;
        private IInsuranceRateDao m_rateDao;

        public AddEnroleePresenter(IAddEnroleeView view)
            : this (
                view, 
                new InsuranceEmployeeInfoDao(GEMAppSession()), 
                new InsuranceTitleDao(MWDataStoreSession()), 
                new InsuranceRateDao(MWDataStoreSession()))
        {            
        }

        public AddEnroleePresenter(IAddEnroleeView view, IInsuranceEmployeeInfoDao empDao, IInsuranceTitleDao titleDao, IInsuranceRateDao rateDao)
        {
            m_view = view;
            m_empDao = empDao;
            m_titleDao = titleDao;
            m_rateDao = rateDao;
        }

        public void Load(string branchNo, string yearmonth)
        {
            m_view.EmployeeDropdown = InsuranceEmployeeInfo.ToOptionHtml(m_empDao.GetEmployeeByBranch(branchNo));
            m_view.InsuranceTitle = m_titleDao.GetByYearMonth(yearmonth);
            m_view.EEStatusDropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetEEStatus(), "").Replace("EEStatus", "EEStatus_Add");
            m_view.Rate1DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("1"), 0m).Replace("InsuranceRates", "InsuranceRates_1");
            m_view.Rate3DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("3"), 0m).Replace("InsuranceRates", "InsuranceRates_3");
            m_view.Rate4DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("4"), 0m).Replace("InsuranceRates", "InsuranceRates_4");
            m_view.Rate5DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("5"), 0m).Replace("InsuranceRates", "InsuranceRates_5");
            m_view.Rate6DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("6"), 0m).Replace("InsuranceRates", "InsuranceRates_6");
            m_view.Rate7DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("7"), 0m).Replace("InsuranceRates", "InsuranceRates_7");
            m_view.Rate9DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("9"), 0m).Replace("InsuranceRates", "InsuranceRates_9");
            m_view.Rate10DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("10"), 0m).Replace("InsuranceRates", "InsuranceRates_10");
            m_view.Rate11DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("11"), 0m).Replace("InsuranceRates", "InsuranceRates_11");
            m_view.Rate12DropDown = InsuranceRate.ToSelectHTML(m_rateDao.GetRatesForType("12"), 0m).Replace("InsuranceRates", "InsuranceRates_12");
        }
    }
}
