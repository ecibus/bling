using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.LOS;
using NHibernate;

namespace Bling.Repository.LOS
{
    public interface IHMDAFieldDao
    {
        IList<HMDAField> GetAll();
    }

    public class HMDAFieldDao : AbstractDao<HMDAField, int>, IHMDAFieldDao
    {
        public HMDAFieldDao(ISession session) : base(session)
        {
            
        }       
    }
}
