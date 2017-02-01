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
    public sealed class ScoreCardCommentDaoTests
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
        public void Should_be_able_to_add_comment()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IScoreCardCommentDao dao = new ScoreCardCommentDao(session);

            ScoreCardComment comment = new ScoreCardComment { FileId = "AAA", CreatedBy = "you", Comment = "comment...", GroupId = 1};

            dao.SaveComment(comment);

            //Assert.That(comment.Id, Is.Not.EqualTo(0));
        }
    }
}
