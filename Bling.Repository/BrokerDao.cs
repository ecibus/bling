using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;
namespace Bling.Repository
{
    public interface IBrokerDao
    {
        List<Broker> GetActiveBroker();
        List<string> GetBranchManagerEmailForCommission(string branchNo);
        List<string> GetActiveBranch();
    }

    public class BrokerDao : AbstractDao<Broker, string> , IBrokerDao
    {
        public BrokerDao(ISession session)
            : base(session)
        {            
        }

        public List<Broker> GetActiveBroker()
        {
            return m_session.CreateCriteria(typeof(Broker))
                .Add(Expression.Eq("Status", "approved"))
                .Add(Expression.Eq("InCorp", false))
                .Add(Expression.Eq("BranchType", "r"))
                .Add(Expression.Not(Expression.Eq("CostCenter", "")))
                .List<Broker>().ToList();
        }

        public List<string> GetBranchManagerEmailForCommission(string branchNo)
        {
            List<string> list = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = String.Format("select email from dbo.xGEM_LOCommissionEMail where BranchNo = '{0}' ", branchNo);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        string email = reader["email"].ToString();

                        if (email != String.Empty)
                        {
                            foreach(string e in email.Split(';').ToList())
                            {
                                if (!(String.IsNullOrEmpty(e.Trim())))
                                {
                                    list.Add(e.Trim());
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }
            return list;
        }

        public List<string> GetActiveBranch()
        {
            List<string> branches = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetActiveBranch";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            for (int i = 0; i < colCount; i++)
                            {
                                branches.Add(reader.GetValue(i).ToString());
                            }
                        }

                    }
                    return branches;
                }
            }

        }

    }
}
