using System;
using System.Collections.Generic;
using Bling.Domain.Secondary;
using Bling.Presenter;
using Bling.Repository.Secondary;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class ProgramCodeByInvestorDaoTests
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
        public void Should_be_able_to_get_count_by_investor()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IProgramCodeByInvestorDao dao = new ProgramCodeByInvestorDao(session);

            List<ProgramCodeByInvestor> list = dao.GetProgramCodeByInvestor(1);

            Assert.That(list.Count, Is.GreaterThan(0));            
        }
    }
}
