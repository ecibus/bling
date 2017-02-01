using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.Accounting;
using Bling.Presenter;
using NHibernate;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class RankingBranchTests
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
            RankingBranch branch = session.Get<RankingBranch>(1);

            Assert.That(branch.BranchId, Is.EqualTo("342"));
        }
    }
}
