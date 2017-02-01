using System;
using System.Collections.Generic;
using Bling.Domain.HR;
using Bling.Presenter;
using Bling.Repository.HR;
using Moq;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public sealed class InsuranceTitleDaoTests
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
        public void Should_be_able_to_retrieve_data()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceTitleDao dao = new InsuranceTitleDao(session);
            IList<InsuranceTitle> list = dao.GetAllCorp();

            Assert.That(list.Count, Is.GreaterThan(1));
        }

        [Test]
        public void Should_be_able_to_update_title()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceTitleDao dao = new InsuranceTitleDao(session);
            dao.UpdateTitle("200308", "hr_ins9_title", "New Nine");            
        }

        [Test]
        public void Should_be_able_to_get_by_yearmonth()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceTitleDao dao = new InsuranceTitleDao(session);
            InsuranceTitle it = dao.GetByYearMonth("200912");

            Assert.That(it.Title7, Is.EqualTo("Blue Cross Dental"));

        }
    }
}
