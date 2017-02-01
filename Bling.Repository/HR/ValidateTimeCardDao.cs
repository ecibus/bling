using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using NHibernate;

namespace Bling.Repository.HR
{
    public interface IValidateTimeCardDao
    {
        IList<TCHeader> GetTimeCardHeader(string start, string end);
        IList<TCLineItems> GetTimeCardLineItem(string start, string end);
        IList<TCLineItems> GetTimeCardPreviousCutoff(string end);
        IList<TCTotal> GetTimeCardTotal(string start, string end);
    }

    public class ValidateTimeCardDao : AbstractDao<string, int>, IValidateTimeCardDao
    {
        public ValidateTimeCardDao(ISession session)
            : base(session)
        {
        }

        public IList<TCHeader> GetTimeCardHeader(string start, string end)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetTimeCardHeader :start, :end")
                .AddEntity(typeof(TCHeader))
                .SetString("start", start)
                .SetString("end", end)
                .List<TCHeader>();
        }

        public IList<TCLineItems> GetTimeCardLineItem(string start, string end)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetTimeCardLineItem :start, :end")
                .AddEntity(typeof(TCLineItems))
                .SetString("start", start)
                .SetString("end", end)
                .List<TCLineItems>();
        }

        public IList<TCLineItems> GetTimeCardPreviousCutoff(string start)
        {
            DateTime currentStart = Convert.ToDateTime(start);
            string lastStart;
            string lastEnd;
            DateTime yesterday = currentStart.AddDays(-1);

            lastStart = yesterday.Month.ToString() + (currentStart.Day == 1 ? "/16/" : "/1/") + yesterday.Year.ToString();
            lastEnd = yesterday.ToShortDateString();

            return m_session.CreateSQLQuery("exec xGEM_GetTimeCardLineItem :start, :end")
                .AddEntity(typeof(TCLineItems))
                .SetString("start", lastStart)
                .SetString("end", lastEnd)
                .List<TCLineItems>();
        }

        public IList<TCTotal> GetTimeCardTotal(string start, string end)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetTimeCardTotal :start, :end")
                .AddEntity(typeof(TCTotal))
                .SetString("start", start)
                .SetString("end", end)
                .List<TCTotal>();
        }

    }
}
