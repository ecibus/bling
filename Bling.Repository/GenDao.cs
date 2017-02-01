using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository
{
    public interface IGenDao
    {
        Gen GetByLoanNumber(string loanNumber);
    }

    public class GenDao : AbstractDao<Gen, string>, IGenDao
    {
        public GenDao(ISession session) 
            : base(session)
        {
        }

        public Gen GetByLoanNumber(string loanNumber)
        {            
            Gen gen = m_session.CreateCriteria(typeof(Gen))
                .Add(Expression.Eq("LoanNumber", loanNumber))                                                
                .UniqueResult<Gen>();
            
            if (gen == null)
            {
                StringBuilder json = new StringBuilder();

                json.Append("data = { ");
                json.Append("\"Message\" : \"Loan Number is not valid.\" ");
                json.Append(" }");

                throw new Exception(json.ToString());
            }
            ILUPopupDao dao = new LUPopupDao(m_session);

            gen.Stage = dao.GetStageDescriptionForAlias(gen.Stage).Description;

            return gen;            
        }
    }
}
