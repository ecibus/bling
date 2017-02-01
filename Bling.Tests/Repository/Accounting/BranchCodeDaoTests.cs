using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Accounting;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Accounting
{
    [TestFixture, Category("Database")]
    public class BranchCodeDaoTests
    {
        [Test]
        public void Test()
        {
            ISession session = StaticSessionManager.OpenSessionForMWDataStore();
            IBranchCodeDao dao = new BranchCodeDao(session);
            Assert.That(dao.GetMarketingGainBranch().Count, Is.GreaterThan(0));
        }
    }
}
