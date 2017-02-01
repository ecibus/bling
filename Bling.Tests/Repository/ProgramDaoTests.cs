using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using NHibernate;
using Bling.Repository;
using Bling.Domain;
using Bling.Presenter;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class ProgramDaoTests
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
        public void Should_be_able_to_get_program_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IProgramDao dao = new ProgramDao(session);
            Program program = dao.GetById("A[:");
            Assert.That(program.ProgramName, Is.EqualTo("NE6ML"));

        }
    }
}
