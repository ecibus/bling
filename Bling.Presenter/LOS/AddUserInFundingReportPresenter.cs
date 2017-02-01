using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Repository;

namespace Bling.Presenter.LOS
{
    public class AddUserInFundingReportPresenter : AddUserInReportPresenter
    {
        public AddUserInFundingReportPresenter(IAddUserInReportView view)
            : base(view, new ReportUserDao(DMDDataSession()), new UserInfoDao(DMDDataSession()))
        {            
        }

        public AddUserInFundingReportPresenter(IAddUserInReportView view, IReportUserDao reportUserDao, IUserInfoDao userInfoDao)
            : base(view, reportUserDao, userInfoDao)
        {
        }

        public override ReportUser AddUser(string employId)
        {
            return m_ReportUserDao.AddFunder(employId);
        }

        public override ReportUser RemoveUser(string employId)
        {
            return m_ReportUserDao.RemoveUserAsFunder(employId);
        }

        public override List<UserInfo> GetAllAvailableUser()
        {
            return m_UserInfoDao.GetAllFunder();
        }

        public override List<ReportUser> GetAllCurrentUser()
        {
            return m_ReportUserDao.GetAllFunder();
        }
    }
}
