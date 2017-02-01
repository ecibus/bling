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
    public class HOPEARuleTests
    {
        private MockRepository m_mocks;
        private List<HMDA> m_List;

        [SetUp]
        public void SetUp()
        {
            m_mocks = new MockRepository();
            m_List = new List<HMDA>();
            m_List.Add(new HMDA() { LoanNumber = "1", HOPEA = "Yes" });            
            m_List.Add(new HMDA() { LoanNumber = "2", HOPEA = "No" });
        }

        [TearDown]
        public void TearDown()
        {
            m_mocks.VerifyAll();
        }

        [Test]
        public void Should_return_a_message_when_list_contains_hopea()
        {
            m_List.Add(new HMDA() { LoanNumber = "3", HOPEA = "Yes" });

            HMDAVerify verify = new HMDAVerify(m_List);

            verify.RegisterRule(new HOPEARule());

            string message = "Validation Warning:<br/><ul><li>2 loans contain 'YES' in Hopea</li></ul>";
            Assert.That(verify.GetWarningMessage(), Is.EqualTo(message));
        }

        [Test]
        public void Should_be_able_to_use_singular_in_message()
        {
            HMDAVerify verify = new HMDAVerify(m_List);

            verify.RegisterRule(new HOPEARule());

            string message = "Validation Warning:<br/><ul><li>1 loan contain 'YES' in Hopea</li></ul>";
            Assert.That(verify.GetWarningMessage(), Is.EqualTo(message));
        }

        [Test]
        public void Should_be_able_to_get_empty_string_if_rule_is_passed()
        {
            m_List[0].HOPEA = "No";

            HMDAVerify verify = new HMDAVerify(m_List);

            verify.RegisterRule(new HOPEARule());

            Assert.That(verify.GetWarningMessage(), Is.EqualTo(String.Empty));
        }
    }
}
