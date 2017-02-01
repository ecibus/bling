using System;
using NUnit.Framework;
using Bling.Presenter;
using NHibernate;
using Bling.Repository.IT;
using Bling.Domain.IT;
using System.Collections.Generic;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.IT
{
    [TestFixture, Category("Database")]
    public sealed class InventoryUserDaoTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Should_be_able_to_get_list_of_InventoryUser()
        {
            //Arrange
            ISession session = StaticSessionManager.OpenSessionForDMDData();
            IInventoryUserDao dao = new InventoryUserDao(session);

            //Act
            IList<InventoryUser> list = dao.GetAllUser();

            //Assert
            Assert.That(list.Count, Is.GreaterThan(0));
            
        }
    }
}
