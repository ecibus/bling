using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;

namespace Bling.Repository.Compliance
{
    public interface IAuditScoreCardGroupDao : IDao<AuditScoreCardGroup, int>
    {

    }

    public class AuditScoreCardGroupDao : AbstractDao<AuditScoreCardGroup, int>, IAuditScoreCardGroupDao
    {
        public AuditScoreCardGroupDao(ISession session)
            : base(session)
        {
        }
    }

}
