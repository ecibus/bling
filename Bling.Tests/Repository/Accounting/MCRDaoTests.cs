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
    public class MCRDaoTests
    {
        [Test]
        public void Test()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IMCRDao dao = new MCRDao(session);

            var e = dao.GetLastMCREnding("2011", "1");

            Assert.That(e.Amount, Is.EqualTo(149544437));
        }

        [Test]
        public void ShouldBeAbleToGetRMLAData()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IMCRDao dao = new MCRDao(session);

            var d = dao.GetSectionOne("2011", "1", "az");

            Assert.That(d.Count, Is.GreaterThan(0));

            foreach (var pair in d)
            {
                Console.WriteLine("{0}, {1}",
                pair.Key,
                pair.Value);
            }
        }

        [Test]
        public void ShouldbeAbleToGeListOfLO()
        {
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IMCRDao dao = new MCRDao(session);

            var d = dao.GetLOItem("2011", "1", "az");

            Assert.That(d.Count, Is.GreaterThan(0));
            
            foreach (var pair in d)
            {
                Console.WriteLine("{0}, {1}",
                pair.NMLSId,
                pair.NoOfLoans);
            }
        }
    }
}
