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
    public class FloatExtensionTests
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
            float ? d = 1.0f;
            Assert.That(d.To4d(), Is.EqualTo("1.0000"));
        }

        [Test]
        public void Null_number_should_return_empty_string()
        {
            float ? d = null;
            Assert.That(d.To4d(), Is.Empty);
        }
    }
}
