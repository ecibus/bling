using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Processing
{
    public interface IQCExportDao
    {
        List<List<string>> GetData(string spName, string from, string to, int includeDataTrac, int includeByte, string loans, string dateType);
    }

    public class QCExportDao : AbstractDao<string, int>, IQCExportDao
    {
        public QCExportDao(ISession session)
            : base(session)
        {
        }

        public List<List<string>> GetData(string spName, string from, string to, int includeDataTrac, int includeByte, string loans, string dateType)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    cmd.Parameters.AddWithValue("@start", from);
                    cmd.Parameters.AddWithValue("@end", to);
                    cmd.Parameters.AddWithValue("@includeDataTrac", includeDataTrac);
                    cmd.Parameters.AddWithValue("@includeByte", includeByte);
                    cmd.Parameters.AddWithValue("@loans", loans);
                    cmd.Parameters.AddWithValue("@dateType", dateType);

                    bool firstRow = true;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            List<string> column = new List<string>();
                            List<string> header = new List<string>();

                            for (int i = 0; i < colCount; i++)
                            {
                                column.Add(reader.GetValue(i).ToString());
                                if (firstRow)
                                {
                                    header.Add(reader.GetName(i));
                                }
                            }
                            if (firstRow)
                            {
                                rows.Add(header);
                                firstRow = false;
                            }
                            rows.Add(column);
                        }

                    }
                    return rows;
                }
            }
        }
    }
}
