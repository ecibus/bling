using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Bling.Repository.Secondary;
using NHibernate;
using Bling.Presenter;

namespace Bling.Tests.Repository.Secondary
{
    [TestFixture, Category("Database")]
    public class HedgeGMACDaoTests
    {
        private MockRepository m_mocks;
        private IHedgeGMACDao m_Dao;
        private ISession m_Session;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new HedgeGMACDao(m_Session);
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void GetList_ShouldReturnA_List()
        {
            //Assert.That(m_Dao.GetInvestor().Count, Is.GreaterThan(0));
            IList<string> list = m_Dao.GetList("1/1/2011", "12/31/2011");

            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
        }

    }
}
