using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Secondary;
using NHibernate;
using System.Data;
using System.Data.SqlClient;

namespace Bling.Repository.Secondary
{
    public interface IProgramCodeByInvestorDao
    {
        List<ProgramCodeByInvestor> GetProgramCodeByInvestor(int type);
        void ShowHideByInvestor(string investor, string hide, string updatedby);
    }

    public class ProgramCodeByInvestorDao : AbstractDao<ProgramCodeByInvestor, string>, IProgramCodeByInvestorDao
    {
        public ProgramCodeByInvestorDao(ISession session)
            : base(session)
        {            
        }

        public List<ProgramCodeByInvestor> GetProgramCodeByInvestor(int t)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetLoanSolutionInvestor :type")
                .AddEntity(typeof(ProgramCodeByInvestor))
                .SetInt32("type", t)                
                .List<ProgramCodeByInvestor>()
                .ToList();                
        }

        public void ShowHideByInvestor(string investor, string hide, string updatedby)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_ShowHideProgramCodeByInvestor";
                    cmd.Parameters.AddWithValue("@investor", investor);
                    cmd.Parameters.AddWithValue("@hide", hide);
                    cmd.Parameters.AddWithValue("@updatedby", updatedby);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
