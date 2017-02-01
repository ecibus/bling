using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.LOS
{
    public interface IAddUserInReportPresenter
    {
        void Load();
        ReportUser Add(string employId);
        ReportUser Remove(string employId);
    }
    public abstract class AddUserInReportPresenter : Presenter, IAddUserInReportPresenter
    {
        private IAddUserInReportView m_View;
        protected IUserInfoDao m_UserInfoDao;
        protected IReportUserDao m_ReportUserDao;

        public AddUserInReportPresenter(IAddUserInReportView view, IReportUserDao reportUserDao, IUserInfoDao userInfoDao)
        {
            m_View = view;
            m_ReportUserDao = reportUserDao;
            m_UserInfoDao = userInfoDao;
        }

        public void Load()
        {
            List<ReportUser> currentUser = GetAllCurrentUser();
            List<UserInfo> allUser = GetAllAvailableUser();

            currentUser.ForEach(x => allUser.Remove(allUser.Find(user => user.EmployId == x.EmployId)));

            m_View.CurrentReportUser = currentUser;
            m_View.AvailableUser = allUser;
        }

        public ReportUser Add(string employId)
        {
            ReportUser ru = AddUser(employId);
            Load();
            return ru;
        }

        public ReportUser Remove(string employId)
        {
            ReportUser ru = RemoveUser(employId);
            Load();
            return ru;
        }

        public abstract ReportUser AddUser(string employId);
        public abstract ReportUser RemoveUser(string employId);
        public abstract List<ReportUser> GetAllCurrentUser();
        public abstract List<UserInfo> GetAllAvailableUser();

    }
}
