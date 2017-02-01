using System;
using Bling.Domain;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;


namespace Bling.Tests.Domain
{
    [TestFixture]
    public sealed class PaginatorTests
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
        public void When_page_number_is_1_and_total_page_is_greater_than_1_previous_link_should_be_disabled()
        {
            //Arrange            

            //Act
            Paginator page = new Paginator(1, 20);

            //Assert
            Assert.That(page.ToString(), Is.EqualTo("Previous <a href='javascript:Page(2)'>Next</a>"));
        }

        [Test]
        public void When_page_number_is_1_and_no_of_item_is_less_than_11_then_do_not_display_page()
        {
            //Arrange
            	
            //Act
            Paginator page = new Paginator(1, 10);

            //Assert
            Assert.That(page.ToString(), Is.Empty);
        }

        [Test]
        public void When_page_is_2_previous_should_have_a_link()
        {
            //Arrange
            	
            //Act
            Paginator page = new Paginator(2, 11);

            //Assert
            Assert.That(page.ToString(), Is.EqualTo("<a href='javascript:Page(1)'>Previous</a> Next"));
        }

        [Test]
        public void When_current_page_is_2_and_total_page_is_3_previous_and_next_should_have_a_link()
        {
            //Arrange
            	
            //Act
            Paginator page = new Paginator(2, 30);

            //Assert
            Assert.That(page.ToString(), Is.EqualTo("<a href='javascript:Page(1)'>Previous</a> <a href='javascript:Page(3)'>Next</a>"));
        }

    }
}
