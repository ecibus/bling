using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;
using log4net;

namespace Bling.Repository
{
    public interface ITranslogDao : IDao<Translog, int>
    {
    }

    public class TranslogDao : AbstractDao<Translog, int>, ITranslogDao
    {
        public TranslogDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(TranslogDao));
        }
    }
}
