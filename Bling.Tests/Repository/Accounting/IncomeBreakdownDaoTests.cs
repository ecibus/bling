using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Accounting;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class IncomeBreakdownDaoTests
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
        public void Should_be_able_to_get_income_breakdown_by_application_number()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IIncomeBreakdownDao dao = new IncomeBreakdownDao(session);

            Assert.That(dao.GetByApplicationOrLoanNumber("1000010451"), Is.Not.Null);
            
                                      
        }
    }
}
