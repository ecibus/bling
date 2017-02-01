using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.LOS
{
    public class AddUserInUnderwritingReportPresenter : AddUserInReportPresenter
    {
        public AddUserInUnderwritingReportPresenter(IAddUserInReportView view)
            : base (view, new ReportUserDao(DMDDataSession()), new UserInfoDao(DMDDataSession()))
        {            
        }

        public AddUserInUnderwritingReportPresenter(IAddUserInReportView view, IReportUserDao reportUserDao, IUserInfoDao userInfoDao)
            : base(view, reportUserDao, userInfoDao)
        {
        }

        public override ReportUser AddUser(string employId)
        {
            return m_ReportUserDao.AddUnderwriter(employId);
        }

        public override ReportUser RemoveUser(string employId)
        {
            return m_ReportUserDao.RemoveUserAsUnderwriter(employId);
        }

        public override List<UserInfo> GetAllAvailableUser()
        {
            return m_UserInfoDao.GetAllUnderwriter();
        }

        public override List<ReportUser> GetAllCurrentUser()
        {
            return m_ReportUserDao.GetAllUnderwriter();
        }
    }
}
