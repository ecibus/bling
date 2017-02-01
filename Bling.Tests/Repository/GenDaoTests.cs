using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using NHibernate;
using Bling.Domain;
using Bling.Repository;
using Bling.Presenter;
namespace Bling.Tests.Repository
{

    [TestFixture, Category("Database")]
    public class GenDaoTests
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
        public void Should_be_able_to_get_data_by_loan_number()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IGenDao dao = new GenDao(session);
            Gen gen = dao.GetByLoanNumber("1110500140");
            Assert.That(gen.FileId, Is.EqualTo("AAA[:"));
            Assert.That(gen.LoanAmount, Is.EqualTo(66000));
        }

        [Test]
        public void Should_be_able_to_get_program()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IGenDao dao = new GenDao(session);
            Gen gen = dao.GetByLoanNumber("1110500140");
            Assert.That(gen.Program.ProgramName, Is.EqualTo("SPSD30/15"));
        }

        [Test]
        public void Should_be_able_to_get_stage()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IGenDao dao = new GenDao(session);
            Gen gen = dao.GetByLoanNumber("1110500140");
            Assert.That(gen.Stage.Trim(), Is.EqualTo("PURCHASED"));
        }

        [Test]
        public void Should_be_able_to_get_loan_officer()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IGenDao dao = new GenDao(session);
            Gen gen = dao.GetByLoanNumber("1110500140");
            Assert.That(gen.LoanOfficer.FirstName, Is.EqualTo("IRMA"));
        }

        [Test]
        public void Should_be_able_to_get_loan_solution_investor()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IGenDao dao = new GenDao(session);
            Gen gen = dao.GetByLoanNumber("TEST1060500584");
            Assert.That(gen.GEMLock.Investor, Is.EqualTo("ALS - GOLDEN EMPIRE"));
        }
    }
    
}
