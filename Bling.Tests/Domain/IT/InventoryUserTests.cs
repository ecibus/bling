using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Bling.Domain.IT;

namespace Bling.Tests.Domain.IT
{
    [TestFixture]
    public sealed class InventoryUserTests
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
        public void Should_be_able_to_create_InventoryUser_object()
        {
            //Arrange
            InventoryUser user = new InventoryUser { FirstName = "First", LastName = "Last", Branch = "Branch" };
            	
            //Act

            //Assert
            Assert.That(user, Is.Not.Null);
            
        }
    }
}
