using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Secondary;
using NHibernate;

namespace Bling.Repository.Secondary
{
    public interface ILSDTInvestorMappingDao
    {
        IList<LSDTInvestorMapping> GetAll();
    }

    public class LSDTInvestorMappingDao : AbstractDao<LSDTInvestorMapping, int>, ILSDTInvestorMappingDao
    {
        public LSDTInvestorMappingDao(ISession session)
            : base(session)
        {            
        }
    }
}
