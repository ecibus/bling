using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class LPELoanInfoDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetReadyForDocs_ShouldReturn_AList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILPELoanInfoDao dao = new LPELoanInfoDao(session);

            //Act
            IList<LPELoanInfo> list = dao.GetReadyForDocs();

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetLoanInfo_ShouldReturn_AnObject()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILPELoanInfoDao dao = new LPELoanInfoDao(session);

            //Act
            LPELoanInfo loan = dao.GetLoanInfo("1050900106");

            //Assert
            Assert.That(loan.Borrower, Is.EqualTo("LANGFORD, NADINE"));
        }
    }
}
