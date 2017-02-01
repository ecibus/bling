using System;
using System.Linq;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Underwriting;
using Bling.Domain.Underwriting;
using System.Collections.Generic;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Underwriting
{
    [TestFixture, Category("Database")]
    public sealed class ScoreCardGroupDaoTests
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
        public void Should_be_able_to_get_ScoreCardGroup()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IScoreCardGroupDao dao = new ScoreCardGroupDao(session);

            IList<ScoreCardGroup> list = dao.GetAll();

            foreach (var sc in list)
            {
                Console.WriteLine(sc.GroupName);
                foreach (var d in sc.Description)
                {
                    Console.WriteLine(" - " + d.Name);
                }
            }

            
            Assert.That(list.Count, Is.GreaterThan(0)); 
        }
    }
}
