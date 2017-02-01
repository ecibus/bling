using System;
using System.Collections.Generic;
using Bling.Domain.Accounting;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.Accounting
{
    [TestFixture]
    public class BranchTests
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
            Branch b1 = new Branch { BranchCode = 1, BranchName = "Branch1" };
            Branch b2 = new Branch { BranchCode = 2, BranchName = "Branch2" };

            List<Branch> branches = new List<Branch> { b1, b2 };

            string expected = "<select id='ActiveBranch' name='ActiveBranch' size='10'>" +
                "<option value='1'>(1) Branch1</option>" +
                "<option value='2'>(2) Branch2</option>" +
                "</select>"
                ;

            NUnit.Framework.Assert.That(Branch.ToHtmlOptionList("ActiveBranch", branches), Is.EqualTo(expected));

        }
    }
}
