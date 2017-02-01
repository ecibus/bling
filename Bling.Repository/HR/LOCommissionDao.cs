using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bling.Domain.HR;
using NHibernate;

namespace Bling.Repository.HR
{
    public interface ILOCommissionDao : IDao<LOCommission, int>
    {
        IList<LOCommission> GetLOCommission(string paymentDate, string fundedDate, string isWeekly);
        IList<LOCommission> GetLOCommissionForOldLoans(string paymentDate, string fundedDate);
        IList<LOCommission> GetManagerCommission(string paymentDate, string fundedDate);
        IList<LOCommission> GetManagerOverride(string paymentDate, string fundedDate);
        IList<string> GetBranchWithCommission(string paymentDate, string fundedDate, string isWeekly);
        IList<string> GetBranchWithTimecard(string month, string year);
        IList<string> GetLOWithCommission(string branchNo, string payDate, string fundedAsOf, string isWeekly);
        bool IsBranchContainsOnHold(string branchNo, string fundedAsOf);
        void LogProcessCommission(string employId, string paymentDate, string fundedDate);
        void RefreshCommissionDataFromTracker(string paymentDate);
        bool IsFirstEmailToSend(string employId, string intervalId);
        string GetUnsendLO(string branchNo, string payDate, string fundedAsOf, string isWeekly,
            string employId, string intervalId);
        void SaveSentEmail(string employId, string intervalId, string datakey, string datavalue);
        bool IsLoEMailSent(string lo, string paydate, string fundedAsOf, bool isWeekly);
        bool IsBranchEMailSent(string branch, string paydate, string fundedAsOf, bool isWeekly);
        void SaveEmailTracking(string lo, string branch, string paydate, string fundedasof, string isweekly);
        void ClearBranchEmailTracking(string paydate, string fundedAsOf, bool isWeekly);
        void ClearLOEmailTracking(string paydate, string fundedAsOf, bool isWeekly);
    }

    public class LOCommissionDao : AbstractDao<LOCommission, int>, ILOCommissionDao
    {
        public LOCommissionDao(ISession session)
            : base(session)
        {
        }

        public void ClearBranchEmailTracking(string paydate, string fundedAsOf, bool isWeekly)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_ClearBranchEmailTracking";
                    cmd.Parameters.AddWithValue("paydate", paydate);
                    cmd.Parameters.AddWithValue("fundedasof", fundedAsOf);
                    cmd.Parameters.AddWithValue("isweekly", isWeekly ? "1" : "0");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ClearLOEmailTracking(string paydate, string fundedAsOf, bool isWeekly)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_ClearLOEmailTracking";
                    cmd.Parameters.AddWithValue("paydate", paydate);
                    cmd.Parameters.AddWithValue("fundedasof", fundedAsOf);
                    cmd.Parameters.AddWithValue("isweekly", isWeekly ? "1" : "0");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsLoEMailSent(string lo, string paydate, string fundedAsOf, bool isWeekly)
        {
            bool sent = false;

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_IsLoEmailSent";
                    cmd.Parameters.AddWithValue("lo", lo);
                    cmd.Parameters.AddWithValue("paydate", paydate);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedAsOf);
                    cmd.Parameters.AddWithValue("isWeekly", isWeekly ? "1" : "0");
                    var s = cmd.ExecuteScalar();

                    if (s != null)
                    {
                        sent = s.ToString() == "" ? false : true;
                    }
                }
            }
            return sent;
        }

        public bool IsBranchEMailSent(string branch, string paydate, string fundedAsOf, bool isWeekly)
        {
            bool sent = false;

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_IsBranchEmailSent";
                    cmd.Parameters.AddWithValue("branch", branch);
                    cmd.Parameters.AddWithValue("paydate", paydate);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedAsOf);
                    cmd.Parameters.AddWithValue("isWeekly", isWeekly ? "1" : "0");
                    var s = cmd.ExecuteScalar();

                    if (s != null)
                    {
                        sent = s.ToString() == "" ? false : true;
                    }
                }
            }
            return sent;
        }

        public void SaveEmailTracking(string lo, string branch, string paydate, string fundedasof, string isweekly)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_SaveEmailTracking";
                    cmd.Parameters.AddWithValue("lo", lo);
                    cmd.Parameters.AddWithValue("branch", branch);
                    cmd.Parameters.AddWithValue("paydate", paydate);
                    cmd.Parameters.AddWithValue("fundedasof", fundedasof);
                    cmd.Parameters.AddWithValue("isweekly", isweekly);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void SaveSentEmail(string employId, string intervalId, string datakey, string datavalue)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_SaveSentEmail";
                    cmd.Parameters.AddWithValue("employId", employId);
                    cmd.Parameters.AddWithValue("intervalId", intervalId);
                    cmd.Parameters.AddWithValue("datakey", datakey);
                    cmd.Parameters.AddWithValue("datavalue", datavalue);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public string GetUnsendLO(string branchNo, string payDate, string fundedAsOf, string isWeekly,
            string employId, string intervalId)
        {
            string lo = "";

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_GetLOToEmail";
                    cmd.Parameters.AddWithValue("branchNo", branchNo);
                    cmd.Parameters.AddWithValue("payDate", payDate);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedAsOf);
                    cmd.Parameters.AddWithValue("isWeekly", isWeekly);
                    cmd.Parameters.AddWithValue("employId", employId);
                    cmd.Parameters.AddWithValue("intervalId", intervalId);
                    lo = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            return lo;
        }

        public IList<LOCommission> GetLOCommission(string paymentDate, string fundedDate, string isWeekly)
        {
            return GetCommissionFromDB(paymentDate, fundedDate, "xGEM_Commission_LO", isWeekly);
        }

        public IList<LOCommission> GetLOCommissionForOldLoans(string paymentDate, string fundedDate)
        {
            return GetCommissionFromDB(paymentDate, fundedDate, "xGEM_Commission_LO_OldLoans", "");
        }

        public IList<LOCommission> GetManagerCommission(string paymentDate, string fundedDate)
        {
            return GetCommissionFromDB(paymentDate, fundedDate, "xGEM_Commission_Manager", "");
        }

        public IList<LOCommission> GetManagerOverride(string paymentDate, string fundedDate)
        {
            return GetCommissionFromDB(paymentDate, fundedDate, "xGEM_Commission_Manager_Override", "");
        }


        public void RefreshCommissionDataFromTracker(string paymentDate)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_RefreshCommissionDataFromTracker";
                    cmd.Parameters.AddWithValue("paymentDate", paymentDate);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void LogProcessCommission(string employId, string paymentDate, string fundedDate)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_Log";
                    cmd.Parameters.AddWithValue("employId", employId);
                    cmd.Parameters.AddWithValue("paymentDate", paymentDate);
                    cmd.Parameters.AddWithValue("fundedDate", fundedDate);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public IList<string> GetBranchWithTimecard(string month, string year)
        {
            IList<string> list = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_InsideSales_GetBranch";
                    cmd.Parameters.AddWithValue("month", month);
                    cmd.Parameters.AddWithValue("year", year);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {   
                        list.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public IList<string> GetLOWithCommission(string branchNo, string payDate, string fundedAsOf, string isWeekly)
        {
            IList<string> list = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = "xGEM_Commission_GetBranchLO";
                    cmd.CommandText = "xGEM_Commission_GetBranchLOLoginName";
                    cmd.Parameters.AddWithValue("branchNo", branchNo);
                    cmd.Parameters.AddWithValue("paydate", payDate);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedAsOf);
                    cmd.Parameters.AddWithValue("isWeekly", isWeekly);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public IList<string> GetBranchWithCommission(string paymentDate, string fundedDate, string isWeekly)
        {
            IList<string> list = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_GetBranch";
                    cmd.Parameters.AddWithValue("paydate", paymentDate);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedDate);
                    cmd.Parameters.AddWithValue("isWeekly", isWeekly);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public bool IsBranchContainsOnHold(string branchNo, string fundedAsOf)
        {
            bool containsOnHold = false;

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_ContainsOnHold";
                    cmd.Parameters.AddWithValue("branchNo", branchNo);
                    cmd.Parameters.AddWithValue("fundedAsOf", fundedAsOf);
                    containsOnHold = Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            return containsOnHold;
        }

        public bool IsFirstEmailToSend(string employId, string intervalId)
        {
            bool firstEmail = false;

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_Commission_FirstEmailToSend";
                    cmd.Parameters.AddWithValue("employId", employId);
                    cmd.Parameters.AddWithValue("intervalId", intervalId);
                    firstEmail = Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            return firstEmail;
        }

        private IList<LOCommission> GetCommissionFromDB(string paymentDate, string fundedDate, string spName, string isWeekly)
        {
            IList<LOCommission> list = new List<LOCommission>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("paymentDate", paymentDate);
                    cmd.Parameters.AddWithValue("fundedDate", fundedDate);
                    if (spName == "xGEM_Commission_LO")
                    {
                        cmd.Parameters.AddWithValue("isWeekly", isWeekly == "true" ? 1 : 0);
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LOCommission c = CreateLOCommission(reader);
                        list.Add(c);
                    }
                    reader.Close();
                }
            }

            return list;
        }

        private static LOCommission CreateLOCommission(SqlDataReader reader)
        {
            if (reader["EmployId"].ToString() == "lallen")
            {
                int a = 1;
            }

            LOCommission c = new LOCommission
            {
                EmployId = reader["EmployId"].ToString(),
                LoanNumber = reader["LoanNumber"].ToString(),
                ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]),
                FundedDate = Convert.ToDateTime(reader["FundedDate"]),
                LoanAmount = Convert.ToDecimal(reader["LoanAmount"]),
                IsBrokeredLoan = Convert.ToBoolean(reader["IsBrokeredLoan"]),
                //PayDate =  Convert.ToDateTime(reader["PayDate"]),
                //PayDate = reader["PayDate"] == null ? null : Convert.ToDateTime(reader["PayDate"]),
                Volume = Convert.ToDecimal(reader["Volume"]),
                BasisPointId = Convert.ToInt32(reader["BasisPointId"]),
                TierId = Convert.ToInt32(reader["TierId"]),
                Rate = Convert.ToDecimal(reader["Rate"]),
                Commission = Convert.ToDecimal(reader["Commission"]),
                ReCompute = Convert.ToBoolean(reader["ReCompute"]),
                IsManager = Convert.ToBoolean(reader["IsManager"]),
                IsOverride = Convert.ToBoolean(reader["IsOverride"]),
                IsWeekly = Convert.ToBoolean(reader["Weekly"])
            };

            if (reader["PayDate"] != DBNull.Value)
                c.PayDate = Convert.ToDateTime(reader["PayDate"]);

            return c;
        }

    }
}
