using System;
using Bling.Domain.HR;
using NHibernate;

namespace Bling.Repository.HR
{
    public interface ICensusDateRangeDao : IDao<CensusDateRange, int>
    {
        
    }

    public class CensusDateRangeDao : AbstractDao<CensusDateRange, int>, ICensusDateRangeDao
    {
        public CensusDateRangeDao(ISession session)
            : base(session)
        {
            
        }
        
    }
}
