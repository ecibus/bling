using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;

namespace Bling.Repository.Compliance
{
    public interface ILPEReasonDao : IDao<LPEReason, int>
    {
    }

    public class LPEReasonDao : AbstractDao<LPEReason, int>, ILPEReasonDao
    {
        public LPEReasonDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(LPEReasonDao));
        }
    }
}
