using System;
using Bling.Presenter;
using Bling.Repository;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using System.Collections.Generic;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class BrokerDaoTests
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
        public void Should_be_able_to_get_approved_broker()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IBrokerDao dao = new BrokerDao(session);

            Assert.That(dao.GetActiveBroker().Count, Is.GreaterThan(0));

        }

        [Test]
        public void GetBranchManagerEmail_ShouldReturnA_List()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IBrokerDao dao = new BrokerDao(session);

            List<string> email = dao.GetBranchManagerEmailForCommission("661");
            email.ForEach(x => Console.WriteLine(x));

        }
    }
}
