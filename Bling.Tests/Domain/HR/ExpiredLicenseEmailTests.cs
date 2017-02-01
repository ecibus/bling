using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Bling.Domain.HR;

namespace Bling.Tests.Domain.HR
{
    [TestFixture]
    public sealed class ExpiredLicenseEmailTests
    {
        private MockFactory m_MockFactory;

        [SetUp]
        public void SetUp()
        {
            m_MockFactory = new MockFactory(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            m_MockFactory.VerifyAll();
        }

        [Test, Category("Database")]
        public void Should_be_able_to_send_email()
        {
            //Arrange
            ExpiredLicenseEmail mail = new ExpiredLicenseEmail("");
            List<ExpiredLicense> lists = new List<ExpiredLicense>();
            lists.Add(new ExpiredLicense { EmployeeId = "336COR", EMail = "a@a.a", BranchEMail = "branch@mail.com", EmployeeName = "CORONADO, ETHAN", BranchNo = "336", Branch = "Thousands Oaks #336" });
            lists.Add(new ExpiredLicense { EmployeeId = "336GAR", EMail = "a@a.a", BranchEMail = "branch@mail.com", EmployeeName = "GARCIA, LISETTE", BranchNo = "336", Branch = "Thousands Oaks #336" });
            lists.Add(new ExpiredLicense { EmployeeId = "336KAT", EMail = "a@a.a", BranchEMail = "branch@mail.com", EmployeeName = "KATZ, FELIX", BranchNo = "336", Branch = "Thousands Oaks #336" });
            lists.Add(new ExpiredLicense { EmployeeId = "336NAW", EMail = "a@a.a", BranchEMail = "branch@mail.com", EmployeeName = "NAWABI, JOSEPH", BranchNo = "336", Branch = "Thousands Oaks #336" });
            	
            //Act
            mail.Send(lists, new DateTime(2009, 12, 31));

            //Assert
        }
    }
}
