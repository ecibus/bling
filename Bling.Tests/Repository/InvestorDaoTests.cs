using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;
using Bling.Repository;
using Bling.Domain;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class InvestorDaoTests
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
        public void Should_be_able_to_get_investor_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IInvestorDao dao = new InvestorDao(session);
            Investor investor = dao.GetById("AA[");

            Assert.That(investor.Inv.ToLower(), Is.EqualTo("flagstar"));
        }
    }
}
