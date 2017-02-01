using System;
using System.Collections.Generic;
using NHibernate;
using Bling.Domain.IT;
using NHibernate.Criterion;

namespace Bling.Repository.IT
{
    public interface IInventoryFilterDao : IDao<Inventory, int>
    {
        int GetFilteredCount(string issuedTo, string branch);
        IList<Inventory> GetFilteredData(int page, string assignto, string branch);
    }

    public sealed class InventoryFilterDao : AbstractDao<Inventory, int>, IInventoryFilterDao
    {
        private const int ITEMS_PER_PAGE = 10;

        public InventoryFilterDao(ISession session)
            : base(session)
        {            
        }                                              

        public IList<Inventory> GetFilteredData(int page, string issuedTo, string branch)
        {
            ICriteria criteria = CreateFilteredCriteria(issuedTo, branch);

            return criteria
                .AddOrder(Order.Desc("Id"))
                .SetFirstResult((page * ITEMS_PER_PAGE) - ITEMS_PER_PAGE)
                .SetMaxResults(ITEMS_PER_PAGE)
                .List<Inventory>();
        }

        public int GetFilteredCount(string issuedTo, string branch)
        {
            ICriteria criteria = CreateFilteredCriteria(issuedTo, branch);
            return criteria
                .List<Inventory>()
                .Count;
        }

        private ICriteria CreateFilteredCriteria(string issuedTo, string branch)
        {
            ICriteria criteria = m_session.CreateCriteria(typeof(Inventory));

            //if (issuedTo != String.Empty)
            //    criteria.Add(Expression.Eq("IssuedTo.EmployId", issuedTo));

            //if (branch != String.Empty)
            //    criteria.Add(Expression.Eq("BranchName", branch));

            return criteria;
        }
    }
}
