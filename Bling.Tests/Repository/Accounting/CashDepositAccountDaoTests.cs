using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Bling.Domain.Accounting;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.Accounting;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class CashDepositAccountDaoTests
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

            CashDepositAccountDao dao = new CashDepositAccountDao(session);
            IList<CashDepositAccount> list = dao.GetAll().OrderBy(x => x.AccountNo).ToList();

            foreach (var l in list)
            {
                Console.WriteLine("{0} {1}", l.AccountNo, l.AccountDescription);
            }

            Assert.That(list.Count, Is.GreaterThan(0));
        }
    }
}
