using System;
using Bling.Presenter.HR;
using Bling.Domain;

namespace Bling.Web.HR
{
    public partial class InsuranceEnrollment : BasePage, IInsuranceEnrollmentView
    {
        private InsuranceEnrollmentPresenter m_presenter;
        public string YearMonthDropDown { get; set; }
        public string BranchDropDown { get; set; }
        public string MonthDropDown { get; set; }
        public string YearDropDown { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_presenter.Load();
        }

        protected override void OnInit(EventArgs e)
        {
            m_presenter = new InsuranceEnrollmentPresenter(this);            
            base.OnInit(e);
        }
    }
}
