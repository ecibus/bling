using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bling.Repository.Compliance;
using NHibernate;
using Bling.Presenter;
using NUnit.Framework.SyntaxHelpers;
using HibernatingRhinos.Profiler.Appender.NHibernate;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class DataIntegrityGroupDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetAllGroup_ShouldReturnAList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityGroupDao dao = new DataIntegrityGroupDao(session);

            //Act
            var list = dao.GetAllGroupFor("closedloan");

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetAllGroup_ShouldReturnChildFields()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityGroupDao dao = new DataIntegrityGroupDao(session);

            //Act
            var list = dao.GetAllGroupFor("closedloan");

            foreach (var i in list)
            {
                Console.WriteLine(i.GroupName);
                foreach (var f in i.Fields)
                {
                    Console.WriteLine(" - " + f.Description );
                }
            }

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetById_ShouldReturnAGroup()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityGroupDao dao = new DataIntegrityGroupDao(session);

            //Act
            var group = dao.GetById(7);

            Console.WriteLine(group.GroupName);
            foreach (var f in group.Fields)
            {
                Console.WriteLine(" - " + f.Description );
            }

            //Assert
            Assert.That(group.Fields.Count, Is.GreaterThan(0));

        }
    }
}
