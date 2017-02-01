using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Repository.Accounting;
using Bling.Presenter;
using NHibernate;
using Bling.Domain.Accounting;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture]
    [Category("Database")]
    public class BranchDaoTests
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
        public void Should_be_able_to_get_all_active_branch()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IBranchDao<TrackerBranch> dao = new BranchDao<TrackerBranch>(session);
            Assert.That(dao.GetActiveBranch().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_tracker_branch()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IBranchDao<TrackerBranch> dao = new BranchDao<TrackerBranch>(session);
            Assert.That(dao.GetTBranch().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_ranking_branch()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IBranchDao<RankingBranch> dao = new BranchDao<RankingBranch>(session);
            Assert.That(dao.GetTBranch().Count, Is.GreaterThan(0));
        }
    }
}
