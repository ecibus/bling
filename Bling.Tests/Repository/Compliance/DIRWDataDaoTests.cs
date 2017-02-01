using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using Bling.Presenter;
using Bling.Repository.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class DIRWDataDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetData_ShouldReturnAList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDIRWDataDao dao = new DIRWDataDao(session);

            //Act
            var list = dao.GetData("AAA[0");

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetLookUp_ShouldReturnAList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDIRWDataDao dao = new DIRWDataDao(session);

            //Act
            var list = dao.GetLookUp("zz");
            foreach (var i in list)
            {
                Console.WriteLine(i.Key);
            }
            
            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetFinal1003Id_ShouldReturnAListOfString()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDIRWDataDao dao = new DIRWDataDao(session);

            //Act
            var list = dao.GetFinal1003Id("AAB<W");
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));

        }

        [Test]
        public void GetDIRWFinal1003Data_ShouldReturnAList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDIRWDataDao dao = new DIRWDataDao(session);

            //Act
            var list = dao.GetFinal1003Data("AAB<W");
            foreach (var i in list)
            {
                Console.WriteLine(i.CurrentData + " - " + i.KeyId);
            }

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }
    }
}
