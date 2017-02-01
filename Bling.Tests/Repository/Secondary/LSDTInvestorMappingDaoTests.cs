using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Repository.Secondary;
using Bling.Presenter;
using NHibernate;
using Bling.Domain.Secondary;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class LSDTInvestorMappingDaoTests
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
        public void Should_be_able_to_get_lsdtinvestormapping()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            
            ILSDTInvestorMappingDao dao = new LSDTInvestorMappingDao(session);
            List<LSDTInvestorMapping> investor = dao.GetAll().ToList();

            Assert.That(investor.Count, Is.GreaterThan(0));

            investor.ForEach(x => Console.WriteLine("'{0}' = '{1}'", x.LoanSolutionInvestor, x.DataTracInvestor));
        }
    }
}
