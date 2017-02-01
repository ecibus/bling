using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain.Secondary;

namespace Bling.Tests.Domain.Secondary
{
    [TestFixture]
    public class LSDTInvestorMappingTests
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
        public void Should_be_able_to_return_html_row()
        {
            LSDTInvestorMapping lsdt = new LSDTInvestorMapping() { LoanSolutionInvestor = "A", DataTracInvestor = "B" };
            Assert.That(lsdt.ToHtmlRow(), Is.EqualTo("<tr><td>A</td><td>B</td></tr>"));
        }

        [Test]
        public void Should_be_able_to_get_html_table()
        {
            LSDTInvestorMapping lsdt = new LSDTInvestorMapping() { LoanSolutionInvestor = "A", DataTracInvestor = "B" };
            List<LSDTInvestorMapping> lists = new List<LSDTInvestorMapping>() { lsdt };

            Assert.That(LSDTInvestorMapping.ToHtmlTable(lists), Is.EqualTo("<table><tr><td>Loan Solution Investor</td><td>Data Trac Investor</td></tr><tr><td>A</td><td>B</td></tr></table>"));
        }

        [Test]
        public void Should_be_able_to_get_code_in_the_list_with_a_given_lsinvestor()
        {
            LSDTInvestorMapping lsdt1 = new LSDTInvestorMapping() { LoanSolutionInvestor = "A", DataTracInvestor = "B" };
            LSDTInvestorMapping lsdt2 = new LSDTInvestorMapping() { LoanSolutionInvestor = "C", DataTracInvestor = "D" };

            List<LSDTInvestorMapping> lists = new List<LSDTInvestorMapping>() { lsdt1, lsdt2 };

            Assert.That(LSDTInvestorMapping.GetCodeFor("a", lists), Is.EqualTo("B"));
            Assert.That(LSDTInvestorMapping.GetCodeFor("C", lists), Is.EqualTo("D"));
        }

        [Test]
        public void Should_return_empty_string_if_code_is_not_in_the_list()
        {
            LSDTInvestorMapping lsdt1 = new LSDTInvestorMapping() { LoanSolutionInvestor = "A", DataTracInvestor = "B" };
            LSDTInvestorMapping lsdt2 = new LSDTInvestorMapping() { LoanSolutionInvestor = "C", DataTracInvestor = "D" };

            List<LSDTInvestorMapping> lists = new List<LSDTInvestorMapping>() { lsdt1, lsdt2 };

            Assert.That(LSDTInvestorMapping.GetCodeFor("e", lists), Is.EqualTo(""));
        }
    }
}
