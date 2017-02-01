using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.HR;
using Bling.Domain.HR;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.HR
{
    [TestFixture, Category("Database")]
    public class LOBasisPointsDaoTests
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
        public void Save_ShouldInsert_Object()
        {
            //ISession session = StaticSessionManager.OpenSessionForDMDData();

            //ILOBasisPointsDao dao = new LOBasisPointsDao(session);

            //BasisPoints bp = new BasisPoints
            //{
            //    CreatedBy = "AAA",
            //    BaseCommission = Convert.ToDecimal(1.25),
            //    EmployeeId = "BBB",
            //    EffectiveDate = DateTime.Now,
            //    InsideSalesRep = true,
            //    Maximum = 5000,
            //    Minimum = 1000,
            //    Tier1 = Convert.ToDecimal(.10),
            //    Tier2 = Convert.ToDecimal(.20),
            //    Tier3 = Convert.ToDecimal(.30),
            //    Tier4 = Convert.ToDecimal(.40)
            //};

            //dao.Save(bp);

            //Assert.That(bp.Id, Is.Not.EqualTo(0));
        }
    }
}
