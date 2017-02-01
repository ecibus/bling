using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Funding
{
    public interface IJPMorganDao
    {
        List<List<string>> GetData(string start, string end, string batchno);
    }

    public class JPMorganDao : AbstractDao<string, int>, IJPMorganDao
    {
        public JPMorganDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(JPMorganDao));
        }

        public List<List<string>> GetData(string start, string end, string batchno)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_JPMorgan";
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@batchno", batchno);

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
