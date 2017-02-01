using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;
namespace Bling.Repository
{
    public interface ILUPopupDao : IDao<LUPopup, int>
    {
        LUPopup GetStageDescriptionForAlias(string alias);
        IList<LUPopup> GetByType(string type);
    }

    public class LUPopupDao : AbstractDao<LUPopup, int>, ILUPopupDao
    {
        public LUPopupDao(ISession session)
		    : base(session)
		{				
		}

        public LUPopup GetStageDescriptionForAlias(string alias)
        {
            return m_session.CreateCriteria(typeof(LUPopup))
                .Add(Expression.Eq("Type", "stage"))
                .Add(Expression.Eq("Alias", alias))
                .UniqueResult<LUPopup>();
        }

        public IList<LUPopup> GetByType(string type)
        {
            return m_session.CreateCriteria(typeof(LUPopup))
                .Add(Expression.Eq("Type", type))
                .AddOrder(Order.Asc("ColumnOrder"))
                .List<LUPopup>();
        }
    }
}
