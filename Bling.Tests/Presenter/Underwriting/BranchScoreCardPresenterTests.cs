using System;
using NUnit.Framework;
using Moq;
using Bling.Presenter.Underwriting;
using NUnit.Framework.SyntaxHelpers;
using Bling.Repository;

namespace Bling.Tests.Presenter.Underwriting
{
    [TestFixture]
    public sealed class BranchScoreCardPresenterTests
    {
        private MockFactory m_MockFactory;
        private Mock<IBrokerDao> m_BrokerDao;
        
        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
            m_BrokerDao = m_MockFactory.Create<IBrokerDao>();
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        //[Test]
        //public void Should_be_able_to_create_dropdown_for_month()
        //{
        //    //Arrange
        //    string monthDropDown = "<select id=\"ddlMonth\"><option>January</option><option>February</option><option>March</option><option>April</option><option>May</option><option>June</option><option>July</option><option>August</option><option>September</option><option selected=\"selected\">October</option><option>November</option><option>December</option></select>";
        //    Mock<IBranchScoreCardView> view = m_MockFactory.Create<IBranchScoreCardView>(MockBehavior.Loose);
        //    
        //    view.SetupSet(x => x.MonthHtml = monthDropDown);
        //    BranchScoreCardPresenter presenter = new BranchScoreCardPresenter(view.Object, new DateTime(2009, 10, 1), m_BrokerDao.Object);

        //    //Act
        //    presenter.Load();

        //    //Assert            
        //}

        //[Test]
        //public void Should_be_able_to_create_dropdown_for_year()
        //{
        //    string yearDropDown = "<select id=\"ddlYear\"><option selected=\"selected\">2009</option><option>2008</option><option>2007</option><option>2006</option><option>2005</option><option>2004</option><option>2003</option><option>2002</option><option>2001</option><option>2000</option></select>";
        //    Mock<IBranchScoreCardView> view = m_MockFactory.Create<IBranchScoreCardView>(MockBehavior.Loose);
        //    view.SetupSet(x => x.YearHtml = yearDropDown);
        //    BranchScoreCardPresenter presenter = new BranchScoreCardPresenter(view.Object, new DateTime(2009, 10, 1), m_BrokerDao.Object);

        //    //Act
        //    presenter.Load();
        //}
    }
}
