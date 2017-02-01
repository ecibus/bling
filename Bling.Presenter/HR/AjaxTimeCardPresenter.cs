using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;
using Bling.Repository;

namespace Bling.Presenter.HR
{
    public class AjaxTimeCardPresenter : Presenter
    {
        private IAjaxView m_View;
        private ITimeCardDao m_Dao;
        private ITimeCardSubmitDao m_TCSDao;
        private IUserInfoDao m_UIDao;

        public AjaxTimeCardPresenter(IAjaxView view)
            : this (view, new TimeCardDao(DMDDataSession()), new TimeCardSubmitDao(DMDDataSession()), new UserInfoDao(DMDDataSession()))
        {
        }

        public AjaxTimeCardPresenter(IAjaxView view, ITimeCardDao dao, ITimeCardSubmitDao tcsDao, IUserInfoDao uiDao)
        {
            m_View = view;
            m_Dao = dao;
            m_TCSDao = tcsDao;
            m_UIDao = uiDao;
        }

        public void GetTimeCard(bool accepted, int month, int year)
        {
            m_View.ResponseText = m_Dao.GetTimeCard(accepted, month, year);
        }

        public void RejectTimeCard(int submitId, string employeeName, string employeeEmail)
        {
            TimeCardSubmit  tcs = m_TCSDao.Reject(submitId);

            if (tcs != null)
            {
                string email = m_UIDao.GetEmailByLoginName(tcs.Username);
                //string email = m_UIDao.GetById(tcs.EmployeeId).EMail;
                tcs.SendEmailToEmployee(email, tcs.Month, tcs.Year, employeeName, employeeEmail);
            }
            m_View.ResponseText = "{ }";
        }
    }
}
