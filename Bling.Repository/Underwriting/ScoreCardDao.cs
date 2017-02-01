using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.Extension;
using Bling.Domain.Underwriting;
using NHibernate;

namespace Bling.Repository.Underwriting
{
    public interface IScoreCardDao 
    {
        void SaveScore(ScoreCard score);
        void RemoveByFileIdAndScoreId(string fileId, int scoreId);
        IDictionary<string, double> GetGroupScore(string fileId);
        IDictionary<string, double> GetOtherScore(string fileId);
        IDictionary<string, double> GetNoFindings(string fileId);
    }

    public class ScoreCardDao : AbstractDao<ScoreCard, int>, IScoreCardDao
    {
        public ScoreCardDao(ISession session)
            : base(session)
        {            
        }

        public void SaveScore(ScoreCard score)
        {
            using (var cmd = new SqlCommand ())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_SaveScoreCard";
                cmd.Parameters.AddWithValue("@fileid", score.FileId);
                cmd.Parameters.AddWithValue("@scoreid", score.ScoreId);
                cmd.Parameters.AddWithValue("@score", score.Score);
                cmd.Parameters.AddWithValue("@createdby", score.CreatedBy);
                ExecuteNonQuery(cmd);
            }
        }

        public void RemoveByFileIdAndScoreId(string fileId, int scoreId)
        {
            using (var cmd = new SqlCommand ())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_RemoveScoreCard";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                cmd.Parameters.AddWithValue("@scoreid", scoreId);

                ExecuteNonQuery(cmd);
            }
        }

        public IDictionary<string, double> GetGroupScore(string fileId)
        {
            return RunStoredProcedure("xGEM_GetGroupScore", fileId);
        }
        
        public IDictionary<string, double> GetOtherScore(string fileId)
        {
            return RunStoredProcedure("xGEM_GetOtherScore", fileId);
        }

        public IDictionary<string, double> GetNoFindings(string fileId)
        {
            return RunStoredProcedure("xGEM_ScoreCardGetNoFindings", fileId);
        }

        private IDictionary<string, double> RunStoredProcedure(string storedProcedure, string fileId)
        {
            IDictionary<string, double> scores = new Dictionary<string, double>();

            DataTable dt;

            using (var cmd = new SqlCommand ())
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
