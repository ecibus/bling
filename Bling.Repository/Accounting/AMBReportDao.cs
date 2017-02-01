using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using NHibernate;
using System.Data;
using System.Configuration;

namespace Bling.Repository.Accounting
{

    public interface IAMBReportDao
    {
        List<List<string>> GetAppraisalBalanceData(string reportType, string from, string to);
        List<List<string>> GetData(string spName);
    }

    public class AMBReportDao : AbstractDao<string, int>, IAMBReportDao
    {
        private string ORADB;
        public AMBReportDao(ISession session)
            : base(session)
        {
            //ORADB = "Data Source=(DESCRIPTION="
            //        + "(ADDRESS=(PROTOCOL=TCP)(HOST=GEMAMB01)(PORT=1521))"
            //        + "(CONNECT_DATA=(SERVICE_NAME=Con5)));"
            //        + "User Id=reporting;Password=techies77";
            //        //+ "User Id=admin;Password=admin";

            ORADB = ConfigurationManager.AppSettings["AMBConnectionString"];

        }


        public List<List<string>> GetAppraisalBalanceData(string reportType, string from, string to)
        {
            List<List<string>> data = ReadAppraisalBalanceData(reportType, from, to);

            TruncateTable("dbo.xGEM_AMB_AppraisalBalance");

            bool isHeader = true;
            string header = "insert into dbo.xGEM_AMB_AppraisalBalance (";
            foreach (var row in data)
            {
                //int colCount = row.Count;
                string sql = "";

                if (isHeader)
                {
                    foreach (var col in row)
                    {
                        header += col.Trim() + ", "; // (counter++ < colCount ? ", " : "");
                    }
                    header += "fromdate, todate, datetype) values (";
                    isHeader = false;
                }
                else
                {
                    sql = header;
                    foreach (var col in row)
                    {
                        sql += (col.Trim() == "null" ? "null, " : "'" + col.Trim().Replace("'", "''") + "', "); // +(counter++ < colCount ? ", " : "");
                    }
                    sql += "'" + from + "', '" + to + "', '" + (reportType == "1" ? "Funded" : reportType == "2" ? "Application Date" : "Cancelled/Denied Date") + "'  )  ";
                }

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

            
            return null;
        }

        public List<List<string>> GetData(string spName)
        {
            List<List<string>> rows = new List<List<string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName; 

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

        private void TruncateTable(string tableName)
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

        private string GetAppraisalBalanceDateCriteria(string reportType)
        {
            string dateCriteria = "";

            switch (reportType)
            {
                case "1" :
                    dateCriteria = "fundingdate";
                    break;

                case "2":
                    dateCriteria = "dateinit";
                    break;

                case "3":
                    dateCriteria = "";
                    break;

            }
            //"select b.brnchname, l.loannumb, replace(l.borname, '|', '') borname, l.dateinit, " +
            //"      l.cancelleddate, l.date2, l.fundingdate, " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '20405' and rectype = 'D'  " +
            //"       ) , 0) " +
            //"       -  " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '20405' and rectype = 'C' " +
            //"       ), 0) PayableBalance, " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '10405' and amount < 0  " +
            //"       ) , 0) " +
            //"       -  " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '10405' and amount > 0  " +
            //"       ), 0) ReceivableBalance, " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb like '40105%' and rectype = 'D'  " +
            //"       ), 0)  " +
            //"       -  " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb like '40105%' and rectype = 'C'  " +
            //"       ), 0) FeeRevenueBalance, " +
            //"       nvl(( " + 
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb like '50015%' and rectype = 'D'  " +
            //"       ), 0)  " +
            //"       -  " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb like '50015%' and rectype = 'C'  " +
            //"       ), 0) ExpenseBalance " +
            //"    from bloan l" +
            //"      left join branch b on l.brnchcode = b.brnchcode" +
            //"   where " +
            //    (reportType == "1" ? "fundingdate between '" + from + "' and '" + to + "'"
            //        : reportType == "2" ? "dateinit between '" + from + "' and '" + to + "'"
            //        : "cancelleddate between '" + from + "' and '" + to + "' or date2 between '" + from + "' and '" + to + "'") +
            //"      and (" +
            //"       (nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '20405' and rectype = 'D'  " +
            //"       ) , 0) " +
            //"       -  " +
            //"       nvl(( " +
            //"         select sum(abs(amount))  " +
            //"         from ltran  " +
            //"         where loannumb = l.loannumb and acctnumb = '20405' and rectype = 'C' " +
            //"       ), 0) <> 0 " +
            //"         )" +
            //"         or                " +
            //"         (" +
            //"            nvl(( " +
            //"              select sum(abs(amount))  " +
            //"              from ltran  " +
            //"              where loannumb = l.loannumb and acctnumb like '40105%' and rectype = 'D'  " +
            //"            ), 0)  " +
            //"            -  " +
            //"            nvl(( " +
            //"              select sum(abs(amount))  " +
            //"              from ltran  " +
            //"              where loannumb = l.loannumb and acctnumb like '40105%' and rectype = 'C'  " +
            //"            ), 0) " +
            //"            >" +
            //"            nvl(( " +
            //"              select sum(abs(amount))  " +
            //"              from ltran  " +
            //"              where loannumb = l.loannumb and acctnumb like '50015%' and rectype = 'D'  " +
            //"            ), 0)  " +
            //"            -  " +
            //"            nvl(( " +
            //"              select sum(abs(amount))  " +
            //"              from ltran  " +
            //"              where loannumb = l.loannumb and acctnumb like '50015%' and rectype = 'C'  " +
            //"            ), 0)  " +
            //"         )" +
            //"     )" +
            //"    order by loannumb asc";

            return dateCriteria;
        }

        private List<List<string>> ReadAppraisalBalanceData(string reportType, string from, string to)
        {
            List<List<string>> rows = new List<List<string>>();

            OracleConnection conn = new OracleConnection(ORADB);

            conn.Open();

            string sql =
                "      select b.brnchname, l.loannumb, " +
                "        replace(l.borname, '|', '') borname, l.dateinit,       " +
                "        l.cancelleddate, l.date2, l.fundingdate,        " +
                "        t.jrnseqnumb, t.ltranseqnumb, t.doctype, t.rectype," +
                "        t.acctnumb, t.amount, t.ldate, t.amblxcode, t.time_stamp, t.trx" +
                "      from bloan l      " +
                "        left join ltran t on l.loannumb = t.loannumb" +
                "        left join branch b on l.brnchcode = b.brnchcode" +
                "      where " +
                "        (t.acctnumb = '20405' or t.acctnumb = '10405' or t.acctnumb like '40105%' or t.acctnumb like '50015%' or t.acctnumb like '50008-%')  " +
                "        and (" +
                    (reportType == "1" ? "fundingdate between '" + from + "' and '" + to + "'"
                        : reportType == "2" ? "dateinit between '" + from + "' and '" + to + "'"
                        : "cancelleddate between '" + from + "' and '" + to + "' or date2 between '" + from + "' and '" + to + "'") +
                "        )";


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

    }
}
