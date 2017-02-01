using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository;
using Bling.Domain;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public interface ILOBasisPointsView
    {
        string LODropDown { set; }
    }

    public class LOBasisPointsPresenter : Presenter
    {
        private ILOBasisPointsView m_View;
        private IUserInfoDao m_UserInfoDao;

        public LOBasisPointsPresenter(ILOBasisPointsView view)
            : this (view, new UserInfoDao(DMDDataSession()))
        {
        }

        public LOBasisPointsPresenter(ILOBasisPointsView view, IUserInfoDao dao)
        {
            m_View = view;
            m_UserInfoDao = dao;
        }

        public void Load()
        {
            List<UserInfo> lo = m_UserInfoDao.GetAllLO();
            //List<UserInfo> lo = m_UserInfoDao.GetActiveLO();
            BuildLODropDown(lo);
        }

        public void LoadByte()
        {
            List<ByteLO> lo = m_UserInfoDao.GetAllByteLO();
            BuildByteLODropDown(lo);
        }

        public void BuildByteLODropDown(List<ByteLO> lo)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='LO'>");
            html.Append("<option value=''>-- Choose Loan Officer --</option>");
            lo.ForEach(x => html.AppendFormat("<option value='{0}|{2}|{3}'>{1} ({3})</option>",
                x.UserName, x.FullName, x.BranchName, x.BranchId));
            html.Append("</select>");

            m_View.LODropDown = html.ToString();
        }

        public void BuildLODropDown(List<UserInfo> lo)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<select id='LO'>");
            html.Append("<option value=''>-- Choose Loan Officer --</option>");
            lo.ForEach(x => html.AppendFormat("<option value='{0}|{2}|{3}'>{1}</option>",
                x.EmployId, x.FullName, x.Broker == null ? "" : x.Broker.DBA, x.Broker == null ? "" : x.Broker.Id));
            html.Append("</select>");

            m_View.LODropDown = html.ToString();
        }

    }
}
