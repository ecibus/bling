using System;
using System.Collections.Generic;
using Bling.Domain.Accounting;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.Accounting
{
    [TestFixture]
    public class DocTrustRunHistoryTests
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
        public void Should_be_able_to_convert_object_to_table_row()
        {
            DocTrustRunHistory hist = new DocTrustRunHistory { AsOf = "01/01/2009", CreatedBy = "ME", 
                CreatedOn = new DateTime(2009, 1, 1), TransferDate = "02/02/2009" };

            string expected = "<tr><td>1/1/2009 12:00:00 AM</td><td>02/02/2009</td><td>01/01/2009</td><td>Me</td></tr>";
            Assert.That(hist.ToRow(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_list_to_html_table()
        {

            DocTrustRunHistory hist1 = new DocTrustRunHistory
            {
                AsOf = "01/01/2009",
                CreatedBy = "ME",
                CreatedOn = new DateTime(2009, 1, 1),
                TransferDate = "02/02/2009"
            };
            DocTrustRunHistory hist2 = new DocTrustRunHistory
            {
                AsOf = "01/01/2009",
                CreatedBy = "ME",
                CreatedOn = new DateTime(2009, 1, 1),
                TransferDate = "02/02/2009"
            };

            List<DocTrustRunHistory> lists = new List<DocTrustRunHistory> { hist1, hist2 };

            string expected = 
                "<table>" + 
                    "<tr><td>Date</td><td>Transfer Date</td><td>As Of</td><td>Run By</td></tr>" +
                    "<tr><td>1/1/2009 12:00:00 AM</td><td>02/02/2009</td><td>01/01/2009</td><td>Me</td></tr>" + 
                    "<tr><td>1/1/2009 12:00:00 AM</td><td>02/02/2009</td><td>01/01/2009</td><td>Me</td></tr>" +
                "</table>";
            
            Assert.That(DocTrustRunHistory.ToHtmlTable(lists), Is.EqualTo(expected));

        }
    }
}
