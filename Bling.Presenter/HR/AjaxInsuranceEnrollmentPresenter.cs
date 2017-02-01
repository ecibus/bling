using System;
using System.Linq;
using Bling.Domain.Extension;
using Bling.Repository.HR;
using Bling.Domain.HR;
using System.Collections.Generic;
using System.Text;

namespace Bling.Presenter.HR
{
    public class AjaxInsuranceEnrollmentPresenter : Presenter
    {
        private IAjaxView m_view;
        private IInsuranceTitleDao m_insTitleDao;
        private IInsuranceEnrollmentDao m_insEnrollmentDao;
        private IInsuranceRateDao m_insRateDao;

        public AjaxInsuranceEnrollmentPresenter(IAjaxView view)
            : this (view, new InsuranceTitleDao(MWDataStoreSession()), new InsuranceEnrollmentDao(MWDataStoreSession()), new InsuranceRateDao(MWDataStoreSession()))
        {            
        }

        public AjaxInsuranceEnrollmentPresenter(IAjaxView view, IInsuranceTitleDao insTitleDao, IInsuranceEnrollmentDao insEnrollmentDao, IInsuranceRateDao insRateDao)
        {
            m_view = view;
            m_insTitleDao = insTitleDao;
            m_insEnrollmentDao = insEnrollmentDao;
            m_insRateDao = insRateDao;
        }

        public void UpdateIsLO(int recid, int isLo)
        {
            m_insEnrollmentDao.UpdateIsLO(recid, isLo);
        }
        public void UpdateTitle(string yearmonth, string column, string value)
        {
            m_insTitleDao.UpdateTitle(yearmonth, column, value);
        }

        public void UpdateEEStatus(int recid, string newValue)
        {
            m_insEnrollmentDao.UpdateEEStatus(recid, newValue);
        }

        public void UpdateEnrollment(int recid, int fieldNo, decimal newValue)
        {
            m_insEnrollmentDao.UpdateRate(recid, fieldNo, newValue);
        }

        public void UpdateEmpCost(int recid, decimal newValue)
        {
            m_insEnrollmentDao.UpdateEmpCost(recid, newValue);
        }

        public void AddNewRate(string type, decimal newRate)
        {
            m_insRateDao.AddNewRate(type, newRate);
        }

        public void GetEnrollment(string yearmonth, string branchno)
        {
            m_view.ResponseText = InsuranceEnrollment.ToTableRow(m_insEnrollmentDao.GetByYearMonthAndBranch(yearmonth, branchno));
        }

        public void AddNewMonth(string yearmonth, string year, string month)
        {
            if (yearmonth == String.Empty)
                throw new Exception("Please choose a month to copy.");

            DateTime dtNewMonthYear = new DateTime(year.ToInteger(), month.ToInteger(), 1);

            if (m_insEnrollmentDao.GetByYearMonthAndBranch(year + month, "000").Count > 0) {
                throw new Exception(String.Format("{0} {1} already existed in the insurance enrollment database.", dtNewMonthYear.ToString("MMMM"), dtNewMonthYear.Year));
            }

            m_insEnrollmentDao.Copy(yearmonth, year + month);

            m_view.ResponseText = String.Format("{0} {1} created in the insurance enrollment database.", dtNewMonthYear.ToString("MMMM"), dtNewMonthYear.Year);

        }

        public void EnrollEmployee(string yearMonth, string employeeName, string branchNo, DateTime birthDate, decimal ins1,
            decimal ins3, decimal ins4, decimal ins5, decimal ins6, decimal ins7, decimal ins9, decimal ins10, decimal ins11, decimal ins12,
            decimal empCost, string eeStatus, bool isLO)            
        {
            InsuranceEnrollment enroll = new InsuranceEnrollment { YearMonth = yearMonth, Location = "C", EmployeeName = employeeName,
                BranchNo = branchNo, BirthDate = birthDate, Ins1 = ins1, Ins2 = 0m, Ins3 = ins3, Ins4 = ins4, Ins5 = ins5, Ins6 = ins6,
                Ins7 = ins7, Ins9 = ins9, Ins10 = ins10, Ins11 = ins11, Ins12 = ins12, EmployeeCost = empCost, Data = eeStatus, IsLO = isLO };

            m_insEnrollmentDao.Save(enroll);
        }

        public void RemoveEnrollment(int recid)
        {
            m_insEnrollmentDao.RemoveEnrollment(recid);            
        }

        public void DisplayReport(string reportName, string pdfName, string yearmonth, string branch, int isLO)
        {
            new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@yearmonth", yearmonth)
               .AddParameter("@islo", isLO)
               .AddParameter("@branch", branch)
               .ViewReport();
        }

        public void LoadData()
        {
            StringBuilder json = new StringBuilder();

            json.Append("var Titles = [");

            IList<InsuranceTitle> insuranceTitle = m_insTitleDao.GetAllCorp();
            insuranceTitle.ToList()
                .ForEach(title => json.AppendFormat("[\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\", \"{11}\", \"{12}\"],",
                    title.YearMonth, 
                    title.Title1, title.Title2, title.Title3, title.Title4, title.Title5,
                    title.Title6, title.Title7, title.Title8, title.Title9, title.Title10,
                    title.Title11, title.Title12
                    ));

            json.Remove(json.Length - 1, 1);
            json.Append("]; ");

            m_view.ResponseText = json.ToString();
        }

    }
}
