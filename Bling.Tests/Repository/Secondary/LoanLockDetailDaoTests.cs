using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Secondary;
using Bling.Domain.Secondary;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class LoanLockDetailDaoTests
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
        public void Should_be_able_to_get_by_id()
        {
            ISession session =  StaticSessionManager.OpenSessionForDMDData();

            ILoanLockDetailDao dao = new LoanLockDetailDao(session);
            LoanLockDetail d = dao.GetById("AAO?N");
            Assert.That(d.Investor, Is.EqualTo("CW - GOLDEN EMPIRE"));
        }
    }
}
