using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;

namespace Bling.Repository.Compliance
{
    public interface IDIRWLoanInfoDao
    {
        DIRWLoanInfo GetLoanInfo(string loannumber);
    }

    public class DIRWLoanInfoDao : AbstractDao<DIRWLoanInfo, string>, IDIRWLoanInfoDao
    {
        public DIRWLoanInfoDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(DIRWLoanInfoDao));
        }

        public DIRWLoanInfo GetLoanInfo(string loannumber)
        {
            return m_session.CreateSQLQuery("exec xGEM_DIRWLoanInfo :loannumber ")
                .AddEntity(typeof(DIRWLoanInfo))
                .SetString("loannumber", loannumber)
                .UniqueResult<DIRWLoanInfo>();
        }
        
    }
}
