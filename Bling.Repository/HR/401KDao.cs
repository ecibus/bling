using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface I401KDao
    {
        List<List<string>> GetData(string start, string end, string isWeekly);
    }

    public class _401KDao : AbstractDao<string, int>, I401KDao
    {
        public _401KDao(ISession session) : base (session)
        {
        }

        public List<List<string>> GetData(string start, string end, string isWeekly)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_401K";
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    cmd.Parameters.AddWithValue("@isWeekly", isWeekly == "w" ? 1 : 0);

                    bool firstRow = true;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount - 1;
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
