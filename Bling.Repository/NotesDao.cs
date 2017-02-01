using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;

namespace Bling.Repository
{
    public interface INotesDao : IDao<Notes, int>
    {
    }

    public class NotesDao : AbstractDao<Notes, int>, INotesDao
    {
        public NotesDao(ISession session)
            : base (session)
        {

        }
    }
}
