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
    public interface ILPELoanInfoDao
    {
        IList<LPELoanInfo> GetReadyForDocs();
        LPELoanInfo GetLoanInfo(string loannumber);
        void UpdateReadyForDocs(string loannumber, bool value);
        void UpdateReasonAndComment(string loannumber, int reasonId, string comment, string commentedby);
        void InitialReviewComplete(string loannumber, string reviewedBy);
        LPELoanInfo GetLastChanges(string loannumber);
        void AddHistory(string createdBy, string loanNumber, string borrower, string loanType, double loanAmount,
            double gemLoanFeeCharged, double loanOriginationFeeCharged, double loanOfficerPrice, double borrowerPaidDiscount,
            double lenderCredit, string ficoScore, string applicationDate, string lockedDate, string noOfBorrower,
            string programType, string transactionType, double finalNetPrice);
    }

    public class LPELoanInfoDao : AbstractDao<LPELoanInfo, string>, ILPELoanInfoDao
    {
        public LPELoanInfoDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(LPELoanInfoDao));
        }

        public IList<LPELoanInfo> GetReadyForDocs()
        {
            return m_session.CreateSQLQuery("exec xGEM_LPE_GetReadyForDocs")
                .AddEntity(typeof(LPELoanInfo))
                .List<LPELoanInfo>();
        }

        public LPELoanInfo GetLoanInfo(string loannumber)
        {
            LPELoanInfo loan = m_session.CreateSQLQuery("exec xGEM_LPE_GetLoan :loannumber ")
                .AddEntity(typeof(LPELoanInfo))
                .SetString("loannumber", loannumber)
                .UniqueResult<LPELoanInfo>();

            if (loan == null)
            {
                throw new ApplicationException(String.Format("Could not find loan {0}, please try again.", loannumber));
            }

            return loan;
        }

        public LPELoanInfo GetLastChanges(string loannumber)
        {
            LPELoanInfo loan = m_session.CreateSQLQuery("exec xGEM_LPE_GetLastChanges :loannumber ")
                .AddEntity(typeof(LPELoanInfo))
                .SetString("loannumber", loannumber)
                .UniqueResult<LPELoanInfo>();

            if (loan == null)
            {
                loan = GetLoanInfo(loannumber);
            }

            return loan;
        }


        public void UpdateReadyForDocs(string loannumber, bool value)
        {
            m_session.CreateSQLQuery("exec xGEM_LPE_UpdateReadyForDocs :loannumber, :newvalue ")
                .SetString("loannumber", loannumber)
                .SetBoolean("newvalue", value)
                .ExecuteUpdate();
        }

        public void UpdateReasonAndComment(string loannumber, int reasonId, string comment, string commentedby)
        {
            m_session.CreateSQLQuery("exec xGEM_LPE_SaveReason :loannumber, :reasonid, :comment, :commentedby ")
                .SetString("loannumber", loannumber)
                .SetInt32("reasonid", reasonId)
                .SetString("comment", comment)
                .SetString("commentedby", commentedby)
                .ExecuteUpdate();
        }

        public void InitialReviewComplete(string loannumber, string reviewedBy)
        {
            m_session.CreateSQLQuery("exec xGEM_LPE_InitialReviewComplete :loannumber, :reviewedBy ")
                .SetString("loannumber", loannumber)
                .SetString("reviewedBy", reviewedBy)
                .ExecuteUpdate();
        }

        public void AddHistory(string createdBy, string loanNumber, string borrower, string loanType, double loanAmount,
            double gemLoanFeeCharged, double loanOriginationFeeCharged, double loanOfficerPrice, double borrowerPaidDiscount,
            double lenderCredit, string ficoScore, string applicationDate, string lockedDate, string noOfBorrower,
            string programType, string transactionType, double finalNetPrice)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_LPE_AddHistory";
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.Parameters.AddWithValue("@loanNumber", loanNumber);
                    cmd.Parameters.AddWithValue("@borrower", borrower);
                    cmd.Parameters.AddWithValue("@loanType", loanType);
                    cmd.Parameters.AddWithValue("@loanAmount", loanAmount);
                    cmd.Parameters.AddWithValue("@gemLoanFeeCharged", gemLoanFeeCharged);
                    cmd.Parameters.AddWithValue("@loanOriginationFeeCharged", loanOriginationFeeCharged);
                    cmd.Parameters.AddWithValue("@loanOfficerPrice", loanOfficerPrice);
                    cmd.Parameters.AddWithValue("@borrowerPaidDiscount", borrowerPaidDiscount);
                    cmd.Parameters.AddWithValue("@lenderCredit", lenderCredit);
                    cmd.Parameters.AddWithValue("@ficoScore", ficoScore);
                    cmd.Parameters.AddWithValue("@applicationDate", applicationDate);
                    cmd.Parameters.AddWithValue("@lockedDate", lockedDate);
                    cmd.Parameters.AddWithValue("@noOfBorrower", noOfBorrower);
                    cmd.Parameters.AddWithValue("@programType", programType);
                    cmd.Parameters.AddWithValue("@transactionType", transactionType);
                    cmd.Parameters.AddWithValue("@finalNetPrice", finalNetPrice);

                    cmd.ExecuteNonQuery();
                }
            }
            /*
            m_session.CreateSQLQuery("exec xGEM_LPE_AddHistory :createdBy, :loanNumber, :borrower, :loanType, " +
                " :loanAmount, :gemLoanFeeCharged, :loanOriginationFeeCharged, :loanOfficerPrice, :borrowerPaidDiscount " +
                " :lenderCredit, :ficoScore, :applicationDate, :lockedDate, :noOfBorrower, :programType, :transactionType " +
                " :finalNetPrice ")
                .SetString("createdBy", createdBy)
                .SetString("loanNumber", loanNumber)
                .SetString("borrower", borrower)
                .SetString("loanType", loanType)
                .SetDouble("loanAmount", loanAmount)
                .SetDouble("gemLoanFeeCharged", gemLoanFeeCharged)
                .SetDouble("loanOriginationFeeCharged", loanOriginationFeeCharged)
                .SetDouble("loanOfficerPrice", loanOfficerPrice)
                .SetDouble("borrowerPaidDiscount", borrowerPaidDiscount)
                .SetDouble("lenderCredit", lenderCredit)
                .SetString("ficoScore", ficoScore)
                .SetString("applicationDate", applicationDate)
                .SetString("lockedDate", lockedDate)
                .SetString("noOfBorrower", noOfBorrower)
                .SetString("programType", programType)
                .SetString("transactionType", transactionType)
                .SetDouble("finalNetPrice", finalNetPrice)
                
                .ExecuteUpdate();
            */
        }
    }
}
