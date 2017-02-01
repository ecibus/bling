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
    public class ActorDaoTests
    {
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_actor_by_login_name()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IActorDao dao = new ActorDao(session);
            Actor actor = dao.GetByLoginName("inhouse");
            Assert.That(actor.ActorId, Is.EqualTo("01A="));
        }
    }
}
