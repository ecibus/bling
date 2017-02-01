using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain.Secondary;
using NHibernate;

namespace Bling.Repository.Secondary
{
    public interface IMCMDao
    {
        IList<MCMLockedLoan> GetLockedLoan(int includeByte);
        IList<MCMClosedLoan> GetClosedLoan(int includeByte);
        IList<MCMFallOutLoan> GetFallOutLoan(int includeByte);
        IList<MCMTrades> GetTrades(int includeByte);
    }

    public class MCMDao : AbstractDao<MCMDao, string>, IMCMDao
    {
        public MCMDao(ISession session)
            : base(session)
        {            
        }


        public IList<MCMTrades> GetTrades(int includeByte)
        {
            var temp = m_session.CreateSQLQuery("exec xGEM_MCMTrades :includeByte")
                .AddEntity(typeof(MCMTrades))
                .SetInt32("includeByte", includeByte)
                .List<MCMTrades>().ToList();

            var list = m_session.CreateSQLQuery("exec xGEM_MCMTrades :includeByte")
                .AddEntity(typeof(MCMTrades))
                .SetInt32("includeByte", includeByte)
                .List<MCMTrades>().ToList();

            return list;
        }


        public IList<MCMLockedLoan> GetLockedLoan(int includeByte)
        {
            var temp = m_session.CreateSQLQuery("exec xGEM_MCMLockedLoans :includeByte")
                .AddEntity(typeof(MCMLockedLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMLockedLoan>().ToList();

            var list = m_session.CreateSQLQuery("exec xGEM_MCMLockedLoans :includeByte")
                .AddEntity(typeof(MCMLockedLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMLockedLoan>().ToList();

            return list;
        }

        public IList<MCMClosedLoan> GetClosedLoan(int includeByte)
        {
            var temp = m_session.CreateSQLQuery("exec xGEM_MCMClosedLoans :includeByte")
                .AddEntity(typeof(MCMClosedLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMClosedLoan>().ToList();

            var list = m_session.CreateSQLQuery("exec xGEM_MCMClosedLoans :includeByte")
                .AddEntity(typeof(MCMClosedLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMClosedLoan>().ToList();

            //m_session.Close();

            return list;
        }

        public IList<MCMFallOutLoan> GetFallOutLoan(int includeByte)
        {
            var temp = m_session.CreateSQLQuery("exec xGEM_MCMFallout :includeByte")
                .AddEntity(typeof(MCMFallOutLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMFallOutLoan>().ToList();

            var list = m_session.CreateSQLQuery("exec xGEM_MCMFallout :includeByte")
                .AddEntity(typeof(MCMFallOutLoan))
                .SetInt32("includeByte", includeByte)
                .List<MCMFallOutLoan>().ToList();

            //m_session.Close();

            return list;

        }
    }
}
