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
    public class LoanSolutionProgramTests
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
        public void Should_be_able_to_create_object_from_string()
        {
            LoanSolutionProgram lsp = new LoanSolutionProgram("a,b,c");
            Assert.That(lsp.InvestorName, Is.EqualTo("a"));
            Assert.That(lsp.InvestorProductName, Is.EqualTo("b"));
            Assert.That(lsp.InvestorProductCodeAlias, Is.EqualTo("c"));
        }

        [Test]
        public void Missing_alias_should_still_get_a_valid_object()
        {
            LoanSolutionProgram lsp = new LoanSolutionProgram("a,b");
            Assert.That(lsp.InvestorName, Is.EqualTo("a"));
            Assert.That(lsp.InvestorProductName, Is.EqualTo("b"));
            Assert.That(lsp.InvestorProductCodeAlias, Is.EqualTo(""));
        }

        [Test]
        public void Missing_alias_with_comma_at_the_end_should_still_get_a_valid_object()
        {
            LoanSolutionProgram lsp = new LoanSolutionProgram("a,b,");
            Assert.That(lsp.InvestorName, Is.EqualTo("a"));
            Assert.That(lsp.InvestorProductName, Is.EqualTo("b"));
            Assert.That(lsp.InvestorProductCodeAlias, Is.EqualTo(""));
        }
    }
}
