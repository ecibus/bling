using System;
using Bling.Domain.Underwriting;
using System.Collections.Generic;
using NHibernate;

namespace Bling.Repository.Underwriting
{
    public interface IScoreCardGroupDao : IDao<ScoreCardGroup, int>
    {
        
    }

    public class ScoreCardGroupDao : AbstractDao<ScoreCardGroup, int>, IScoreCardGroupDao
    {
        public ScoreCardGroupDao(ISession session) : base(session)
        {
        }        
    }
}
