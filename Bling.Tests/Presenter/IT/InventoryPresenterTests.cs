using System;
using NUnit.Framework;
using Rhino.Mocks;
using Bling.Presenter.IT;
using Bling.Repository.IT;
using System.Collections;
using Bling.Domain.IT;
using System.Collections.Generic;
using Bling.Domain;

namespace Bling.Tests.Presenter.IT
{
    [TestFixture]
    public sealed class InventoryPresenterTests
    {
        private MockRepository m_mocks;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Should_be_able_to_get_available_user_combobox()
        {
            //Arrange
            IInventoryView view = m_mocks.DynamicMock<IInventoryView>();
            IInventoryUserDao inventoryUserDao = m_mocks.DynamicMock<IInventoryUserDao>();
            IInventoryDao inventoryDao = m_mocks.DynamicMock<IInventoryDao>();

            List<InventoryUser> users = new List<InventoryUser>();
            users.Add(new InventoryUser { EmployId = "1", Branch = "Branch1", FirstName = "FirstName1", LastName = "LastName1" });
            users.Add(new InventoryUser { EmployId = "2", Branch = "Branch2", FirstName = "FirstName2", LastName = "LastName2" });

            using (m_mocks.Record())
            {
                Expect.Call(inventoryUserDao.GetAllUser())
                    .Repeat.Once()
                    .Return(users);

                view.InventoryUserDropDown = "<select id='ddUser'><option value=\"|\">-- Please Select -- </option><option value=\"1|Branch1\">Firstname1 Lastname1 </option><option value=\"2|Branch2\">Firstname2 Lastname2 </option></select>";
                LastCall.Repeat.Once();
            }

            //Act
            using (m_mocks.Playback())
            {
                InventoryPresenter presenter = new InventoryPresenter(view, inventoryUserDao);
                presenter.Load();
            }
            //Assert
        }
    }
}
