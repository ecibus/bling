using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bling.Domain.Secondary;
using log4net;
using NHibernate;

namespace Bling.Repository.Secondary
{
    public interface ILoanSolutionDao
    {       
        void DeleteAll();
        void UpdateInvestor(string lsInvestor, string dtInvestorId);
        void InsertDataInLSMap(string userid);
        List<string> GetLSInvestor();
        List<string> GetLSInvestorByProgramId(string programId);
        List<string> GetProgramDescriptionByInvestorAndProgramId(string investor, string programId);
        List<LSDTInvestorMapping> GetInvestorMapping();
        LoanSolutionProgram Save(LoanSolutionProgram lsp);
    }

    public class LoanSolutionDao : AbstractDao<LoanSolutionProgram, int>, ILoanSolutionDao
    {
        public LoanSolutionDao(ISession session) : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(LoanSolutionDao));            
        }

        public void DeleteAll()
        {
            m_session.CreateSQLQuery("truncate table xGEM_LoanSolutionProgram")
                .ExecuteUpdate();
        }

        public List<string> GetLSInvestor()
        {
            return m_session.CreateSQLQuery("select distinct InvestorName from xGEM_LoanSolutionProgram")
                .List<string>().ToList();
        }

        public List<string> GetLSInvestorByProgramId(string programId)
        {
            return m_session.CreateSQLQuery("select distinct LS_InvCode from xGEM_LSmap where program_id = :programId and gemlock_exclude = 0 order by ls_invcode ")
                .SetString("programId", programId)
                .List<string>().ToList();
        }

        public List<string> GetProgramDescriptionByInvestorAndProgramId(string investor, string programId)
        {
            return m_session.CreateSQLQuery("select distinct LS_LoanDesc from xGEM_LSmap where program_id = :programId and ls_invcode = :investor and gemlock_exclude = 0 order by LS_LoanDesc ")
                .SetString("investor", investor)
                .SetString("programId", programId)
                .List<string>().ToList();
        }
        
        public List<LSDTInvestorMapping> GetInvestorMapping()
        {
            string sql = 
             "Select distinct " +
                " c.Id Id, " +
                " a.InvestorName LSInvestor, b.Investor + ' (' + b.inv_name + ')' DTInvestorCode " +
            "From " +
               "xGEM_LoanSolutionProgram a " +
               "inner join Investor b on a.gem_invcode = b.investor_id " +
               "inner join xGEM_LSDTInvestor c on a.InvestorName = c.LSInvestor ";
            
            return m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(LSDTInvestorMapping))
                .List<LSDTInvestorMapping>().ToList();
        }

        public void UpdateInvestor(string lsInvestor, string dtInvestorId)
        {
            m_logger.DebugFormat("Mapping {0} to {1}", lsInvestor, dtInvestorId);

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateLoanSolutionInvestor";
                    cmd.Parameters.AddWithValue("@LSInvestor", lsInvestor);
                    cmd.Parameters.AddWithValue("@DTInvestorCode", dtInvestorId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertDataInLSMap(string userid)
        {
            m_logger.DebugFormat("Inserting Loan Solution Program in xGEM_LSMap table." );

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_AddLSProgramInGEMLock";
                    cmd.Parameters.AddWithValue("@CreatedBy", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
