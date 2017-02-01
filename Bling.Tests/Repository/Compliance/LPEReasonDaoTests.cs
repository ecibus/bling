using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class LPEReasonDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetAll_ShouldReturn_AList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            ILPEReasonDao dao = new LPEReasonDao(session);

            //Act
            IList<LPEReason> list = dao.GetAll();

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }
    }
}
