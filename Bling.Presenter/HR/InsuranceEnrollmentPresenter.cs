using System;
using System.Linq;
using Bling.Repository.HR;
using Bling.Domain.HR;
using Bling.Domain;
using Bling.Repository;
using System.Collections.Generic;

namespace Bling.Presenter.HR
{
    public interface IInsuranceEnrollmentView
    {
        string YearMonthDropDown { set; }
        string BranchDropDown { set; }
        string MonthDropDown { set; }
        string YearDropDown { set; }
    }

    public class InsuranceEnrollmentPresenter : Presenter
    {
        private IInsuranceEnrollmentView m_view;
        private IInsuranceTitleDao m_insTitleDao;
        private ICashDepositBranchDao m_cashDepositBranchDao;
        
        public InsuranceEnrollmentPresenter(IInsuranceEnrollmentView view)
            : this (view, new InsuranceTitleDao(MWDataStoreSession()), new CashDepositBranchDao(MWDataStoreSession()))
        {
            
        }

        public InsuranceEnrollmentPresenter(IInsuranceEnrollmentView view, IInsuranceTitleDao insTitleDao, ICashDepositBranchDao cdbDao)
        {
            m_view = view;
            m_insTitleDao = insTitleDao;
            m_cashDepositBranchDao = cdbDao;
        }

        public void Load()
        {
            IList<InsuranceTitle> insuranceTitle = m_insTitleDao.GetAllCorp();

            m_view.YearMonthDropDown = InsuranceTitle.ToSelectHTML(insuranceTitle);
            m_view.BranchDropDown = CashDepositBranch.ToSelectHTML(m_cashDepositBranchDao.GetAll());
            CalendarHtml cal = new CalendarHtml("hr");
            m_view.MonthDropDown = cal.MonthDropDown(DateTime.Now.ToString("MM"));
            m_view.YearDropDown = cal.YearDropDown(2);
        }
    }
}
