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
    public class DateTimeExtensionTests
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
        public void Should_be_able_to_convert_datetime_to_short_string_if_variable_has_value()
        {
            DateTime? dt = new DateTime(2009, 2, 24);
            Assert.That(dt.ToDate(), Is.EqualTo("02/24/2009"));
        }

        [Test]
        public void Should_return_empty_string_when_date_is_null()
        {
            DateTime? dt = null;
            Assert.That(dt.ToDate(), Is.Empty);
        }
    }
}
