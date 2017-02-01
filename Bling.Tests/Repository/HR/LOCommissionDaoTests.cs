using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class LOCommissionDaoTests
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
        public void GetByPaymentDate_ShouldReturn_AList()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();

            LOCommissionDao dao = new LOCommissionDao(session);

            IList<LOCommission> list = dao.GetLOCommission("3/25/2011", "3/11/2011", "1");

            foreach (var l in list)
            {
                Console.WriteLine("{0} {1}", l.LoanNumber, l.Volume);
            }
        }
    }
}
