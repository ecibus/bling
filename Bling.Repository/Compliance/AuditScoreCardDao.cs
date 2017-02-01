using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;

namespace Bling.Repository.Compliance
{
    public interface IAuditScoreCardDao : IDao<AuditScoreCardLoanInfo, string>
    {
        AuditScoreCardLoanInfo GetLoanInfo(string loanNumber);
        void UpdateCustomData(string fileId, string field, string value);
        void UpdateSubmittedDate(string fileId, string oldValue, string newValue);
        void PushNotesInDataTrac(string fileId, string actorId);
    }

    public class AuditScoreCardDao : AbstractDao<AuditScoreCardLoanInfo, string>, IAuditScoreCardDao
    {
        public AuditScoreCardDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(AuditScoreCardDao));
        }


        public AuditScoreCardLoanInfo GetLoanInfo(string loanNumber)
        {
            var loan = m_session.CreateSQLQuery("exec xGEM_AuditScoreCard_GetLoan :loanNumber")
                .AddEntity(typeof(AuditScoreCardLoanInfo))
                .SetString("loanNumber", loanNumber)
                .UniqueResult<AuditScoreCardLoanInfo>();

            if (loan != null)
            {
                string sql =
                    "select Score_Id from xGEM_AuditScoreCardScore  " +
                    "where file_id = :fileid ";

                loan.ScoreIds = m_session.CreateSQLQuery(sql)
                    .SetString("fileid", loan.FileId)
                    .List<int>();
            }

            return loan;
        }

        public void UpdateCustomData(string fileId, string field, string value)
        {
            m_session.CreateSQLQuery("exec xGEM_CustomData_Update :fileId, :field, :value")
                .AddEntity(typeof(AuditScoreCardLoanInfo))
                .SetString("fileId", fileId)
                .SetString("field", field)
                .SetString("value", value)
                .ExecuteUpdate();
        }

        public void UpdateSubmittedDate(string fileId, string oldSubmittedDate, string newSubmittedDate)
        {
            m_session.CreateSQLQuery("exec xGEM_AuditScoreCard_UpdateSubmittedDate :file_Id, :oldSubmittedDate, :newSubmittedDate")
                .SetString("file_Id", fileId)
                .SetString("oldSubmittedDate", oldSubmittedDate)
                .SetString("newSubmittedDate", newSubmittedDate)
                .ExecuteUpdate();
        }

        public void PushNotesInDataTrac(string fileId, string actorId)
        {
            m_session.CreateSQLQuery("exec xGEM_AuditScoreCard_PushToDataTrac :fileId, :actorId")
                .AddEntity(typeof(AuditScoreCardLoanInfo))
                .SetString("fileId", fileId)
                .SetString("actorId", actorId)
                .ExecuteUpdate();
        }

    }
}
