using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.HR;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class LOMasterDaoTests
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

        public void GetAll_ShouldReturn_All()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();

            ILOMasterDao dao = new LOMasterDao(session);
            var list = dao.GetAll().Where(x => x.Name != null)
                .OrderBy(x => x.Name);
            Console.WriteLine(list.Count());
 
        }
    }
}
