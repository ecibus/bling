using System;
using Bling.Domain;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository
{
    public interface IAppraiserDao: IDao<Appraiser, string>
    {
        IList<Appraiser> GetAppraiserForBranchAndCounty(string branchno, string county);
        string GetCountyForLoan(string loannumber);
    }

    public class AppraiserDao : AbstractDao<Appraiser, string>, IAppraiserDao
    {
        public AppraiserDao(ISession session) 
            : base (session)
        {            
        }

        public string GetCountyForLoan(string loannumber)
        {
            string county = "loanhasnocounty";

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetCountyFromLoanNumber";
                    cmd.Parameters.AddWithValue("@LoanNumber", loannumber);

                    county = (string) cmd.ExecuteScalar();
                    
                }
            }
            return county;
        }

        public IList<Appraiser> GetAppraiserForBranchAndCounty(string branchno, string county)
        {
            return m_session.CreateCriteria(typeof(Appraiser))
                .Add(Expression.Eq("Exclude", false))
                .Add(Expression.Like("Region", String.Format("%{0}%", branchno)))
                .Add(Expression.Like("OtherCounty", String.Format("%{0}%", county)))                
                .Add(Expression.In("Status", new List<string> { "approved", "expired" }))
                .AddOrder(Order.Asc("FirstName"))
                .List<Appraiser>();                
        }

    }
}
