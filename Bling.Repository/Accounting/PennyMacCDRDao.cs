using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Accounting
{
    public interface IPennyMacCDRDao
    {
        List<List<string>> GetData(string sql);
    }

    public class PennyMacCDRDao : AbstractDao<string, int>, IPennyMacCDRDao
    {
        public PennyMacCDRDao(ISession session)
            : base(session)
        {
        }

        public List<List<string>> GetData(string sql)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
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
