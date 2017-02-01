using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain;

namespace Bling.Tests.Domain
{
    [TestFixture]
    public class InvestorTests
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
        public void Should_be_able_to_convert_object_to_option_html()
        {
            Investor investor = new Investor { Id = "AAA", Name = "InvestorName", Inv = "INV" };
            Assert.That(investor.ToOptionHtml("abc"), Is.EqualTo("<option value='AAA'>INV (InvestorName)</option>"));
        }

        [Test]
        public void Should_be_able_to_add_selected_if_code_is_the_same_as_the_id()
        {
            Investor investor = new Investor { Id = "AAA", Name = "InvestorName", Inv = "INV" };
            Assert.That(investor.ToOptionHtml("aaa"), Is.EqualTo("<option value='AAA' selected>INV (InvestorName)</option>"));
        }

        [Test]
        public void Should_be_able_to_add_selected_if_code_is_the_same_as_the_id_and_case_is_different()
        {
            Investor investor = new Investor { Id = "aaa", Name = "InvestorName", Inv = "INV" };
            Assert.That(investor.ToOptionHtml("AAA"), Is.EqualTo("<option value='aaa' selected>INV (InvestorName)</option>"));
        }

        [Test]
        public void Should_be_able_to_convert_investor_to_select_html()
        {
            Investor investor1 = new Investor { Id = "AAA", Name = "InvestorName", Inv = "INV" };
            Investor investor2 = new Investor { Id = "BBB", Name = "InvestorName", Inv = "INV" };
            List<Investor> list = new List<Investor> { investor1, investor2 };

            string expected = "" +
                "<select id='id' class='s1'>" +
                "<option value=''> -- Please Select --</option>" +
                "<option value='AAA'>INV (InvestorName)</option>" +
                "<option value='BBB'>INV (InvestorName)</option>" +
                "</select>";

            Assert.That(Investor.ToSelectHtml(list, "id", "BBc"), Is.EqualTo(expected));

        }
    }
}
