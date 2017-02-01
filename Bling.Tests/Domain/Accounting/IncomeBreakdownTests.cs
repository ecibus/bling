using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework.SyntaxHelpers;
using NUnit.Framework;
using Bling.Domain;
using Bling.Domain.Accounting;

namespace Bling.Tests.Domain.Accounting
{
    [TestFixture]
    public class IncomeBreakdownTests
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
        public void Should_be_able_to_compute_price_difference_when_commit_price_is_not_zero()
        {
            IncomeBreakdown ib = new IncomeBreakdown { CommitPrice = 1, InvestorIncentive = 1,
                InvestorPrice = 1, InvestorSRP = 1
            };

            MWE3Data mw = new MWE3Data { BranchPrice = 1, BranchSRP = 1, H5125_SHB = 1, CommitSRP = 1 };
            ib.MWE3 = mw;

            Assert.That(ib.PriceDifference, Is.EqualTo(-2m));            
        }

        [Test]
        public void Should_be_able_to_compute_price_difference_when_commit_price_is_zero()
        {
            IncomeBreakdown ib = new IncomeBreakdown
            {
                CommitPrice = 0,
                InvestorIncentive = 1,
                InvestorPrice = 1,
                InvestorSRP = 1
            };

            MWE3Data mw = new MWE3Data { BranchPrice = 1, BranchSRP = 1, H5125_SHB = 1, CommitSRP = 1 };
            ib.MWE3 = mw;

            Assert.That(ib.PriceDifference, Is.EqualTo(-2m));     
        }

        [Test]
        public void Should_be_able_to_compute_price_adjustment()
        {
            IncomeBreakdown ib = new IncomeBreakdown
            {
                CommitPrice = 0,
                InvestorIncentive = 1,
                InvestorPrice = 1,
                InvestorSRP = 1
            };

            MWE3Data mw = new MWE3Data
            {
                BranchPrice = 1,
                BranchSRP = 1,
                H5125_SHB = 1,
                CommitSRP = 1,
                UnpaidPrincipalBalance = 100
            };

            ib.MWE3 = mw;

            Assert.That(ib.PriceAdjustment, Is.EqualTo(-2m));    
        }

        [Test]
        public void Months_difference_should_return_zero_when_unpaid_principal_balance_is_zero()
        {
            IncomeBreakdown ib = new IncomeBreakdown { InvestorInterestPaidToDate = new DateTime(2009, 1, 1) };
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 0 };
            ib.MWE3 = mw;

            Assert.That(ib.MonthsDifference, Is.EqualTo(0));                
        }

        [Test]
        public void Should_be_able_to_compute_months_difference()
        {            
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 10, FirstPaymentDate = new DateTime(2008, 1, 1) };
            IncomeBreakdown ib = new IncomeBreakdown { InvestorInterestPaidToDate = new DateTime(2009, 1, 1),                
            };
            ib.MWE3 = mw;
            Assert.That(ib.MonthsDifference, Is.EqualTo(13));
        }

        [Test]
        public void Should_be_able_to_compute_impounds_owed_at_sale()
        {
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 10, FirstPaymentDate = new DateTime(2008, 1, 1),
                ImpoundsAtClose = 1, H1001_HazardInsurance = 2, H1002_MI = 3, H1004_CountyTax = 4,
                H1006_FloodInsurance = 5, H1007_Other = 6
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1),
            };
            ib.MWE3 = mw;

            Assert.That(ib.ImpoundsOwedAtSale, Is.EqualTo(261));
        }

        [Test]
        public void Tax_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 0, FirstPaymentDate = new DateTime(2008, 1, 1) };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.Tax, Is.EqualTo(0));
        }

        [Test]
        public void Tax_should_be_computed_when_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 10, FirstPaymentDate = new DateTime(2008, 1, 1), 
                H1004_CountyTax = 10 };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2008, 12, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.Tax, Is.EqualTo(-120));
        }

        [Test]
        public void MMI_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data mw = new MWE3Data { UnpaidPrincipalBalance = 0, 
                FirstPaymentDate = new DateTime(2008, 1, 1), H1002_MI = 10 };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1)
                
            };
            ib.MWE3 = mw;

            Assert.That(ib.MMI, Is.EqualTo(0));
        }

        [Test]
        public void MMI_should_be_computed_when_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 10,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1002_MI = 10
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2008, 12, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.MMI, Is.EqualTo(-120));
        }

        [Test]
        public void Insurance_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 0,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1001_HazardInsurance = 10
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1)

            };
            ib.MWE3 = mw;

            Assert.That(ib.Insurance, Is.EqualTo(0));
        }

        [Test]
        public void Insurance_should_be_computed_when_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 10,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1001_HazardInsurance = 10
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2008, 12, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.Insurance, Is.EqualTo(-120));
        }

        [Test]
        public void Other_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 0,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1006_FloodInsurance = 10,
                H1007_Other = 5
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1)

            };
            ib.MWE3 = mw;

            Assert.That(ib.Other, Is.EqualTo(0));
        }

        [Test]
        public void Other_should_be_computed_when_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 10,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1006_FloodInsurance = 10,
                H1007_Other = 5
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2008, 12, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.Other, Is.EqualTo(-180));
        }

        [Test]
        public void PrincipalAndEscrow_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 0,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1006_FloodInsurance = 10,
                H1007_Other = 5
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2009, 1, 1)

            };
            ib.MWE3 = mw;

            Assert.That(ib.PrincipalAndEscrow, Is.EqualTo(0));
        }

        [Test]
        public void PrincipalAndEscrow_should_be_computed_when_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data mw = new MWE3Data
            {
                UnpaidPrincipalBalance = 10,
                FirstPaymentDate = new DateTime(2008, 1, 1),
                H1006_FloodInsurance = 10,
                H1007_Other = 5
            };
            IncomeBreakdown ib = new IncomeBreakdown
            {
                InvestorInterestPaidToDate = new DateTime(2008, 12, 1)
            };
            ib.MWE3 = mw;

            Assert.That(ib.PrincipalAndEscrow, Is.EqualTo(-170));
        }

        [Test]
        public void InvestorInterest_should_return_zero_if_purchase_date_has_no_value()
        {
            MWE3Data mw = new MWE3Data();
            IncomeBreakdown ib = new IncomeBreakdown { InvestorInterestPaidToDate = new DateTime(2008, 1, 1), MWE3 = mw };
            
            Assert.That(ib.InvestorInterest, Is.EqualTo(0));
        }

        [Test]
        public void InvestorInterest_should_compute_when_purchaseddate_is_gt_investorinterestpaidtodate()
        {
            MWE3Data mw = new MWE3Data { PurchasedDate = new DateTime(2008, 1, 2) };
            IncomeBreakdown ib = new IncomeBreakdown { InvestorInterestPaidToDate = new DateTime(2008, 1, 1), MWE3 = mw };

            Assert.That(ib.InvestorInterest, Is.EqualTo(1));
        }

        [Test]
        public void InvestorInterest_should_compute_when_investorinterestpaidtodate_is_gt_purchaseddate()
        {
            MWE3Data mw = new MWE3Data { PurchasedDate = new DateTime(2008, 1, 2) };
            IncomeBreakdown ib = new IncomeBreakdown { InvestorInterestPaidToDate = new DateTime(2008, 1, 10), MWE3 = mw };

            Assert.That(ib.InvestorInterest, Is.EqualTo(-7));
        }
    }
}
