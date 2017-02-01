using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class GEMApplicationTests
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
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_be_able_to_convert_object_as_html_list_item()
        {
            GEMApplication app = new GEMApplication() { Id = 1, Image = "Image", ApplicationName = "Name", Link = "link" };
            Assert.That(app.GetApplicationAsListItem(), Is.EqualTo("<li id='App1' href='link'><img src='Image' alt='Name' /><br />Name</li>"));
        }

        [Test]
        public void Should_be_able_to_convert_object_as_html_link()
        {
            GEMApplication app = new GEMApplication() { Id = 1, Image = "Image", ApplicationName = "Name", Link = "link" };
            Assert.That(app.GetApplicationAsALink(), Is.EqualTo("<a href='link?a=1'>Name</a>"));
        }
    }
}
