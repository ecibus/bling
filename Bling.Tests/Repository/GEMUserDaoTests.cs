using System;
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
    public class GEMUserDaoTests
    {
        private IGEMUserDao m_Dao;
        private ISession m_Session;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new GEMUserDao(m_Session);
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_current_user()
        {
            GEMUser user = m_Dao.GetCurrentUser();
            Assert.That(user, Is.Not.Null);
        }

        [Test]
        public void Should_be_able_to_get_all_user()
        {
            Assert.That(m_Dao.GetAllUser().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_user_by_login_name()
        {
            Assert.That(m_Dao.GetUserByLoginName("pgonzales").ActorId.ToLower(), Is.EqualTo("008w"));
        }

        [Test]
        public void Should_be_able_to_get_group()
        {
            GEMUser user = m_Dao.GetUserByLoginName("mfernandez");

            foreach (GEMGroup group in user.Groups)
            {
                Console.WriteLine(group.Id);
            }
        }
   
    }
}
