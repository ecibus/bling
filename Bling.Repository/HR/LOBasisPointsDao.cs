using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.HR;
using NHibernate;
using NHibernate.Criterion;


namespace Bling.Repository.HR
{
    public interface ILOBasisPointsDao : IDao<BasisPoints, int>
    {
        List<BasisPoints> GetAllByLO(string employeeNo);
        void RemoveBasisPoints(int id);
        void UpdateBasisPoint(int id, string item, string newValue);
    }

    public interface IByteLOBasisPointsDao : IDao<ByteBasisPoints, int>
    {
        List<ByteBasisPoints> GetAllByLO(string employeeNo, string branchNo);
        void RemoveBasisPoints(int id);
        void UpdateBasisPoint(int id, string item, string newValue);
    }

    public class ByteLOBasisPointsDao : AbstractDao<ByteBasisPoints, int>, IByteLOBasisPointsDao
    {
        public ByteLOBasisPointsDao(ISession session)
            : base(session)
        {
        }

        public List<ByteBasisPoints> GetAllByLO(string employeeId, string branchNo)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetByteBasisPointByLO :employeeId, :branchNo")
                .AddEntity(typeof(ByteBasisPoints))
                .SetString("employeeId", employeeId)
                .SetString("branchNo", branchNo)
                .List<ByteBasisPoints>().ToList();
        }

        public void RemoveBasisPoints(int id)
        {
            m_session.CreateSQLQuery("exec xGEM_ByteBasisPoint_Delete :id")
                .SetInt32("id", id)
                .ExecuteUpdate();
        }

        public void UpdateBasisPoint(int id, string item, string newValue)
        {
            m_session.CreateSQLQuery("exec xGEM_ByteBasisPoint_Update :id, :item, :newValue")
                .SetInt32("id", id)
                .SetString("item", item)
                .SetString("newValue", newValue)
                .ExecuteUpdate();
        }
    }

    public class LOBasisPointsDao : AbstractDao<BasisPoints, int>, ILOBasisPointsDao
    {
        public LOBasisPointsDao(ISession session)
            : base(session)
        {
        }

        public List<BasisPoints> GetAllByLO(string employeeId)
        {
            return m_session.CreateCriteria(typeof(BasisPoints))
                .Add(Expression.Eq("LoanOfficer.EmployId", employeeId))
                .Add(Expression.Eq("Enabled", true))
                .AddOrder(Order.Desc("EffectiveDate"))
                .List<BasisPoints>().ToList(); 
        }

        public void RemoveBasisPoints(int id)
        {
            m_session.CreateSQLQuery("exec xGEM_BasisPoint_Delete :id")
                .SetInt32("id", id)
                .ExecuteUpdate();
        }

        public void UpdateBasisPoint(int id, string item, string newValue)
        {
            m_session.CreateSQLQuery("exec xGEM_BasisPoint_Update :id, :item, :newValue")
                .SetInt32("id", id)
                .SetString("item", item)
                .SetString("newValue", newValue)
                .ExecuteUpdate();
        }
    }
}
