using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;
using System.Data.SqlClient;
using System.Data;
using Oracle.DataAccess.Client;

namespace Bling.Repository.Accounting
{
    public interface IReserveRequirementDao : IDao<ReserveRequirement, int>
    {
        void Delete(ReserveRequirement rr);
        void Update(string id, string costCenter, string reserveMinimum, string fixedReserve, string recipient);
        List<ReserveRequirement> GetByIds(string ids);
        void RefreshPLRecap(string branch, string month, string year);
        List<List<string>> ReadGL(string sql);
        void InsertData(string tableName, List<List<string>> data);
        void TruncateTable(string tableName);
        void ConsolidateGL();
        void PerformBulkOperation(DataTable dt);
    }

    public class ReserveRequirementDao : AbstractDao<ReserveRequirement, int>, IReserveRequirementDao
    {
        public ReserveRequirementDao(ISession session) 
            : base(session)
        {
        }

        public void Delete(ReserveRequirement rr)
        {
            m_session.Delete(rr);
        }

        public void InsertData(string tableName, List<List<string>> data)
        {
            bool isHeader = true;



            string header = "insert into dbo." + tableName + " ("; //xGEM_AMB_JRN
            foreach (var row in data)
            {
                int colCount = row.Count;
                int counter = 1;
                string sql = "";

                if (isHeader)
                {
                    foreach (var col in row)
                    {
                        header += col.Trim() + (counter++ < colCount ? ", " : "");
                    }
                    header += ") values (";
                    isHeader = false;
                }
                else
                {
                    sql = header;
                    foreach (var col in row)
                    {
                        sql += (col.Trim() == "null" ?
                            "null" :
                            String.Format("'{0}'", col.Trim().Replace("'", "''"))
                            );

                        sql += counter++ < colCount ? ", " : "";
                    }
                    sql += ")";
                    //sql += "'" + from + "', '" + to + "', '" + (reportType == "1" ? "Funded" : reportType == "2" ? "Application Date" : "Cancelled/Denied Date") + "'  )  ";
                }

                //Console.WriteLine(sql);
                if (sql.Length != 0)
                {
                    using (var cn = new SqlConnection(DMDDataConnectionString))
                    {
                        using (var cmd = new SqlCommand { Connection = cn })
                        {
                            cn.Open();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }



        }

        public void TruncateTable(string tableName)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "truncate table " + tableName;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ConsolidateGL()
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_AMB_ConsolidateGL_Debit";
                    cmd.ExecuteNonQuery();
                }
            }

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_AMB_ConsolidateGL_Credit";
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<ReserveRequirement> GetByIds(string ids)
        {
            string sql = "Select Id, CostCenter, ReserveMinimum, FixedReserve, Recipient from dbo.xGEM_ReserveRequirement " +
                " where id in (" + ids.Substring(0, ids.Length - 1) + ") ";
            var list = m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(ReserveRequirement))
                .List<ReserveRequirement>().ToList();

            return list;
        }

        public void Update(string id, string costCenter, string reserveMinimum, string fixedReserve, string recipient)
        {
            string sql = "Update top (1) dbo.xGEM_ReserveRequirement " +
                         "Set CostCenter = '" + costCenter + "', " +
                         "    recipient = '" + recipient + "', " +
                         "    reserveMinimum = " + reserveMinimum + ", " +
                         "    fixedReserve = " + fixedReserve + " " +
                         "Where id = " + id;

            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                ExecuteNonQuery(cmd);
            }
        }

        public void RefreshPLRecap(string branch, string month, string year)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_PLRecapB_Refresh";
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@branch", branch);
                    cmd.Parameters.AddWithValue("@m1", month);
                    cmd.Parameters.AddWithValue("@y1", year);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<List<string>> ReadGL(string sql)
        {
            List<List<string>> rows = new List<List<string>>();

            OracleConnection conn = new OracleConnection(AMBConnectionString);

            conn.Open();

            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                int colCount = reader.FieldCount;
                List<string> header = new List<string>();
                bool firstRow = true;

                while (reader.Read())
                {
                    List<string> row = new List<string>();
                    for (int i = 0; i < colCount; i++)
                    {
                        row.Add(reader.IsDBNull(i) ? "null" : reader.GetValue(i).ToString());
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
                    rows.Add(row);
                }
            }

            return rows;

        }

        public void PerformBulkOperation(DataTable dt)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DMDDataConnectionString))
                {

                    con.Open();
                    using (SqlBulkCopy bc = new SqlBulkCopy(con))
                    {
                        bc.BulkCopyTimeout = 3600;
                        bc.DestinationTableName = "xGEM_AMB_LTRAN";
                        SqlBulkCopyColumnMapping jrnseqnumb = new SqlBulkCopyColumnMapping(0, 2);
                        bc.ColumnMappings.Add(jrnseqnumb);
                        SqlBulkCopyColumnMapping ltranseqnumb = new SqlBulkCopyColumnMapping(1, 3);
                        bc.ColumnMappings.Add(ltranseqnumb);
                        SqlBulkCopyColumnMapping doctype = new SqlBulkCopyColumnMapping(2, 4);
                        bc.ColumnMappings.Add(doctype);
                        SqlBulkCopyColumnMapping rectype = new SqlBulkCopyColumnMapping(3, 5);
                        bc.ColumnMappings.Add(rectype);
                        SqlBulkCopyColumnMapping loannumb = new SqlBulkCopyColumnMapping(4, 6);
                        bc.ColumnMappings.Add(loannumb);
                        SqlBulkCopyColumnMapping acctnumb = new SqlBulkCopyColumnMapping(5, 7);
                        bc.ColumnMappings.Add(acctnumb);
                        SqlBulkCopyColumnMapping amount = new SqlBulkCopyColumnMapping(6, 8);
                        bc.ColumnMappings.Add(amount);
                        SqlBulkCopyColumnMapping ldate = new SqlBulkCopyColumnMapping(7, 9);
                        bc.ColumnMappings.Add(ldate);
                        SqlBulkCopyColumnMapping amblxcode = new SqlBulkCopyColumnMapping(8, 10);
                        bc.ColumnMappings.Add(amblxcode);
                        SqlBulkCopyColumnMapping time_stamp = new SqlBulkCopyColumnMapping(9, 11);
                        bc.ColumnMappings.Add(time_stamp);
                        SqlBulkCopyColumnMapping last_user = new SqlBulkCopyColumnMapping(10, 12);
                        bc.ColumnMappings.Add(last_user);
                        SqlBulkCopyColumnMapping loanofficer = new SqlBulkCopyColumnMapping(11, 13);
                        bc.ColumnMappings.Add(loanofficer);
                        bc.WriteToServer(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message + " - PerformBulkOperation";
            }
        }
    }
}
