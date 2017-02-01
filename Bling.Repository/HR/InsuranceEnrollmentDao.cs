using System;
using Bling.Domain.HR;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface IInsuranceEnrollmentDao : IDao<InsuranceEnrollment, int>
    {
        IList<InsuranceEnrollment> GetByYearMonthAndBranch(string yearMonth, string branch);
        void UpdateRate(int recid, int fieldNo, decimal newValue);
        void UpdateEEStatus(int recid, string newValue);
        void UpdateIsLO(int recid, int islo);
        void UpdateEmpCost(int recid, decimal newValue);
        void RemoveEnrollment(int recid);
        void Copy(string existing, string newmonth);
    }

    public class InsuranceEnrollmentDao : AbstractDao<InsuranceEnrollment, int>, IInsuranceEnrollmentDao
    {
        public InsuranceEnrollmentDao(ISession session)
            : base(session)
        {            
        }

        public IList<InsuranceEnrollment> GetByYearMonthAndBranch(string yearMonth, string branch)
        {
            return m_session.CreateCriteria(typeof(InsuranceEnrollment))
                 .Add(Expression.Eq("Location", "C"))
                 .Add(Expression.Eq("YearMonth", yearMonth))
                 .Add(Expression.Eq("BranchNo", branch))
                 .AddOrder(Order.Asc("EmployeeName"))
                 .List<InsuranceEnrollment>();
        }

        public void UpdateIsLO(int recid, int islo)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateInsuranceIsLO";
                    cmd.Parameters.AddWithValue("@recid", recid);
                    cmd.Parameters.AddWithValue("@islo", islo);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void UpdateEEStatus(int recid, string newValue)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateInsuranceEEStatus";
                    cmd.Parameters.AddWithValue("@recid", recid);
                    cmd.Parameters.AddWithValue("@newValue", newValue);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmpCost(int recid, decimal newValue)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateEmpCost";
                    cmd.Parameters.AddWithValue("@recid", recid);
                    cmd.Parameters.AddWithValue("@newValue", newValue);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRate(int recid, int fieldNo, decimal newValue)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateInsuranceEnrollment";
                    cmd.Parameters.AddWithValue("@recid", recid);
                    cmd.Parameters.AddWithValue("@field", fieldNo);
                    cmd.Parameters.AddWithValue("@newValue", newValue);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveEnrollment(int recid)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_RemoveInsuranceEnrollment";
                    cmd.Parameters.AddWithValue("@recid", recid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Copy(string existing, string newmonth)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_CopyInsuranceEnrollment";
                    cmd.Parameters.AddWithValue("@existingYearMonth", existing);
                    cmd.Parameters.AddWithValue("@newYearMonth", newmonth);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
