using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Presenter;
using Bling.Repository;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class UserInfoDaoTests
    {
        private ISession m_Session;
        private IUserInfoDao m_Dao;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new UserInfoDao(m_Session);
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_userinfo_by_id()
        {
            Assert.That(m_Dao.GetById("B<4").FirstName.ToLower(), Is.EqualTo("house"));
        }

        [Test]
        public void Should_be_able_to_get_all_funder()
        {
            Assert.That (m_Dao.GetAllFunder().Count, Is.GreaterThan(0));            
        }

        [Test]
        public void Should_be_able_to_get_all_underwriter()
        {
            Assert.That(m_Dao.GetAllUnderwriter().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_login_name()
        {
            Assert.That(m_Dao.GetById("B<4").Actor.LoginName.ToLower(), Is.EqualTo("inhouse"));
        }

        [Test]
        public void Should_be_able_to_get_licensed_user()
        {
            Assert.That(m_Dao.GetLicensedUser().Count, Is.GreaterThan(0));            
        }

        [Test]
        public void Should_be_able_to_get_by_actor_id()
        {
            Assert.That(m_Dao.GetByActorId("01A=").FirstName.ToLower(), Is.EqualTo("house"));
        }

        [Test]
        public void Should_be_able_to_get_all_lo()
        {
            Assert.That(m_Dao.GetAllLO().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_branch()
        {
            UserInfo ui = m_Dao.GetById("BWT");
            Assert.That(ui.FirstName.ToLower(), Is.EqualTo("angela"));
            Console.WriteLine(ui.Broker.DBA);
            
        }
    }
}
