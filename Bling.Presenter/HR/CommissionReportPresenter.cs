using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository;
using Bling.Domain;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface ICommissionReportView
    {
        string LODropDown { set; }
        string FundedFrom { set; get; }
        string FundedTo { set; get; }
    }

    public class CommissionReportPresenter : Presenter
    {
        private ICommissionReportView m_View;
        private IUserInfoDao m_UserInfoDao;

        public CommissionReportPresenter(ICommissionReportView view)
            : this (view, new UserInfoDao(DMDDataSession()))
        {
        }

        public CommissionReportPresenter(ICommissionReportView view, IUserInfoDao dao)
        {
            m_View = view;
            m_UserInfoDao = dao;
        }

        public void Load()
        {
            //List<UserInfo> lo = m_UserInfoDao.GetAllLO();
            List<ByteLO> lo = m_UserInfoDao.GetAllByteLO();
            List<ByteLO> uniqueLO = new List<ByteLO>();

            foreach (var l in lo)
            {
                if (uniqueLO.Find(x => x.UserName == l.UserName) == null)
                {
                    uniqueLO.Add(l);
                }
            }

            StringBuilder html = new StringBuilder();

            html.Append("<select id='LO'>");
            html.Append("<option value=''>-- Choose Loan Officer --</option>");
            uniqueLO.ForEach(x => html.AppendFormat("<option value='{0}'>{1}</option>",
                x.UserName, x.FullName));
            html.Append("</select>");

            m_View.LODropDown = html.ToString();

            DateTime now = DateTime.Now;
            m_View.FundedFrom = Convert.ToDateTime(now.Month.ToString() + "/1/" + now.Year.ToString()).ToString("MM/dd/yyyy");
            m_View.FundedTo = Convert.ToDateTime(now.AddMonths(1).Month.ToString() + "/1/" + now.AddMonths(1).Year.ToString()).AddDays(-1).ToString("MM/dd/yyyy");

        }

        public void LoadDT()
        {
            List<UserInfo> lo = m_UserInfoDao.GetAllLO();

            StringBuilder html = new StringBuilder();

            html.Append("<select id='LO'>");
            html.Append("<option value=''>-- Choose Loan Officer --</option>");
            lo.ForEach(x => html.AppendFormat("<option value='{0}'>{1}</option>",
                x.EmployId, x.FullName));
            html.Append("</select>");

            m_View.LODropDown = html.ToString();

            DateTime now = DateTime.Now;
            m_View.FundedFrom = Convert.ToDateTime(now.Month.ToString() + "/1/" + now.Year.ToString()).ToString("MM/dd/yyyy");
            m_View.FundedTo = Convert.ToDateTime(now.AddMonths(1).Month.ToString() + "/1/" + now.AddMonths(1).Year.ToString()).AddDays(-1).ToString("MM/dd/yyyy");

        }

    }
}
