using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using NHibernate;

namespace Bling.Repository.Secondary
{
    public interface ITradeDao
    {
        List<List<string>> GetTradeFile(string from, string to, string dateForRange, string sortBy);
        List<List<string>> GetTradeFile2(string fromSettleDate, string toSettleDate, string fromPairOffDate, string toPairOffDate, string status, string sortBy);
    }

    public class TradeDao : AbstractDao<string, int>, ITradeDao
    {
        public TradeDao(ISession session)
            : base(session)
        {
        }

        public List<List<string>> GetTradeFile2(string fromSettleDate, string toSettleDate, string fromPairOffDate, string toPairOffDate, string status, string sortBy)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_TradeFile2";
                    cmd.Parameters.AddWithValue("@startSettleDate", fromSettleDate);
                    cmd.Parameters.AddWithValue("@endSettleDate", toSettleDate);
                    cmd.Parameters.AddWithValue("@startPairOffDate", fromPairOffDate);
                    cmd.Parameters.AddWithValue("@endPairOffDate", toPairOffDate);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@sortBy", sortBy);

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

        public List<List<string>> GetTradeFile(string from, string to, string dateForRange, string sortBy)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_TradeFile";
                    cmd.Parameters.AddWithValue("@start", from);
                    cmd.Parameters.AddWithValue("@end", to);
                    cmd.Parameters.AddWithValue("@dateRange", dateForRange);
                    cmd.Parameters.AddWithValue("@sortBy", sortBy);

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
