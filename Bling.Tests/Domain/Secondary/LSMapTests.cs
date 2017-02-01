using System;
using System.Collections.Generic;
using Bling.Domain.Secondary;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.Secondary
{
    [TestFixture]
    public class LSMapTests
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
        public void Should_be_able_to_build_dropdown_of_investors()
        {
            string expected =
                "<select name='ddlInvestor' id='ddlInvestor' class='s1'>" +
                    "<option value=''>-- Select Investor to Show or Hide --</option>" +
                    "<option value='Investor1'>Investor1</option>" +
                    "<option value='Investor2'>Investor2</option>" +
                "</select>";

            List<string> investors = new List<string> { "Investor1", "Investor2" };
            Assert.That(LSMap.BuildInvestorDropdownHtml(investors), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_build_dropdown_of_program_code()
        {
            string expected =
                "<select name='ddlLoanCode' id='ddlLoanCode' class='s1'>" +
                    "<option value=''>-- Select Program Code --</option>" +
                    "<option value='Code1'>Code1</option>" +
                    "<option value='Code2'>Code2</option>" +
                "</select>";

            List<string> code = new List<string> { "Code1", "Code2" };
            Assert.That(LSMap.BuildLoanCodeDropdownHtml(code), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_exclude_to_checkbox()
        {
            LSMap map = new LSMap { Exclude = true, Id = 1 };
            string expected = "<input id='1' type='checkbox' checked />";
            Assert.That(map.ToHtmlCheckBox(), Is.EqualTo(expected));
        }

        [Test]
        public void Checkbox_should_not_be_check_when_exclude_is_false()
        {
            LSMap map = new LSMap { Exclude = false, Id = 1 };
            string expected = "<input id='1' type='checkbox' />";
            Assert.That(map.ToHtmlCheckBox(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_object_to_html_row()
        {
            LSMap map = new LSMap 
            {
                Id = 1,
                LS_InvestorCode = "Investor",
                LS_LoanCode = "Code",
                LS_LoanDescription = "Description",
                Exclude = true 
            };

            string expected = "<tr><td>Investor</td><td>Code</td><td>Description</td><td><input id='1' type='checkbox' checked /></td></tr>";

            Assert.That(map.ToHtmlRow(), Is.EqualTo(expected));
        }

        [Test]
        public void Should_be_able_to_convert_list_to_html_table()
        {
            LSMap map = new LSMap
            {
                Id = 1,
                LS_InvestorCode = "Investor",
                LS_LoanCode = "Code",
                LS_LoanDescription = "Description",
                Exclude = true
            };

            List<LSMap> list = new List<LSMap> { map };

            string expected =
                "<table class='t1'>" +
                    "<tr class='yellow'><td>Investor</td><td>Code</td><td>Description</td><td>Hide?</td></tr>" +
                    "<tr><td>Investor</td><td>Code</td><td>Description</td><td><input id='1' type='checkbox' checked /></td></tr>" +
                "</table>";

            Assert.That(LSMap.ToHtmlTable(list), Is.EqualTo(expected));
        }
    }
}
