using System;
using System.Collections.Generic;
using NHibernate;
using Bling.Domain.HR;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository.HR
{
    public interface IExpiredLicenseDao : IDao<ExpiredLicense, string>
    {
        IList<ExpiredLicense> GetAllEmployee();
    }

    public class ExpiredLicenseDao : AbstractDao<ExpiredLicense, string>, IExpiredLicenseDao
    {
        public ExpiredLicenseDao(ISession session)
            : base(session)
        {            
        }

        public IList<ExpiredLicense> GetAllEmployee()
        {            
            IList<ExpiredLicense> list = new List<ExpiredLicense>();
            
            using (var cn = new SqlConnection(DMDDataConnectionString)) 
            {
                using (var cmd = new SqlCommand { Connection = cn }) 
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_ExpiredLicense";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) 
                    {
                        list.Add(
                            new ExpiredLicense() 
                            { 
                                Branch = reader["Branch"].ToString(),
                                BranchNo = reader["BranchNo"].ToString(),
                                EMail = reader["EMail"].ToString(),
                                BranchEMail = reader["BranchEMail"].ToString(),
                                EmployeeId = reader["EmployeeId"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(), 
                                HireDate = Convert.ToDateTime(reader["HireDate"].ToString()),
                                ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"].ToString())                                
                            }
                        );
                    }
                }
            }
            
            return list;
        }


    }
}
