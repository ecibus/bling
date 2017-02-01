using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Bling.Repository.Compliance;
using NHibernate;
using Bling.Presenter;
using NUnit.Framework.SyntaxHelpers;
using Bling.Domain.Compliance;
using Bling.Domain;

namespace Bling.Tests.Repository.Compliance
{
    [TestFixture, Category("Database")]
    public class DataIntegrityFieldDaoTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            NHibernateProfiler.Initialize();
        }

        [Test]
        public void GetAll_ShouldReturnAList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityFieldDao dao = new DataIntegrityFieldDao(session);

            //Act
            var list = dao.GetAll();

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }


        [Test]
        public void GetTransLog_NoLinkTable_ShouldGetData()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityFieldDao dao = new DataIntegrityFieldDao(session);

            //Act
            Translog translog = dao.GetTransLog("3", "AAD6O", "01BR", "NEWVALUE", "");

            //Assert
            Assert.That(translog.OldValue, Is.EqualTo("10/10/2005"));
        }

        [Test]
        public void GetTransLog_WithLinkTable_ShouldGetData()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IDataIntegrityFieldDao dao = new DataIntegrityFieldDao(session);

            //Act
            Translog translog = dao.GetTransLog("1", "AAD6O", "01BR", "NEWVALUE", "");

            //Assert
            Assert.That(translog.OldValue, Is.EqualTo("BRANCH"));
        }

        //[Test]
        //public void GetCurrentData_ShouldReturnAString()
        //{
        //    //Arrange
        //    ISession session = StaticSessionManager.OpenSessionForDMDData();
        //    IDataIntegrityFieldDao dao = new DataIntegrityFieldDao(session);
        //    DataIntegrityField field = new DataIntegrityField
        //    {
        //        TargetTable = "gen",
        //        TargetField = "branch_type",
        //        SourceTable = "DataTracFieldEnum",
        //        SourceField = "value",
        //        SourceId = "alias",
        //        SourceCriteria = "DataTracFieldId = '680'"
        //    };

        //    //Act
        //    string data = dao.GetCurrentData(field, "aaseo");

        //    //Assert
        //    Assert.That(data, Is.EqualTo("BRANCH"));
        //}

        //[Test]
        //public void GetNewDataHtml_ShouldReturnAHtml()
        //{
        //    //Arrange
        //    ISession session = StaticSessionManager.OpenSessionForDMDData();
        //    IDataIntegrityFieldDao dao = new DataIntegrityFieldDao(session);
        //    DataIntegrityField field = new DataIntegrityField
        //    {
        //        SourceTable = "DataTracFieldEnum",
        //        SourceField = "value",
        //        SourceId = "alias",
        //        SourceCriteria = "DataTracFieldId = '680'",
        //        DisplayAs = "DropDown"
        //    };

        //    //Act
        //    string data = dao.GetNewDataHtml(field, "aaseo");
        //    Console.WriteLine(data);
        //    //Assert
        //    Assert.That(data, Is.EqualTo("<select id='11'><option value=\"\">-- Please Select -- </option><option value=\"R\">BRANCH</option><option value=\"B\">BROKER</option><option value=\"K\">LENDER (B)</option><option value=\"L\">LENDER (P)</option></select>"));
        //}
    }
}
