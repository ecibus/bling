using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using NHibernate.Criterion;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.Extension;

namespace Bling.Repository.Compliance
{
    public interface IAuditScoreCardScoreDao : IDao<AuditScoreCardScore, int>
    {
        void RemoveByFileIdAndScoreId(string fileId, int scoreId);
        void SaveScore(AuditScoreCardScore score);
        IDictionary<string, double> GetGroupScore(string fileId);
        void CategoryHasNoFindings(string fileId, int groupId, string createdBy);
        void CategoryHasFindings(string fileId, int groupId);
        IDictionary<string, double> GetNoFindings(string fileId);
        IDictionary<string, double> GetOtherScore(string fileId);
    }

    public class AuditScoreCardScoreDao : AbstractDao<AuditScoreCardScore, int>, IAuditScoreCardScoreDao
    {
        public AuditScoreCardScoreDao(ISession session)
            : base(session)
        {            
        }

        public void CategoryHasNoFindings(string fileId, int groupId, string createdBy)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_GroupHasNoFindings";
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
                cmd.CommandText = "xGEM_AuditScoreCard_GroupHasFindings";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                ExecuteNonQuery(cmd);
            }
        }

        public void SaveScore(AuditScoreCardScore score)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_SaveScore";
                cmd.Parameters.AddWithValue("@fileid", score.FileId);
                cmd.Parameters.AddWithValue("@scoreid", score.ScoreId);
                cmd.Parameters.AddWithValue("@score", score.Score);
                cmd.Parameters.AddWithValue("@createdby", score.CreatedBy);
                ExecuteNonQuery(cmd);
            }
        }

        public void RemoveByFileIdAndScoreId(string fileId, int scoreId)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_RemoveScore";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@scoreid", scoreId);

                ExecuteNonQuery(cmd);
            }
        }

        public IDictionary<string, double> GetNoFindings(string fileId)
        {
            return RunStoredProcedure("xGEM_AuditScoreCard_GetNoFindings", fileId);
        }

        public IDictionary<string, double> GetGroupScore(string fileId)
        {
            return RunStoredProcedure("xGEM_AuditScoreCard_GetGroupScore", fileId);
        }

        public IDictionary<string, double> GetOtherScore(string fileId)
        {
            return RunStoredProcedure("xGEM_AuditScoreCard_GetOtherScore", fileId);
        }

        private IDictionary<string, double> RunStoredProcedure(string storedProcedure, string fileId)
        {
            IDictionary<string, double> scores = new Dictionary<string, double>();

            DataTable dt;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.AddWithValue("@fileid", fileId);

                dt = GetDataTable(cmd);
            }

            foreach (DataRow row in dt.Rows)
                scores.Add(row["Key"].ToString(), row["Value"].ToString().ToDouble());

            return scores;
        }
    }


}
