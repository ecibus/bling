using System;
using Bling.Domain.LOS;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Bling.Tests.Domain.LOS
{
    [TestFixture]
    public class HMDATests
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
        public void Should_be_able_to_create_string_representation_of_object()
        {
            HMDA hmda = new HMDA() { 
                LoanNumber = "1", SubmissionDate = "2", ProgramName = "3", LoanType = "4", PropertyType = "5", 
                HMDALoanPurpose = "6", OwnerOccupied = "7", LoanAmount = "8", PreApproval = "9", ActionType = "10", 
                ActionDate = "11", PropertyStreetNo = "12", PropertyStreet = "13", PropertyCity = "14", County = "15", 
                PropertyState = "16", PropertyZipCode = "17", CensusTract = "18", BorrowerRace = "19", CoborrowerRace = "20", 
                BorrowerSex = "21", CoborrowerSex = "22", BorrowerEthnicity = "23", SpouseEthnicity = "24", BorrowerIncome = "25", 
                CoborrowerIncome = "26", TypeOfPurchaser = "27", HmdaDenialReason = "28", RateSpread = "29", TreasuryYield = "30", 
                APRFromTIL = "31", LoanTerm = "32", Lien = "33", LockDateTime = "34", HOPEA = "35", 
                Investor = "36", Underwriter = "37", LoanTermType="38", OriginalAPRTerm="39" };

            Assert.That(hmda.ToString(), Is.EqualTo("1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39"));
        }

    }
}
