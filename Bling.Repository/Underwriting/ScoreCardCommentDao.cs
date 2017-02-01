using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.Underwriting;
using NHibernate;

namespace Bling.Repository.Underwriting
{
    public interface IScoreCardCommentDao : IDao<ScoreCardComment, int>
    {
        void SaveComment(ScoreCardComment comment);
        IDictionary<string, string> GetGroupComment(string fileId);        
    }

    public class ScoreCardCommentDao : AbstractDao<ScoreCardComment, int> , IScoreCardCommentDao
    {
        public ScoreCardCommentDao(ISession session)
            : base(session)
        {
            
        }

        public void SaveComment(ScoreCardComment comment)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_SaveScoreCardComment";
                cmd.Parameters.AddWithValue("@fileid", comment.FileId);
                cmd.Parameters.AddWithValue("@groupid", comment.GroupId);
                cmd.Parameters.AddWithValue("@comment", comment.Comment);
                cmd.Parameters.AddWithValue("@createdby", comment.CreatedBy);

                ExecuteNonQuery(cmd);
            }
        }

        public IDictionary<string, string> GetGroupComment(string fileId)        
        {
            IDictionary<string, string> scores = new Dictionary<string, string>();

            DataTable dt;

            using (var cmd = new SqlCommand ())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_GetGroupComment";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                dt = GetDataTable(cmd);
            }

            foreach (DataRow row in dt.Rows)
                scores.Add(row["GroupId"].ToString(), row["Comment"].ToString().Replace("\n", "\\n"));
            

            return scores;
        }
    }
}
