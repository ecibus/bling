using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Domain.IT;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Domain.IT
{
    [TestFixture]
    public sealed class InventoryTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_create_Inventory_object()
        {
            //Arrange
            Inventory inventory = new Inventory
            {
                Make = "Make", Model = "Mode", SerialNumber = "Serial Number",
                Quantity = 1,
                IssuedTo = "AAA",
                BranchName = "Branch",
                AddedBy = "AAA",
                IssuedOn = DateTime.Now,
                AddedOn = DateTime.Now
            };

            //Act            

            //Assert
            Assert.That(inventory, Is.Not.Null);            
        }

        [Test]
        public void Should_be_able_to_generate_list_of_inventory()
        {
            //Arrange
            Inventory inventory = new Inventory
            {
                Make = "Make",
                Model = "Mode",
                SerialNumber = "Serial Number",
                Quantity = 1,
                IssuedTo = "AAA",
                BranchName = "Branch",
                AddedBy = "AAA",
                IssuedOn = DateTime.Now,
                AddedOn = DateTime.Now
            };

            List<Inventory> inventories = new List<Inventory>();
            inventories.Add(inventory);

            //Act
            string table = Inventory.ToHTMLTable(inventories, 1, 10);

            //Assert
            Assert.That(table, Is.Not.Empty);
        }
    }
}
