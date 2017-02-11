using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.RestApi.TurningPoint;
using System.Data.SqlClient;
using System.Data;
using NHibernate;

namespace Bling.Repository.RestApi
{
    public interface ITurningPointDao : IDao<ByteUser, int>
    {
        List<ByteUser> SearchUser(string crit);
        List<ByteUser> GetTurningPointUser();
    }

    public class TurningPointDao : AbstractDao<ByteUser, int>, ITurningPointDao
    {
        public TurningPointDao(ISession session) 
            : base(session)
        {
        }

        public List<ByteUser> SearchUser(string crit)
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_TurningPointSearchUsers :search")
                .AddEntity(typeof(ByteUser))
                .SetString("search", crit)
                .List<ByteUser>()
                .ToList();
        }

        public List<ByteUser> GetTurningPointUser()
        {
            return m_session.CreateSQLQuery("exec dbo.xGEM_GetTurningPointUsers")
                .AddEntity(typeof(ByteUser))
                .List<ByteUser>()
                .ToList();
        }
    }
}
