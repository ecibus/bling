using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using NHibernate;
using NHibernate.Criterion;

namespace Bling.Repository.HR
{
    public interface ILOAdjustmentDao : IDao<LOAdjustment, int>
    {
        IList<LOAdjustment> GetAllByLOCode(string loCode);
        void DeleteAdjustment(int id);
    }

    public class LOAdjustmentDao : AbstractDao<LOAdjustment, int>, ILOAdjustmentDao
    {
        public LOAdjustmentDao(ISession session)
            : base (session)
        {
        }

        public IList<LOAdjustment> GetAllByLOCode(string loCode)
        {
            return m_session.CreateCriteria(typeof(LOAdjustment))
            .Add(Expression.Eq("LOCode", loCode))  
            .AddOrder(Order.Desc("Id"))
            .List<LOAdjustment>();
        }

        public void DeleteAdjustment(int id)
        {
            var adj = GetById(id);
            m_session.Delete(adj);
        }
    }
}
