using System;
using Bling.Presenter;
using Bling.Repository;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Repository
{
    [TestFixture]
    public class AbstractDaoTests 
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
        public void Should_be_able_to_get_connection_string()
        {            
            TestDao dao = new TestDao(StaticSessionManager.OpenSessionForDMDData());            
            Assert.That(dao.DMDDataConnectionString, Is.Not.Empty);
        }

        class TestDao : AbstractDao<string, int>
        {
            public TestDao(ISession session)
                : base(session)
            {                
            }            
        }
    }
}
