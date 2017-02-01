using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Domain;
using Bling.Repository;
namespace Bling.Tests
{
    [TestFixture, Category("Database")]
    public class LUPopupDaoTests
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
        public void Should_be_able_to_get_by_id()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILUPopupDao dao = new LUPopupDao(session);
            LUPopup lu = dao.GetById(245);
            Assert.That(lu.Description.Trim(), Is.EqualTo("MARKETING"));
        }
    }
}
