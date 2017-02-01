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
    public class LoanCountRuleTests
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
        public void Should_be_able_to_display_message_when_loan_is_more_than_6000()
        {
            List<HMDA> list = new List<HMDA>();
            for (int i = 0; i < 20001; i++)
            {
                list.Add(new HMDA());
            }            

            HMDAVerify verify = new HMDAVerify(list);

            verify.RegisterRule(new LoanCountRule());

            string message = "Validation Warning:<br/><ul><li>Loan contains 20,001.  The APR and Denial Workbook only support up to 20000 loans.</li></ul>";
            Assert.That(verify.GetWarningMessage(), Is.EqualTo(message));
        }

        [Test]
        public void Should_be_able_to_display_empty_string_when_list_is_less_than_6000()
        {
            List<HMDA> list = new List<HMDA>();
            list.Add(new HMDA());

            HMDAVerify verify = new HMDAVerify(list);

            verify.RegisterRule(new LoanCountRule());

            Assert.That(verify.GetWarningMessage(), Is.EqualTo(String.Empty));
        }

        [Test]
        public void Should_be_able_to_display_empty_string_when_list_is_equal_to_6000()
        {
            List<HMDA> list = new List<HMDA>();
            for (int i = 0; i < 6000; i++)
            {
                list.Add(new HMDA());
            }  

            HMDAVerify verify = new HMDAVerify(list);

            verify.RegisterRule(new LoanCountRule());

            Assert.That(verify.GetWarningMessage(), Is.EqualTo(String.Empty));
        }
    }
}
