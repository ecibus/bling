using System;
using Bling.Domain.Accounting;
using Bling.Presenter;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture]
    [Category("Database")]
    public class BranchTests
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
        public void Should_be_able_to_retrieve_branch_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            Branch branch = session.Get<Branch>(5);
            Assert.That(branch.BranchName, Is.EqualTo("Production"));
        }

        [Test]
        public void TestIdentityInTheSameSession()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            Branch branch1 = session.Get<Branch>(5);
            Branch branch2 = session.Get<Branch>(5);

            Assert.That(branch1, Is.EqualTo(branch2));

        }

        [Test]
        public void TestIdentityInDifferentSession()
        {
            ISession s1 = StaticSessionManager.OpenSessionForMWDataStore();
            ISession s2 = StaticSessionManager.OpenSessionForMWDataStore();

            Branch branch1 = s1.Get<Branch>(5);
            Branch branch2 = s2.Get<Branch>(5);

            Assert.That(branch1, Is.Not.EqualTo(branch2));
        }
    }
}
