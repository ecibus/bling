using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Presenter;
using Bling.Repository;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;


namespace Bling.Tests.Repository
{
    [TestFixture, Category("Database")]
    public class AppraiserDaoTests
    {
        [Test]
        public void GetAppraiserForBranch_ShouldReturn_AList()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
	        
            //Act
            IAppraiserDao dao = new AppraiserDao(session);
            IList<Appraiser> list = dao.GetAppraiserForBranchAndCounty("312", "conv");

            foreach (Appraiser a in list)
            {
                Console.WriteLine(a.FirstName + " " + a.LastName);
            }
            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetById_WhenCalled_ShouldReturnAnAddress()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();

            //Act
            IAppraiserDao dao = new AppraiserDao(session);
            Appraiser appraiser = dao.GetById("06V");            	

            //Assert
            Assert.That(appraiser.Address.Street, Is.EqualTo("1200 DISCOVERY DRIVE / 3RD FLOOR"));
        }

        [Test]
        public void GetById_WhenCalled_ShouldBeAbleToRetrievePhone()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();

            //Act
            IAppraiserDao dao = new AppraiserDao(session);
            Appraiser appraiser = dao.GetById("06V");

            //Assert
            Assert.That(appraiser.Phone.Line, Is.EqualTo("328-1600"));
        }

        [Test]
        public void GetById_WhenCalled_ShouldBeAbleToRetrieveFax()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();

            //Act
            IAppraiserDao dao = new AppraiserDao(session);
            Appraiser appraiser = dao.GetById("06V");

            //Assert
            Assert.That(appraiser.Fax.Line, Is.EqualTo("283-9777"));
        }

        [Test]
        public void GetCountyForLoan_WhenLoanHasCounty_ShouldReturnCounty()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();

            //Act
            IAppraiserDao dao = new AppraiserDao(session);
            string county = dao.GetCountyForLoan("testappraiser");

            //Assert
            Assert.That(county, Is.EqualTo("LOS ANGELES"));
        }
    }
}
