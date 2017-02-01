using System;
using System.Collections.Generic;
using Bling.Domain.Accounting;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.Accounting
{
    [TestFixture]
    public class TrustAccountTests
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
        public void Should_be_able_to_convert_object_to_html_row()
        {
            TrustAccount trust = new TrustAccount { Id = 1, Category = "CrdRpt", Type = "OUT", Date = new DateTime(2008, 1, 1), Amount = 100.00 };
            string expected = "<tr id='doc1'><td>CrdRpt</td><td>OUT</td><td>01/01/2008</td><td>$ 100.00</td><td></td><td><img id='1' alt='Delete' src='/Images/Trash.gif' /></td></tr>";
            Assert.That(trust.ToRow(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_list_to_html_table()
        {
            TrustAccount trust1 = new TrustAccount { Id = 1, Category = "CrdRpt", Type = "OUT", Date = new DateTime(2008, 1, 1), Amount = 100.00 };
            TrustAccount trust2 = new TrustAccount { Id = 2, Category = "CrdRpt", Type = "OUT", Date = new DateTime(2008, 1, 1), Amount = -100.00 };
            List<TrustAccount> list = new List<TrustAccount>();
            list.Add(trust1);
            list.Add(trust2);

            string expected =
                "<table>" +
                    "<tr><td>Trust Category</td><td>Trust Type</td><td>Trust Date</td><td>Trust Amount</td><td>Trust Notes</td><td>&nbsp;</td></tr>" +
                    "<tr id='doc1'><td>CrdRpt</td><td>OUT</td><td>01/01/2008</td><td>$ 100.00</td><td></td><td><img id='1' alt='Delete' src='/Images/Trash.gif' /></td></tr>" +
                    "<tr id='doc2'><td>CrdRpt</td><td>OUT</td><td>01/01/2008</td><td>$ -100.00</td><td></td><td><img id='2' alt='Delete' src='/Images/Trash.gif' /></td></tr>" +
                    "<tr><td>Total</td><td></td><td></td><td id='total'>$ 0.00</td><td></td><td></td></tr>" +
                "</table>";

            Assert.That(TrustAccount.ConvertListToTable(list), Is.EqualTo(expected));
        }
    }
}
