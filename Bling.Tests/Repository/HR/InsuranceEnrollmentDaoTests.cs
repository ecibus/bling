using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using System.Collections.Generic;
using Bling.Domain.HR;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public sealed class InsuranceEnrollmentDaoTests
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
        public void Should_be_able_to_get_data()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceEnrollmentDao dao = new InsuranceEnrollmentDao(session);
            IList<InsuranceEnrollment> list = dao.GetByYearMonthAndBranch("200308", "000");

            Assert.That(list.Count, Is.GreaterThan(1));
        }

        [Test]
        public void Should_be_able_to_update_rate()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceEnrollmentDao dao = new InsuranceEnrollmentDao(session);
            dao.UpdateRate(13663, 2, 0m);
        }

        [Test]
        public void Should_be_able_to_enroll_employee()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceEnrollmentDao dao = new InsuranceEnrollmentDao(session);

            InsuranceEnrollment enroll = new InsuranceEnrollment { 
                EmployeeName = "Test", IsLO = true, BirthDate = new DateTime(2009, 1, 1), BranchNo = "000",
                Data = "EE", EmployeeCost = 100m, Ins1 = 1m, Ins2 = 2m, Ins3 = 3m, Ins4 = 4m, Ins5 = 5m,
                Ins6 = 6m, Ins7 = 7m, Ins9 = 9m, Ins10 = 10m, Location = "W", YearMonth = "200308" 
            };

            dao.Save(enroll);

        }
    }
}
