using System;
using Bling.Domain.Accounting;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Bling.Domain.Extension;

namespace Bling.Tests.Domain.Accounting
{
    [TestFixture]
    public class MWE3DataTests
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
        public void Should_be_able_to_build_borrower_lastname_and_firstname()
        {
            MWE3Data m = new MWE3Data { BorrowerFirstName = "A", BorrowerLastName = "B" };

            Assert.That(m.BorrowerLastFirstName, Is.EqualTo("B, A"));
        }

        [Test]
        public void PurchasedPaidoffDate_should_return_purchased_date_when_purchase_date_has_value()
        {
            MWE3Data m = new MWE3Data { PurchasedDate = new DateTime(2001, 1, 1), PaidoffDate = new DateTime(2002, 2, 2) };

            Assert.That(m.PurchasedPaidoffDate, Is.EqualTo(m.PurchasedDate));
        }

        [Test]
        public void PurchasedPaidoffDate_should_return_paidoff_date_when_purchase_date_has_no_value()
        {
            MWE3Data m = new MWE3Data { PurchasedDate = null, PaidoffDate = new DateTime(2002, 2, 2) };

            Assert.That(m.PurchasedPaidoffDate, Is.EqualTo(m.PaidoffDate));
        }

        [Test]
        public void Should_be_able_to_compute_for_appraisal_fee()
        {
            MWE3Data m = new MWE3Data
            {
                H803_AppraisalFee = 10,
                H1304_ReinspectionFee = 10,
                H208_AppraisalCredit = 5
            };

            Assert.That(m.AppraisalFee, Is.EqualTo(15));
        }

        [Test]
        public void RFCPackageWireFee_should_return_zero_when_channel_is_brokered()
        {
            MWE3Data m = new MWE3Data { Channel = "Brokered" };
            Assert.That(m.RFCPackageWireFee, Is.EqualTo(0));
        }

        [Test]
        public void RFCPackageWireFee_should_return_75_when_channel_is_not_brokered_and_closed_date_is_gt_10012007()
        {
            MWE3Data m = new MWE3Data { Channel = "", ClosedDate = new DateTime(2008, 1, 1) };
            Assert.That(m.RFCPackageWireFee, Is.EqualTo(75));
        }

        [Test]
        public void RFCPackageWireFee_should_return_50_when_channel_is_not_brokered_and_closed_date_is_lt_10012007()
        {
            MWE3Data m = new MWE3Data { Channel = "", ClosedDate = new DateTime(2005, 1, 1) };
            Assert.That(m.RFCPackageWireFee, Is.EqualTo(50));
        }

        [Test]
        public void Should_be_able_to_compute_credit_report()
        {
            MWE3Data m = new MWE3Data { H804_CreditReportFee = 20, H209_CreditReportCredit = 10 };
            Assert.That(m.CreditReport, Is.EqualTo(10));
        }

        [Test]
        public void PMI_MIP_VAFF_should_return_zero_if_channel_is_brokered()
        {
            MWE3Data m = new MWE3Data
            {
                Channel = "Brokered",
                DBSource = "M",
                H902_MI = 10,
                H809_VAFundingFee = 10,
                H213_MICredit = 10,
                InitPMI = 5
            };

            Assert.That(m.PMI_MIP_VAFF, Is.EqualTo(0));
        }

        [Test]
        public void PMI_MIP_VAFF_should_compute_when_channel_is_not_brokered_and_dbsource_is_m()
        {
            MWE3Data m = new MWE3Data
            {
                Channel = "",
                DBSource = "M",
                H902_MI = 10,
                H809_VAFundingFee = 10,
                H213_MICredit = 10,
                InitPMI = 5
            };

            Assert.That(m.PMI_MIP_VAFF, Is.EqualTo(10));
        }

        [Test]
        public void PMI_MIP_VAFF_should_return_initpmi_when_channel_is_not_brokered_and_dbsource_is_not_m()
        {
            MWE3Data m = new MWE3Data
            {
                Channel = "",
                DBSource = "",
                H902_MI = 10,
                H809_VAFundingFee = 10,
                H213_MICredit = 10,
                InitPMI = 5
            };

            Assert.That(m.PMI_MIP_VAFF, Is.EqualTo(5));
        }

        [Test]
        public void TaxServiceFee_should_return_zero_if_channel_is_brokered()
        {
            MWE3Data m = new MWE3Data { Channel = "Brokered", H815_TaxServiceFee = 10 };
            Assert.That(m.TaxServiceFee, Is.EqualTo(0));
        }

        [Test]
        public void TaxServiceFee_should_return_H815_TaxServiceFee_if_channel_is_not_brokered()
        {
            MWE3Data m = new MWE3Data { Channel = "", H815_TaxServiceFee = 10 };
            Assert.That(m.TaxServiceFee, Is.EqualTo(10));
        }

        [Test]
        public void PrincipalDeduction_should_return_zero_if_unpaid_principal_balance_is_zero()
        {
            MWE3Data m = new MWE3Data { AdjustedLoanAmount = 10 };
            Assert.That(m.PrincipalReduction.ToValue(), Is.EqualTo(0));
        }

        [Test]
        public void PrincipalDeduction_should_be_computed_if_unpaid_principal_balance_is_not_zero()
        {
            MWE3Data m = new MWE3Data { AdjustedLoanAmount = 10, UnpaidPrincipalBalance = 1 };
            Assert.That(m.PrincipalReduction.ToValue(), Is.EqualTo(-9));
        }

        [Test]
        public void UnderwritingFee_should_return_zero_if_channel_is_brokered()
        {
            MWE3Data m = new MWE3Data { Channel = "brokered", H814_UnderwritingFee = 10 };
            Assert.That(m.UnderwritingFee, Is.EqualTo(0));
        }

        [Test]
        public void UnderwritingFee_should_return_the_fee_if_channel_is_not_brokered()
        {
            MWE3Data m = new MWE3Data { Channel = "", H814_UnderwritingFee = 10 };
            Assert.That(m.UnderwritingFee, Is.EqualTo(10));
        }

        [Test]
        public void WarehouseBank_should_return_RFC_when_ClosedDate_is_lt_04012006_and_warehouse_line_is_bofa()
        {
            MWE3Data m = new MWE3Data { WarehouseLine = "bofa", ClosedDate = new DateTime(2006, 3, 31) };
            Assert.That(m.WarehouseBank, Is.EqualTo("RFC"));
        }

        [Test]
        public void WarehouseBank_should_return_W_when_warehouse_line_is_wamu()
        {
            MWE3Data m = new MWE3Data { WarehouseLine = "w", ClosedDate = new DateTime(2006, 3, 31) };
            Assert.That(m.WarehouseBank, Is.EqualTo("WAMU"));
        }

        [Test]
        public void WarehouseBank_should_return_RFC_when_warehouse_line_starts_with_rfc()
        {
            MWE3Data m = new MWE3Data { WarehouseLine = "rfc123", ClosedDate = new DateTime(2006, 3, 31) };
            Assert.That(m.WarehouseBank, Is.EqualTo("RFC"));
        }

        [Test]
        public void WarehouseBank_should_return_warehouse_line()
        {
            MWE3Data m = new MWE3Data { WarehouseLine = "anything", ClosedDate = new DateTime(2006, 3, 31) };
            Assert.That(m.WarehouseBank, Is.EqualTo("ANYTHING"));
        }

        [Test]
        public void Should_be_able_to_compute_MiscIncome()
        {
            MWE3Data m = new MWE3Data { H1305_MiscFee = 1, H808_BrokerOrigFee = 2, H805_AdminFee = 3, H1113_ClosingFee = 4 };
            Assert.That(m.MiscIncome, Is.EqualTo(10));
        }

    }
}
