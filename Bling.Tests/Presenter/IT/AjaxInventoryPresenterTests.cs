using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Domain.IT;
using Bling.Presenter;
using Bling.Presenter.IT;
using Bling.Repository.IT;
using Moq;
using NUnit.Framework;

namespace Bling.Tests.Presenter.IT
{    
    [TestFixture]
    public sealed class AjaxInventoryPresenterTests
    {
        private MockFactory m_MockFactory;

        private Mock<IInventoryDao> m_MockDao;
        private Mock<IAjaxView> m_MockView;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
            m_MockView = m_MockFactory.Create<IAjaxView>();
            m_MockDao = m_MockFactory.Create<IInventoryDao>();
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_add_entries_in_inventory()
        {
            //Arrange
            Inventory inventory = new Inventory
            {
                AddedOn = new DateTime(2009, 6, 1),
                AddedBy = "AAA",
                IssuedTo = "BBB",
                BranchName = "Corporate",
                IssuedOn = new DateTime(2009, 6, 1),
                Make = "Make",
                Model = "Model",
                Quantity = 1,
                SerialNumber = "AAAAAAA"
            };
            
            m_MockView.SetupSet(x => x.ResponseText = It.IsAny<string>());            
            m_MockDao.Setup(x => x.Add(inventory)).Returns(1);            

            AjaxInventoryPresenter presenter = new AjaxInventoryPresenter(m_MockView.Object, m_MockDao.Object, null);

            //Act
            presenter.Add(inventory);
            
            //Assert
        }

        [Test]
        public void Should_be_able_to_get_last_10_data_added()
        {
            //Arrange
            m_MockView.SetupSet(x => x.ResponseText = It.IsAny<string>());
            m_MockDao.Setup(x => x.GetAllInventory(1)).Returns(It.IsAny<IList<Inventory>>);
            
            AjaxInventoryPresenter presenter = new AjaxInventoryPresenter(m_MockView.Object, m_MockDao.Object, null);

            //Act
            presenter.GetAllInventoryWithPage(1);

            //Assert
        }
    }
}
