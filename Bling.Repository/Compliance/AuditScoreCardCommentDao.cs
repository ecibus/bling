using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Compliance
{
    public interface IAuditScoreCardCommentDao : IDao<AuditScoreCardComment, int>
    {
        void SaveComment(AuditScoreCardComment comment);
        IDictionary<string, string> GetComment(string fileId);
    }

    public class AuditScoreCardCommentDao : AbstractDao<AuditScoreCardComment, int>, IAuditScoreCardCommentDao
    {
        public AuditScoreCardCommentDao(ISession session)
            : base(session)
        {
        }

        public void SaveComment(AuditScoreCardComment comment)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_SaveComment";
                cmd.Parameters.AddWithValue("@fileid", comment.FileId);
                cmd.Parameters.AddWithValue("@itemid", comment.ItemId);
                cmd.Parameters.AddWithValue("@comment", comment.Comment);
                cmd.Parameters.AddWithValue("@createdby", comment.CreatedBy);

                ExecuteNonQuery(cmd);
            }
        }

        public IDictionary<string, string> GetComment(string fileId)
        {
            IDictionary<string, string> scores = new Dictionary<string, string>();

            DataTable dt;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_GetComment";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                dt = GetDataTable(cmd);
            }

            foreach (DataRow row in dt.Rows)
                scores.Add(row["ItemId"].ToString(), row["Comment"].ToString().Replace("\n", "\\n"));


            return scores;
        }
    }
}
