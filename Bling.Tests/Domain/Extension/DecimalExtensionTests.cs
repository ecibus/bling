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
    public class DecimalExtensionTests
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
        public void Should_be_abl_to_convert_number_to_string_with_4_decimal()
        {
            decimal? d = 1.0m;
            Assert.That(d.To4d(), Is.EqualTo("1.0000"));
        }

        [Test]
        public void Null_number_should_return_empty_string()
        {
            decimal? d = null;
            Assert.That(d.To4d(), Is.Empty);
        }

        [Test]
        public void Should_be_able_to_convert_to_string()
        {
            decimal ? d = 1.0m;
            Assert.That(d.ToCurrency(), Is.EqualTo("$ 1.00"));
        }

        [Test]
        public void Should_be_able_to_display_with_comma()
        {
            decimal? d = 1234.0m;
            Assert.That((d).ToCurrency(), Is.EqualTo("$ 1,234.00"));
        }

        [Test]
        public void Negative_should_be_displayed_with_parentheses()
        {
            decimal? d = -1234.0m;
            Assert.That((d).ToCurrency(), Is.EqualTo("($ 1,234.00)"));
        }
    }
}
