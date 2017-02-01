using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Secondary
{
    public interface IHedgeGMACDao
    {
        IList<string> GetList(string start, string end);
    }

    public class HedgeGMACDao : AbstractDao<string, int>, IHedgeGMACDao
    {
        public HedgeGMACDao(ISession session)
            : base(session)
        {
        }

        public IList<string> GetList(string start, string end)
        {
            IList<string> data = new List<string>();

            StringBuilder line = new StringBuilder();
            StringBuilder header = new StringBuilder();

            bool firstLine = true;

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GMACHedgeLoan";
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            for (int i = 0; i < colCount; i++)
                            {
                                if (firstLine)
                                {
                                    header.AppendFormat("\"{0}\"", reader.GetName(i));
                                }

                                line.AppendFormat("\"{0}\"", reader.GetValue(i).ToString());
                                if (i < (colCount - 1))
                                {
                                    line.Append(",");
                                    header.Append(",");
                                }
                                else
                                {
                                    if (firstLine)
                                    {
                                        data.Add(header.ToString());
                                    }
                                    data.Add(line.ToString());
                                    line = new StringBuilder();
                                }
                            }

                            firstLine = false;
                        }

                    }
                }
            }

            return data;
        }
    }
}
