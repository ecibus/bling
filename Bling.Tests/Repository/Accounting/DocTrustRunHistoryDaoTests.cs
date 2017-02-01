using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Repository.Accounting;
using Bling.Domain.Accounting;
using Bling.Presenter;
using NHibernate;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class DocTrustRunHistoryDaoTests
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
        public void Should_be_able_to_add_data_in_database()
        {
            DocTrustRunHistory hist = new DocTrustRunHistory { TransferDate = "01/01/2009", AsOf = "02/02/2009", CreatedBy = "me" };
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();

            IDocTrustRunHistoryDao dao = new DocTrustRunHistoryDao(session);
            dao.Save(hist);

            Assert.That(hist.Id, Is.Not.EqualTo(0));
        }
    }
}
