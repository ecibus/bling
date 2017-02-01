using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.Extension;

namespace Bling.Tests.Domain.Extension
{
    [TestFixture]
    public class DoubleExtensionTests
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
        public void Should_be_able_to_convert_to_string()
        {
            double d = 1.0;
            Assert.That(d.ToCurrency(), Is.EqualTo("$ 1.00"));
        }

        [Test]
        public void Should_be_able_to_display_with_comma()
        {
            Assert.That((1234.0).ToCurrency(), Is.EqualTo("$ 1,234.00"));
        }

        [Test]
        public void Negative_should_be_displayed_with_parentheses()
        {
            Assert.That((-1234.0).ToCurrency(), Is.EqualTo("($ 1,234.00)"));
        }
    }
}
