using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bling.Domain;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class AddressTests
    {
        [Test]
        public void Add1_WhenCalled_ShouldReturnStreet()
        {
            //Arrange
            Address address = new Address { Street = "123 Main St." };
	
            //Act
            string add1 = address.Add1;

            //Assert
            Assert.That(add1, Is.EqualTo("123 Main St."));
        }

        [Test]
        public void Add2_WhenCalled_ShouldReturnCityStateAndZip()
        {
            //Arrange
            Address address = new Address { Street = "123 Main St.", City = "Bakersfield", State = "CA", Zip = "93309" };	

            //Act
            string add2 = address.Add2;

            //Assert
            Assert.That(add2, Is.EqualTo("Bakersfield, CA 93309"));
        }

        [Test]
        public void Add2_ZipHaveDashAtTheEnd_ShouldReturnWithoutTheDash()
        {
            //Arrange
            Address address = new Address { Street = "123 Main St.", City = "Bakersfield", State = "CA", Zip = "93309-" };

            //Act
            string add2 = address.Add2;

            //Assert
            Assert.That(add2, Is.EqualTo("Bakersfield, CA 93309"));
        }
    }
}
