using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.LOS;

namespace Bling.Tests.Domain.LOS
{
    [TestFixture]
    public class AvailableYearTests
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
        public void Available_year_should_be_able_to_contain_2_items()
        {
            List<string> availableYear = new AvailableYear(new DateTime(2008, 1, 1)).GetListOfAvailableYear();
            Assert.That(availableYear.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_be_able_to_contain_correct_year()
        {
            List<string> availableYear = new AvailableYear(new DateTime(2008, 1, 1)).GetListOfAvailableYear();
            Assert.That(availableYear.Contains("2007"));
            Assert.That(availableYear.Contains("2008"));
        }
    }
}
