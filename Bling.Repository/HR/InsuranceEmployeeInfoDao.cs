using System;
using Bling.Domain.HR;
using NHibernate;
using System.Collections.Generic;

namespace Bling.Repository.HR
{
    public interface IInsuranceEmployeeInfoDao
    {
        IList<InsuranceEmployeeInfo> GetEmployeeByBranch(string branchNo);
    }

    public class InsuranceEmployeeInfoDao : AbstractDao<InsuranceEmployeeInfo, string>, IInsuranceEmployeeInfoDao
    {
        public InsuranceEmployeeInfoDao(ISession session)
            : base(session)
        {            
        }
        
        public IList<InsuranceEmployeeInfo> GetEmployeeByBranch(string branchNo)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetInsuranceEmployeeInfo :branchNo")
                .AddEntity(typeof(InsuranceEmployeeInfo))
                .SetString("branchNo", branchNo)
                .List<InsuranceEmployeeInfo>();
        }
    }
}
