using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Repository.Secondary;
using Bling.Presenter;
using Bling.Domain.Secondary;
using NHibernate;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class LoanSolutionProgramDaoTests
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
        public void Should_be_able_to_get_loan_solution_investor()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
           
            ILoanSolutionDao dao = new LoanSolutionDao(session);
            List<string> investor = dao.GetLSInvestor();

            Assert.That(investor.Count, Is.GreaterThan(0));

            investor.ForEach(x => Console.WriteLine(x));
        }

        [Test]
        public void Should_be_able_to_get_investor_mapping()
        {            
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILoanSolutionDao dao = new LoanSolutionDao(session);
            List<LSDTInvestorMapping> mapping = dao.GetInvestorMapping();

            Assert.That(mapping.Count, Is.GreaterThan(0));

            mapping.ForEach(x => Console.WriteLine("{0} = {1}", x.LoanSolutionInvestor, x.DataTracInvestor));
        }

        [Test]
        public void Should_be_able_to_get_investor_by_program_id()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILoanSolutionDao dao = new LoanSolutionDao(session);
            List<string> mapping = dao.GetLSInvestorByProgramId("A^H");

            Assert.That(mapping.Count, Is.GreaterThan(0));

            mapping.ForEach(x => Console.WriteLine("{0}", x));
        }

        [Test]
        public void Should_be_able_to_get_program_description_by_program_id_and_investor()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILoanSolutionDao dao = new LoanSolutionDao(session);
            List<string> mapping = dao.GetProgramDescriptionByInvestorAndProgramId("FRANKLIN AMERICAN", "AW<");

            Assert.That(mapping.Count, Is.GreaterThan(0));

            mapping.ForEach(x => Console.WriteLine("{0}", x));
        }
    }
}
