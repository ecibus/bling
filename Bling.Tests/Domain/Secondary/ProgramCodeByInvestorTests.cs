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
    public class ProgramCodeByInvestorTests
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
            ProgramCodeByInvestor pcbi = new ProgramCodeByInvestor 
            { 
                Investor = "Investor1", Total = 100, Displayed = 50, Hidden = 50 
            };

            string expected = String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", 
                pcbi.Investor, pcbi.Total, pcbi.Hidden, pcbi.Displayed);

            Assert.That(pcbi.ToHtmlRow(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_list_to_html_table()
        {
            ProgramCodeByInvestor pcbi = new ProgramCodeByInvestor
            {
                Investor = "Investor1",
                Total = 100,
                Displayed = 50,
                Hidden = 50
            };

            string expected = String.Format(
                "<table class='t1'>" +
                    "<tr class='yellow'><td>Investor</td><td>Total Program Code</td><td>Hidden</td><td>Displayed</td>" +
                    "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>" +
                "</table>",
                pcbi.Investor, pcbi.Total, pcbi.Hidden, pcbi.Displayed);

            List<ProgramCodeByInvestor> list = new List<ProgramCodeByInvestor> { pcbi };
            Assert.That(ProgramCodeByInvestor.ToHtmlTable(list), Is.EqualTo(expected));

        }
    }
}
