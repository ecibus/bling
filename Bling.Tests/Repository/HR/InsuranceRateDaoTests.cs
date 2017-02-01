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
    public sealed class InsuranceRateDaoTests
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
            IInsuranceRateDao dao = new InsuranceRateDao(session);
            IList<InsuranceRate> list = dao.GetRatesForType("1");

            Assert.That(list.Count, Is.GreaterThan(1));
        }

        [Test]
        public void Should_be_able_to_add_new_rate()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceRateDao dao = new InsuranceRateDao(session);
            dao.AddNewRate("1", 777m);
        }

        [Test]
        public void Should_be_able_to_get_EEStatus()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IInsuranceRateDao dao = new InsuranceRateDao(session);

            IList<string> data = dao.GetEEStatus();

            Assert.That(data.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Test()
        {
            Console.WriteLine("122009".Substring(0, 2));
        }
    }
}
