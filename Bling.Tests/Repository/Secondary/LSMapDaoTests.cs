using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.Secondary;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class LSMapDaoTests
    {
        private MockRepository m_mocks;
        private ILSMapDao m_Dao;
        private ISession m_Session;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new LSMapDao(m_Session);
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_unique_investor()
        {            
            Assert.That(m_Dao.GetInvestor().Count, Is.GreaterThan(0));            
        }

        [Test]
        public void Should_be_able_to_get_unique_program_code()
        {            
            Assert.That(m_Dao.GetProgramCode().Count, Is.GreaterThan(0));
        }

        [Test]
        public void Should_be_able_to_get_by_program_code()
        {
            Assert.That(m_Dao.GetByProgramCode("co15").Count, Is.GreaterThan(0));
        }
    }
}
