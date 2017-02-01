using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bling.Domain.Compliance;
using NUnit.Framework.SyntaxHelpers;

namespace Bling.Tests.Domain.Compliance
{
    [TestFixture]
    public class DIRWLoanInfoTests
    {
        [Test]
        public void Json_ReturnsAJsonObject()
        {
            //Arrange
            DIRWLoanInfo loan = new DIRWLoanInfo
            {                
                FileId = @"\<>aa",
                LoanNumber = "12345",
                Borrower = "Borrower",
                DateReviewed = "DateReviewed",
                Funder = "Funder",
                LoanProgram = "LoanProgram",
                Processor = "Processor",
                Reviewer = "Reviewer",
                Status = "Status",
                Underwriter = "Underwriter",
                FundedDate = "1/1/2010"
            };

            //Act
            string json = loan.ToJson();

            //Assert
            string expected = "{ FileId : '\\\\&lt;&gt;aa', LoanNumber : '12345', Borrower : \"Borrower\", DateReviewed : 'DateReviewed', " +
                "Funder : \"Funder\", LoanProgram : 'LoanProgram', Processor : \"Processor\", ReviewType : \"Closed Loan\", Reviewer : \"Reviewer\", " +
                "Status : 'Status', State : '', Underwriter : \"Underwriter\" }";

            Assert.That(json, Is.EqualTo(expected));

        }

        [Test]
        public void Test()
        {
            Console.WriteLine(3 | 2);
            //Assert.That(2, Is.EqualTo(1 | 1));
        }
    }
}
