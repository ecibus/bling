using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository;
using Bling.Domain;
using System.Collections.Generic;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public sealed class CashDepositBranchDaoTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_read_all_data()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            ICashDepositBranchDao dao = new CashDepositBranchDao(session);
            IList<CashDepositBranch> list = dao.GetAll();

            Assert.That(list.Count, Is.GreaterThan(1));
        }
    }
}
