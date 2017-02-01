using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Domain.Accounting;
using Bling.Repository.Accounting;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class TrustAccountDaoTests
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
        public void Should_be_able_to_get_object_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();

            TrustAccount log = new TrustAccountDao(session).GetById(75710);

            Assert.That(log.ApplicationNumber.Trim(), Is.EqualTo("10552805"));
            Assert.That(log.Amount, Is.EqualTo(450));
        }

        [Test]
        public void Should_be_able_to_get_list_by_app_number()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            ITrustAccountDao dao = new TrustAccountDao(session);
            var list = dao.GetByApplicationNumber("13755393");
            Assert.That (list.Count, Is.GreaterThan(0));
            
        }
    }
}
