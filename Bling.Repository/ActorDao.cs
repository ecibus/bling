using System;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface IActorDao
    {
        Actor GetByLoginName(string loginName);
    }

    public class ActorDao : AbstractDao<ActorDao, string>, IActorDao
    {
        public ActorDao(ISession session)
            : base(session)
        {            
        }

        public Actor GetByLoginName(string loginName)
        {
            return m_session.CreateCriteria(typeof(Actor))
                .Add(Expression.Eq("LoginName", loginName))
                .UniqueResult<Actor>();
            
        }

    }
}
