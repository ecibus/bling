using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter.LOS;
using Bling.Repository;
using Bling.Domain;

namespace Bling.Tests.Presenter.LOS
{
    [TestFixture]
    public class AddUserInFundingReportPresenterTests : IAddUserInReportView
    {
        private IAddUserInReportView m_View;
        private IReportUserDao m_ReportUserDao;
        private IUserInfoDao m_UserInfoDao;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_View = m_mocks.DynamicMock<IAddUserInReportView>();
            m_ReportUserDao = m_mocks.DynamicMock<IReportUserDao>();
            m_UserInfoDao = m_mocks.DynamicMock<IUserInfoDao>();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_set_current_user_and_available_user()
        {
            using (m_mocks.Record())
            {
            	List<ReportUser> reportUsers = new List<ReportUser>();
                List<UserInfo> allUser = new List<UserInfo>();

                Expect.Call(m_ReportUserDao.GetAllFunder())
                    .Repeat.Once()
                    .Return(reportUsers);
                
                Expect.Call(m_UserInfoDao.GetAllFunder())
                    .Repeat.Once()
                    .Return(allUser);

                m_View.AvailableUser = allUser;
                LastCall.Repeat.Once();
                m_View.CurrentReportUser = reportUsers;
                LastCall.Repeat.Once();
            }
            using (m_mocks.Playback())
            {
                AddUserInFundingReportPresenter presenter = new AddUserInFundingReportPresenter(m_View, m_ReportUserDao, m_UserInfoDao);
                presenter.Load();
            }
        }

        [Test]
        public void Available_user_should_contain_only_user_not_listed_in_current_user()
        {
            ReportUser ru1 = new ReportUser { EmployId = "AAA", IsFunder = true };
            
            UserInfo ui1 = new UserInfo { EmployId = "AAA", IsFunder = true };
            UserInfo ui2 = new UserInfo { EmployId = "BBB", IsFunder = true };

            List<ReportUser> reportUsers = new List<ReportUser> { ru1 };
            List<UserInfo> allUser = new List<UserInfo> { ui1, ui2 };

            List<UserInfo> modifiedUser = allUser;
            modifiedUser.Remove(ui1);

           
            using (m_mocks.Record())
            {
                Expect.Call(m_ReportUserDao.GetAllFunder())
                    .Repeat.Once()
                    .Return(reportUsers);

                Expect.Call(m_UserInfoDao.GetAllFunder())
                    .Repeat.Once()
                    .Return(allUser);
            }
            using (m_mocks.Playback())
            {
                AddUserInFundingReportPresenter presenter = new AddUserInFundingReportPresenter(this, m_ReportUserDao, m_UserInfoDao);
                presenter.Load();

                Assert.That(m_UserInfo.Count, Is.EqualTo(1));
            }

        }

        private List<ReportUser> m_ReportUser;
        private List<UserInfo> m_UserInfo;

        public List<ReportUser> CurrentReportUser
        {
            set { m_ReportUser = value; }
        }

        public List<UserInfo> AvailableUser
        {
            set { m_UserInfo = value; }
        }

    }
}
