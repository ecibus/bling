using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Bling.Domain.HR;
using NHibernate.Criterion;

namespace Bling.Repository.HR
{
    
        public interface ITimeCardSubmitDao : IDao<TimeCardSubmit, int>
        {
            TimeCardSubmit Reject(int id);
            //TimeCardSubmit GetSubmittedTimeCard(TimeCardSubmit tcs);
        }

        public class TimeCardSubmitDao : AbstractDao<TimeCardSubmit, int>, ITimeCardSubmitDao
        {
            public TimeCardSubmitDao(ISession session)
                : base(session)
            {
            }

            public TimeCardSubmit Reject(int id)
            {
                TimeCardSubmit current = GetById(id); // GetSubmittedTimeCard(tcs);

                if (current != null)
                {
                    current.Submitted = false;
                    current.Accepted = false;
                    Save(current);
                }
                return current;
            }

            //public TimeCardSubmit GetSubmittedTimeCard(TimeCardSubmit tcs)
            //{
            //    return m_session.CreateCriteria(typeof(TimeCardSubmit))
            //        .Add(Expression.Eq("EmployeeId", tcs.EmployeeId))
            //        .Add(Expression.Eq("Month", tcs.Month))
            //        .Add(Expression.Eq("Year", tcs.Year))
            //        .Add(Expression.Eq("MonthPart", tcs.MonthPart))
            //        .UniqueResult<TimeCardSubmit>();
            //}

        }

    
}
