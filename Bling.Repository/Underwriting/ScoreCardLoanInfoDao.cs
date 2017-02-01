using System;
using Bling.Domain.Underwriting;
using NHibernate;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Underwriting
{
    public interface IScoreCardLoanInfoDao
    {
        ScoreCardLoanInfo GetByLoanNumber(string loanNumber);
        void MakeALoanPerfect(string fileId, string createdBy);
        void MakeALoanNotPerfect(string fileId);
        void CategoryHasNoFindings(string fileId, int groupId, string createdBy);
        void CategoryHasFindings(string fileId, int groupId);
    }

    public class ScoreCardLoanInfoDao : AbstractDao<ScoreCardLoanInfo, string>, IScoreCardLoanInfoDao
    {
        public ScoreCardLoanInfoDao(ISession session) : base(session)
        {
            
        }

        public ScoreCardLoanInfo GetByLoanNumber(string loanNumber)
        {
            string sql =
                "select " +
                   "g.loan_num 'LoanNumber', " +
                   "g.borrow_fn + ' ' + borrow_ln 'Borrower', " +
                   "u.Processor, " +
                   "rtrim(lo.last_name) + ', ' + rtrim(lo.first_name) 'LoanOfficer', " +
                   "g.file_id, " +
                   "isnull(ui2.first_name, '') + ' ' + isnull(ui2.last_name, '') 'Underwriter', " +
                   "case when p.prog_name = 'fha203k' then 1 else 0 end 'Is203K', " +
                   "case when scp.file_id is null then 0 else 1 end 'IsPerfect' " +
                "from " +
                   "gen g " +
                   "left join und u on g.file_id = u.file_id " +
                   "left join userinfo ui2 on u.underwrtr = ui2.employ_id " +
                   "left join userinfo lo on g.lo_rep = lo.employ_id " +
                   "left join programs p on g.programs_id = p.programs_id " +
                   "left join xGEM_ScoreCardPerfect scp on g.file_id = scp.file_id " +
                "where " +
                   "g.loan_num = :loanNumber ";

            ScoreCardLoanInfo loanInfo = m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(ScoreCardLoanInfo))
                .SetString("loanNumber", loanNumber)
                .UniqueResult<ScoreCardLoanInfo>();

            if (loanInfo != null)
            {
                sql =
                    "select ScoreId from xGEM_ScoreCard  " +
                    "where file_id = :fileid ";

                loanInfo.ScoreIds = m_session.CreateSQLQuery(sql)
                    .SetString("fileid", loanInfo.FileId)
                    .List<int>();
            }

            return loanInfo;
        }


        public void MakeALoanPerfect(string fileId, string createdBy)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ScoreCardIsPerfect";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                ExecuteNonQuery(cmd);
            }
        }

        public void MakeALoanNotPerfect(string fileId)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ScoreCardIsNotPerfect";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                ExecuteNonQuery(cmd);
            }
        }

        public void CategoryHasNoFindings(string fileId, int groupId, string createdBy)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ScoreCardGroupHasNoFindings";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                ExecuteNonQuery(cmd);
            }
        }
  
        public void CategoryHasFindings(string fileId, int groupId)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_ScoreCardGroupHasFindings";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                ExecuteNonQuery(cmd);
            }
        }
    }
}
