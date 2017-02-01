using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class CashDepositDaoTests
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

        public void ShouldBeAbleToGetAllData()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();

            CashDepositDao dao = new CashDepositDao(session);
            IList<CashDeposit> list = dao.GetByInputDate("3/31/2011");


            Assert.That(list.Count, Is.GreaterThan(0));
        }
    }
}
