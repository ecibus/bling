﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using NHibernate;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Accounting
{
    public interface IAMBExportDao
    {
        List<List<string>>  GetData(string from, string to);
    }

    public class AMBExportDao : AbstractDao<string, int>, IAMBExportDao
    {
        public AMBExportDao(ISession session)
            : base(session)
        {
        }

        public List<List<string>>  GetData(string from, string to)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_AMBExport";
                    cmd.Parameters.AddWithValue("@start", from);
                    cmd.Parameters.AddWithValue("@end", to);

                    //return cmd.ExecuteReader();
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
                                //data.Add(reader.GetName(i), reader.GetValue(i).ToString());
                                //data.Add("", reader[0].ToString());
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
