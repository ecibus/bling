using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class CommissionAnalysisDaoTests
    {
        [Test]
        public void GetLoan_ShouldReturn_AnObject()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ICommissionAnalysisDao dao = new CommissionAnalysisDao(session);

            var ca = dao.GetLoan("TEST0000600120");

            Assert.That(ca.Borrower, Is.EqualTo("STERNER, DONALD"));
        }

        [Test]
        public void Test()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ICommissionAnalysisDao dao = new CommissionAnalysisDao(session);

            var list = dao.GetAwaitingApproval();

            Console.WriteLine(list.Count);

            Console.WriteLine(list[0].LoanNumber);
            Console.WriteLine(list[0].ApprovedLO);
            Console.WriteLine(list[0].ApplicationDate);
            Console.WriteLine(list[0].FundedDate);
            Console.WriteLine(list[0].LoanAmount);
            Console.WriteLine(list[0].LoanOfficer);
        }

    }
}
