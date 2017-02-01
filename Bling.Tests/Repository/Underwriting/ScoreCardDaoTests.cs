using System;
using Bling.Domain.Underwriting;
using Bling.Presenter;
using Bling.Repository.Underwriting;
using Moq;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Underwriting
{
    [TestFixture, Category("Database")]
    public sealed class ScoreCardDaoTests
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
        public void Should_be_able_to_add_ScoreCard()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IScoreCardDao dao = new ScoreCardDao(session);

            ScoreCard scoreCard = new ScoreCard { FileId = "AAA", Score = 0.5, ScoreId = 1 };

            dao.SaveScore(scoreCard);

            Assert.That(scoreCard.Id, Is.Not.EqualTo(0));
        }

       
    }
}
