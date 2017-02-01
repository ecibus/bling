using System;
using System.Collections.Generic;
using Bling.Domain;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class BrokerTests
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
        public void Should_be_able_to_convert_list_to_html_listbox()
        {
            Broker b1 = new Broker { CostCenter = "1", DBA = "Branch1", IDNum = "1" };
            Broker b2 = new Broker { CostCenter = "2", DBA = "Branch2", IDNum = "2" };

            List<Broker> brokers = new List<Broker> { b1, b2 };

            string expected = "<select id='ActiveBranch' name='ActiveBranch' size='10'>" +
                "<option value='1'>1 - Branch1</option>" +
                "<option value='2'>2 - Branch2</option>" +
                "</select>";

            Assert.That(Broker.ToHtmlOptionList("ActiveBranch", brokers), Is.EqualTo(expected));

        }
    }
}
