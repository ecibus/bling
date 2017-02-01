using System;
using Bling.Domain.HR;
using Bling.Presenter;
using Bling.Repository.HR;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class JobDaoTests
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
        public void Should_be_able_to_get_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IJobDao dao = new JobDao(session);
            Job job = dao.GetById(53);

            Assert.That(job.Title, Is.EqualTo("Programmer / Developer"));

        }
    }
}
