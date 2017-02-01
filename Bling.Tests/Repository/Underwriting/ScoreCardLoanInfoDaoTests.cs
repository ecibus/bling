using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Underwriting;
using Bling.Domain.Underwriting;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Underwriting
{
    [TestFixture, Category("Database")]
    public sealed class ScoreCardLoanInfoDaoTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_ScoreCardLoanInfo()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IScoreCardLoanInfoDao dao = new ScoreCardLoanInfoDao(session);

            ScoreCardLoanInfo loan = dao.GetByLoanNumber("1600700386");

            Assert.That(loan.LoanNumber, Is.EqualTo("1600700386"));
            Assert.That(loan.Is203K, Is.True);
        }
    }
}
