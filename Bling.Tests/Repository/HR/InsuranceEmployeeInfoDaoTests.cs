using System;
using NUnit.Framework;
using Moq;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using Bling.Domain.HR;
using System.Collections.Generic;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public sealed class InsuranceEmployeeInfoDaoTests
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
        public void Should_be_able_to_get_employee_info_by_branch()
        {
            ISession session = StaticSessionManager.OpenSessionForGEMApp();
            IInsuranceEmployeeInfoDao dao = new InsuranceEmployeeInfoDao(session);
            IList<InsuranceEmployeeInfo> list = dao.GetEmployeeByBranch("000");

            Assert.That(list.Count, Is.GreaterThan(1));
        }
    }
}
