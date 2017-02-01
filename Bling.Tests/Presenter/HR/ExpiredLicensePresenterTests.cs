using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Bling.Presenter.HR;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Tests.Presenter.HR
{
    [TestFixture]
    public sealed class ExpiredLicensePresenterTests
    {
        private MockFactory m_MockFactory;
        private Mock<IExpiredLicenseView> m_MockView;
        private Mock<IExpiredLicenseDao> m_MockDao;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
            m_MockView = m_MockFactory.Create<IExpiredLicenseView>();
            m_MockDao = m_MockFactory.Create<IExpiredLicenseDao>();
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test, Category("Database")]
        public void Should_be_able_to_send_email()
        {
            //Arrange
            List<ExpiredLicense> lists = new List<ExpiredLicense>();
            lists.Add(new ExpiredLicense { Branch = "Branch1", BranchEMail = "branch1@email.com", BranchNo = "001", EMail = "emp1@mail.com", EmployeeId = "001EM1", EmployeeName = "Employee One", ExpirationDate = new DateTime(2009, 7, 1), HireDate = new DateTime(2009, 1, 1) } );
            
            lists.Add(new ExpiredLicense { Branch = "Branch2", BranchEMail = "branch2@email.com", BranchNo = "002", EMail = "emp2@mail.com", EmployeeId = "002EM2", EmployeeName = "Employee Two", ExpirationDate = new DateTime(2009, 7, 1), HireDate = new DateTime(2009, 1, 1) });
            lists.Add(new ExpiredLicense { Branch = "Branch2", BranchEMail = "branch2@email.com", BranchNo = "002", EMail = "emp3@mail.com", EmployeeId = "002EM3", EmployeeName = "Employee Three", ExpirationDate = new DateTime(2009, 7, 1), HireDate = new DateTime(2009, 1, 1) });
                        
            m_MockView.SetupGet(x => x.DeadLine).Returns("12/31/2020");
            m_MockDao.Setup(x => x.GetAllEmployee()).Returns(lists);
            
            //Act
            ExpiredLicensePresenter presenter = new ExpiredLicensePresenter(m_MockView.Object, m_MockDao.Object);
            presenter.SendMail();

            //Assert
        }
    }
}
