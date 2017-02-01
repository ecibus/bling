using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;

namespace Bling.Repository.Accounting
{
    public interface IWarehouseAdvanceRecapDao
    {
        List<List<string>> GetData();
    }

    public class WarehouseAdvanceRecapDao : IWarehouseAdvanceRecapDao
    {
        public WarehouseAdvanceRecapDao()
        {
        }

        public List<List<string>> GetData()
        {
            string sql =
                " select " + //l.acctnumb, b.status, " +
                "  trim(b.warehouse) as \"Warehouse Bank\", trim(b.product) as \"Product Type\", " +
                "  trim(b.loannumb )as \"Loan Number\", b.loanamt as \"Loan Amount\", " +
                "  b.intrate as \"Interest Rate\", l.amount * -1 as \"Warehouse Advance\" " +
                " from bloan b " +
                "  inner join ltran l on b.loannumb = l.loannumb  " +
                "     and l.acctnumb > '20505' and l.acctnumb < '20595' and l.rectype = 'C' " +
                " where " +
                "  b.status = 'FUNDED' or b.status = 'SHIPPED' " +
                " order by b.warehouse, b.product -- b.loannumb ";

                //"select CAST(l.jrnseqnumb AS varchar(2000)), CAST(l.ltranseqnumb AS varchar(2000)), " +
                //"CAST(l.doctype AS varchar(2000)), CAST(l.rectype AS varchar(2000)), " +
                //"CAST(l.loannumb AS varchar(2000)), CAST(l.acctnumb AS varchar(2000)), " +
                //"CAST(l.amount AS varchar(2000)), l.ldate , " +
                //"CAST(l.amblxcode AS varchar(2000)), l.time_stamp, CAST(l.last_user AS varchar(2000)), " +
                //"CAST(b.loanofficer AS varchar(2000)) from ltran l left join bloan b on l.loannumb = b.loannumb";

            List<List<string>> rows = new List<List<string>>();

            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["AMBConnectionString"]);

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

    }
}
