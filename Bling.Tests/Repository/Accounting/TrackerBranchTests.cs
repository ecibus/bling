using System;
using Bling.Domain.Accounting;
using Bling.Presenter;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class TrackerBranchTests
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
            TrackerBranch trackerBranch = session.Get<TrackerBranch>(1);
            Assert.That(trackerBranch.BranchId, Is.EqualTo("105"));
        }
    }
}
