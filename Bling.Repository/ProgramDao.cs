using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;

namespace Bling.Repository
{
    public interface IProgramDao : IDao<Program, string>
    {        
    }

    public class ProgramDao : AbstractDao<Program, string>, IProgramDao
    {
        public ProgramDao(ISession session)
            : base(session)
        {            
        }

    }
}
