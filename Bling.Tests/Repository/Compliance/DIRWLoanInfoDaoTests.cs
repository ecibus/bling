using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Bling.Repository.Compliance;
using Bling.Presenter;
using NHibernate;
using Bling.Domain.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class DIRWLoanInfoDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetLoanInfo_ShouldReturnAnObject()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDIRWLoanInfoDao dao = new DIRWLoanInfoDao(session);

            //Act
            DIRWLoanInfo loan = dao.GetLoanInfo("1051000076");

            //Assert
            Assert.That(loan.FileId, Is.EqualTo("AASGQ"));
        }
    }
}
