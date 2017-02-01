using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;
using Bling.Presenter;
using Bling.Presenter.Compliance;

namespace Bling.Tests.Presenter.Compliance
{
    [TestFixture]
    public class AjaxDIRWPresenterTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Default);
        }

        [Test]
        public void Load_LoanNumberIsNotAvailable_ShouldReturnAnErrorMessage()
        {
            //Arrange
            var view = m_MockFactory.Create<IAjaxView>();
            view.SetupSet(x => x.ResponseText = "{ Message : 'Could not find Loan Number 123, please try again.' }");

            var dao = m_MockFactory.Create<IDIRWLoanInfoDao>();
            dao.Setup(x => x.GetLoanInfo(It.IsAny<string>()))
                .Returns((DIRWLoanInfo) null);
            
            //Act
            var presenter = new AjaxDIRWPresenter(view.Object, dao.Object, null, null, null, null, null);
            presenter.Load("123");

            //Assert
            view.VerifyAll();

        }

        [Test]
        public void Load_ThrowsAnException_ShouldReturnAnErrorMessage()
        {
            //Arrange
            var view = m_MockFactory.Create<IAjaxView>();
            view.SetupSet(x => x.ResponseText = "{ Message : 'Error' }");

            var dao = m_MockFactory.Create<IDIRWLoanInfoDao>();
            dao.Setup(x => x.GetLoanInfo(It.IsAny<string>()))
                .Throws(new Exception("Error"));

            //Act
            var presenter = new AjaxDIRWPresenter(view.Object, dao.Object, null, null, null, null, null);
            presenter.Load("123");

            //Assert
            view.VerifyAll();
        }

        [Test]
        public void Load_LoanIsAvailable_ReturnsAJsonObject()
        {
            //Arrange
            var view = m_MockFactory.Create<IAjaxView>();
            view.SetupSet(x => x.ResponseText = It.IsAny<string>());

            var dao = m_MockFactory.Create<IDIRWLoanInfoDao>();
            dao.Setup(x => x.GetLoanInfo(It.IsAny<string>()))
                .Returns(new DIRWLoanInfo());
                
            //Act
            var presenter = new AjaxDIRWPresenter(view.Object, dao.Object, null, null, null, null, null);
            presenter.Load("123123");

            //Assert
            view.VerifyAll();
        }
    }
}
