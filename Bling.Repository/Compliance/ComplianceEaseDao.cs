using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using System.Data.SqlClient;
using System.Data;
using NHibernate;
using log4net;

namespace Bling.Repository.Compliance
{
    public interface IComplianceEaseDao : IDao<ComplianceEase, int>
    {
        ComplianceEase GetData(string start, string end, string loans);
        List<List<string>> GetData2(string start, string end);
    }

    public class ComplianceEaseDao : AbstractDao<ComplianceEase, int>, IComplianceEaseDao
    {
        public ComplianceEaseDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(ComplianceEaseDao));  
        }

        public ComplianceEase GetData(string start, string end, string loans)
        {
            IList<IList<string>> list = new List<IList<string>>();

            ComplianceEase ce = new ComplianceEase();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_CEGetData";
                    cmd.Parameters.AddWithValue("start", start);
                    cmd.Parameters.AddWithValue("end", end);
                    cmd.Parameters.AddWithValue("loans", loans);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IList<string> row = new List<string>();
                        foreach (var h in ce.Header)
                        {
                            row.Add(reader[h].ToString());
                        }

                        foreach (var f in ce.Fees)
                        {
                            foreach (var fa in f.FeeAmount)
                            {
                                var amount = reader[fa.FieldName].ToString();
                                var pfc = reader[fa.FieldName + "PFC"].ToString();

                                fa.Amount = amount;
                                fa.PFC = pfc;
                            }

                            row.Add(f.ToString());
                        }

                        list.Add(row);
                    }
                    reader.Close();
                }
            }

            ce.Data = list;

            return ce;
        }

        public List<List<string>> GetData2(string start, string end)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_CEGetData2";
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);

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
