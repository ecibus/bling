using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Accounting;
using NHibernate;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.Accounting
{
    public interface IMCRDao
    {
        MCREnding GetLastMCREnding(string year, string quarter);
        Dictionary<string, string> GetSectionOne(string year, string quarter, string state);
        Dictionary<string, string> GetSectionTwo(string year, string quarter, string state);
        IList<MCRLOSItem> GetLOItem(string year, string quarter, string state);
    }

    public class MCRDao : AbstractDao<MCREnding, int>, IMCRDao
    {
        public MCRDao(ISession session) : base (session)
        {
        }

        public MCREnding GetLastMCREnding(string year, string quarter)
        {
            return m_session.CreateCriteria(typeof(MCREnding))
                .Add(Expression.Eq("Year", year))
                .Add(Expression.Eq("Quarter", quarter))
                .UniqueResult<MCREnding>();
        }


        public Dictionary<string, string> GetSectionOne(string year, string quarter, string state)
        {
            return GetData("xGEM_MCR_RMLA_SectionOne", year, quarter, state);
        }

        public Dictionary<string, string> GetSectionTwo(string year, string quarter, string state)
        {
            return GetData("xGEM_MCR_RMLA_SectionTwo", year, quarter, state);

        }

        public IList<MCRLOSItem> GetLOItem(string year, string quarter, string state)
        {
            return m_session.CreateSQLQuery("exec xGEM_MCR_RMLA_LO :year, :quarter, :state")
                .AddEntity(typeof(MCRLOSItem))
                .SetString("year", year)
                .SetString("quarter", quarter)
                .SetString("state", state)
                .List<MCRLOSItem>();
        }

        private Dictionary<string, string> GetData(string spname, string year, string quarter, string state)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();


            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spname;
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@quarter", quarter);
                    cmd.Parameters.AddWithValue("@state", state);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            for (int i = 0; i < colCount; i++)
                            {
                                data.Add(reader.GetName(i), reader.GetValue(i).ToString());
                                //data.Add("", reader[0].ToString());
                            }
                        }                        
                    }
                }
            }

            return data;
        }

    }
}
