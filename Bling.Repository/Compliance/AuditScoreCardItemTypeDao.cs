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
    public interface IAuditScoreCardItemTypeDao : IDao<AuditScoreCardItemType, int>
    {
        void SaveItemType(AuditScoreCardItemType itemType);
        IDictionary<string, string> GetItemType(string fileId);
    }

    public class AuditScoreCardItemTypeDao : AbstractDao<AuditScoreCardItemType, int>, IAuditScoreCardItemTypeDao
    {
        public AuditScoreCardItemTypeDao(ISession session)
            : base(session)
        {
        }

        public void SaveItemType(AuditScoreCardItemType itemType)
        {
            if (itemType.ItemType == "undefined")
                return;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_SaveItemType";
                cmd.Parameters.AddWithValue("@fileid", itemType.FileId);
                cmd.Parameters.AddWithValue("@itemid", itemType.ItemId);
                cmd.Parameters.AddWithValue("@itemtype", itemType.ItemType);
                cmd.Parameters.AddWithValue("@createdby", itemType.CreatedBy);

                ExecuteNonQuery(cmd);
            }
        }

        public IDictionary<string, string> GetItemType(string fileId)
        {
            IDictionary<string, string> scores = new Dictionary<string, string>();

            DataTable dt;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "xGEM_AuditScoreCard_GetItemType";
                cmd.Parameters.AddWithValue("@fileid", fileId);
                dt = GetDataTable(cmd);
            }

            foreach (DataRow row in dt.Rows)
                scores.Add(row["ItemId"].ToString(), row["ItemType"].ToString().Replace("\n", "\\n"));


            return scores;
        }
    }
}
