using System;
using Bling.Domain.HR;
using Bling.Presenter;
using Bling.Repository.HR;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Bling.Repository.Funding;
using Bling.Domain.Funding;
using System.Collections.Generic;

namespace Bling.Tests.Repository.Funding
{
    [TestFixture, Category("Database")]
    public class FundingExceptionSummaryDaoTests
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
        public void Should_be_able_to_get_the_list()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IFundingExceptionSummaryDao dao = new FundingExceptionSummaryDao(session);

            IList<FundingExceptionSummary> list = dao.GetList(7, 2011);

            Assert.That(list.Count, Is.GreaterThan(0));
            Console.WriteLine(list.Count);
        }
    }
}
