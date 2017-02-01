using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface ICSVExportDao : IDao<CSVExport, int>
    {
        List<CSVExport> GetCSVExportByType(string type);
    }

    public class CSVExportDao : AbstractDao<CSVExport, int>, ICSVExportDao
    {
        public CSVExportDao(ISession session)
            : base(session)
        {            
        }

        public List<CSVExport> GetCSVExportByType(string type)
        {
            return m_session.CreateCriteria(typeof(CSVExport))
                .Add(Expression.Eq("Type", type))
                .List<CSVExport>()
                .OrderBy(x => x.Value)
                .ToList()
                ;

        }
    }
}
