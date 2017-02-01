using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Compliance
{
    public interface IChaseFundingFormDao
    {
        ChaseFundingFormLoanInfo GetLoanInfo(string loanNumber);
        void SaveGeneralInfo(string fileId, string purchaseProperty, string item2, string chkOccInvestmentYes, string chkOccInvestmentNo, string chkOccInvestmentNA);
        void SaveATRQM(string fileId, string aporPcnt, string qmSafeHarbor, string qmRebuttablePresumption, string nonQM, string qmNotApplicable, string item7AYes, string item7ANo, string item7BYes, string item7BNo, string item8Yes, string item8No);
        void SaveHighCost(string fileId, string prepaymentPenalty);
        void SaveHighCostContinued(string fileId, string pointsExcluded, string feesImposed, string hoepaAPR);
        void SaveExcludedBonafide(string fileId, string item15Percent, string item15Amount, string hoepaQMPcnt, string hoepaQMAmount, string statePcnt, string stateAmount);
        void SaveSpecialFeature(string fileId, string SFFannieMae, string SFFreddieMac, string MIMonthlyPremium, string MISinglePremium);
        
    }

    public class ChaseFundingFormDao : AbstractDao<ChaseFundingFormLoanInfo, string>, IChaseFundingFormDao
    {
        public ChaseFundingFormDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(AuditScoreCardDao));
        }


        public ChaseFundingFormLoanInfo GetLoanInfo(string loanNumber)
        {
            var loan = m_session.CreateSQLQuery("exec xGEM_ChaseFundingForm_GetLoan :loanNumber")
                .AddEntity(typeof(ChaseFundingFormLoanInfo))
                .SetString("loanNumber", loanNumber)
                .UniqueResult<ChaseFundingFormLoanInfo>();

            return loan;
        }

        public void SaveGeneralInfo(string fileId, string purchaseProperty, string item2, string chkOccInvestmentYes, string chkOccInvestmentNo, string chkOccInvestmentNA)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveGeneralInfo";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@purchaseProperty", purchaseProperty);
                cmd.Parameters.AddWithValue("@item2", item2);
                cmd.Parameters.AddWithValue("@chkOccInvestmentYes", chkOccInvestmentYes);
                cmd.Parameters.AddWithValue("@chkOccInvestmentNo", chkOccInvestmentNo);
                cmd.Parameters.AddWithValue("@chkOccInvestmentNA", chkOccInvestmentNA);

                ExecuteNonQuery(cmd);
            }
        }

        public void SaveATRQM(string fileId, string aporPcnt, string qmSafeHarbor, string qmRebuttablePresumption, string nonQM, string qmNotApplicable, string item7AYes, string item7ANo, string item7BYes, string item7BNo, string item8Yes, string item8No)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveATRQM";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@aporPcnt", aporPcnt);
                cmd.Parameters.AddWithValue("@qmSafeHarbor", qmSafeHarbor);
                cmd.Parameters.AddWithValue("@qmRebuttablePresumption", qmRebuttablePresumption);
                cmd.Parameters.AddWithValue("@nonQM", nonQM);
                cmd.Parameters.AddWithValue("@qmNotApplicable", qmNotApplicable);
                cmd.Parameters.AddWithValue("@item7AYes", item7AYes);
                cmd.Parameters.AddWithValue("@item7ANo", item7ANo);
                cmd.Parameters.AddWithValue("@item7BYes", item7BYes);
                cmd.Parameters.AddWithValue("@item7BNo", item7BNo);
                cmd.Parameters.AddWithValue("@item8Yes", item8Yes);
                cmd.Parameters.AddWithValue("@item8No", item8No);

                ExecuteNonQuery(cmd);
            }
        }

        public void SaveHighCost(string fileId, string prepaymentPenalty)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveHighCost";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@prepaymentPenalty", prepaymentPenalty);

                ExecuteNonQuery(cmd);
            }
        }

        public void SaveHighCostContinued(string fileId, string pointsExcluded, string feesImposed, string hoepaAPR)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveHighCostContinued";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@pointsExcluded", pointsExcluded);
                cmd.Parameters.AddWithValue("@feesImposed", feesImposed);
                cmd.Parameters.AddWithValue("@hoepaAPR", hoepaAPR);

                ExecuteNonQuery(cmd);
            }
        }

        public void SaveSpecialFeature(string fileId, string SFFannieMae, string SFFreddieMac, string MIMonthlyPremium, string MISinglePremium)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveSpecialFeature";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@SFFannieMae", SFFannieMae);
                cmd.Parameters.AddWithValue("@SFFreddieMac", SFFreddieMac);
                cmd.Parameters.AddWithValue("@MIMonthlyPremium", MIMonthlyPremium);
                cmd.Parameters.AddWithValue("@MISinglePremium", MISinglePremium);

                ExecuteNonQuery(cmd);
            }
        }

        public void SaveExcludedBonafide(string fileId, string item15Percent, string item15Amount, string hoepaQMPcnt, string hoepaQMAmount, string statePcnt, string stateAmount)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ChaseFundingForm_SaveExcludeBonafide";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@item15Percent", item15Percent);
                cmd.Parameters.AddWithValue("@item15Amount", item15Amount);
                cmd.Parameters.AddWithValue("@hoepaQMPcnt", hoepaQMPcnt);
                cmd.Parameters.AddWithValue("@hoepaQMAmount", hoepaQMAmount);
                cmd.Parameters.AddWithValue("@statePcnt", statePcnt);
                cmd.Parameters.AddWithValue("@stateAmount", stateAmount);

                ExecuteNonQuery(cmd);
            }
        }
    }
}
