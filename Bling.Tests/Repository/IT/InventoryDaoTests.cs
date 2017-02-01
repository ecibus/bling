using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using Bling.Domain.IT;
using Bling.Presenter;
using Bling.Repository.IT;
using Moq;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Repository.IT
{
    [TestFixture, Category("Database")]
    public sealed class InventoryDaoTests
    {
        private MockFactory m_MockFactory;

        private ISession m_Session;
        private IInventoryDao m_Dao;
        private IInventoryFilterDao m_FilterDao;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
            m_Session = StaticSessionManager.OpenSessionForDMDData();
            m_Dao = new InventoryDao(m_Session);
            m_FilterDao = new InventoryFilterDao(m_Session);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_save_record_in_database()
        {
            //Arrange            
            Inventory newInventory = new Inventory
            {
                AddedOn = new DateTime(2009, 6, 1),
                AddedBy = "AAA",
                IssuedTo = "BBBBB",
                BranchName = "Corporate",
                IssuedOn = new DateTime(2009, 6, 1),
                Make = "Make",
                Model = "Model",
                Quantity = 1,
                SerialNumber = "AAAAAAA"
            };

            m_Session.BeginTransaction();

            int id = m_Dao.Add(newInventory);

            //Act
            Inventory inventory = m_Dao.GetById(id);

            //Assert
            Assert.That(inventory, Is.Not.Null);

            m_Session.Transaction.Rollback();
        }

        [Test]
        public void Should_be_able_to_get_last_10_added_inventory()
        {
            //Arrange
            	
            //Act
            List<Inventory> inventories = m_Dao.GetAllInventory(1).ToList();
            
            //Assert
            Assert.That(inventories.Count, Is.GreaterThan(0));
            inventories.ForEach(x => Console.WriteLine(x.Id));
        }

        [Test]
        public void Should_be_able_to_get_distinct_AssignTo_in_inventory()
        {
            //Arrange

            //Act
            List<LookUp> users = m_Dao.GetDistictAssignToInInventory().ToList();

            //Assert
            Assert.That(users.Count, Is.GreaterThan(0));
            users.ForEach(x => Console.WriteLine(x.Name));
        }

        [Test]
        public void Should_be_able_to_get_distinct_Branch_in_inventory()
        {
            //Arrange
            	
            //Act
            List<LookUp> branches = m_Dao.GetDistinctBranchInInventory().ToList();

            //Assert
            Assert.That(branches.Count, Is.GreaterThan(0));
            branches.ForEach(x => Console.WriteLine(x.Name));

        }

        [Test]
        public void Should_be_able_to_get_filtered_data()
        {
            //Arrange
            	
            //Act
            List<Inventory> inventories = m_FilterDao.GetFilteredData(1, "bav", "corporate").ToList();

            //Assert
            Assert.That(inventories.Count, Is.GreaterThan(0));
            inventories.ForEach(x => Console.WriteLine(x.Id));

        }

        [Test]
        public void Search_ShouldReturnAList()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            List<Inventory> inventories = m_Dao.Search(1, "kitty 0X706H").ToList();

            foreach (var i in inventories)
            {
                Console.WriteLine(i.Id + " " + i.Make);
            }
        }
    }
}
