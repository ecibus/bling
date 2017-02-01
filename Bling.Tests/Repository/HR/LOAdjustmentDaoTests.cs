using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class LOAdjustmentDaoTests
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
        public void GetAllByLOCode_ShouldReturn_AList()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            ILOAdjustmentDao dao = new LOAdjustmentDao(session);

            var list = dao.GetAllByLOCode("RALCARAZ");

            Console.WriteLine(list.Count);
            
        }
    }
}
