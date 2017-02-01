using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;
using Bling.Repository;
using Bling.Domain;

namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class GEMApplicationDaoTests
    {
        private ISession m_Session;
        private IGEMApplicationDao m_Dao;
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new GEMApplicationDao(m_Session);
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_get_application_by_link()
        {
            Assert.That(m_Dao.GetApplicationByLink("/main.aspx"), Is.Not.Null);

        }

        [Test]
        public void Should_be_able_to_get_application_by_id()
        {
            GEMApplication app = m_Dao.GetApplicationById(1);
            //int count = app.Groups.Count;
        }
    }
}
