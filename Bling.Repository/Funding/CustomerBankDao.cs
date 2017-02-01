using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Funding;
using NHibernate;
using log4net;

namespace Bling.Repository.Funding
{
    public interface ICustomerBankDao
    {
        IList<CustomerBankData> GetData(string start, string end, string batchno, int includeByte);
    }

    public class CustomerBankDao : AbstractDao<CustomerBankData, int>, ICustomerBankDao
    {
        public CustomerBankDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(CustomerBankDao));            
        }

        public IList<CustomerBankData> GetData(string start, string end, string batchno, int includeByte)
        {
            return m_session.CreateSQLQuery("exec xGEM_CustomerBank :start, :end, :batchno, :includeByte")
                .AddEntity(typeof(CustomerBankData))
                .SetString("start", start)
                .SetString("end", end)
                .SetString("batchno", batchno)
                .SetInt32("includeByte", includeByte)
                .List<CustomerBankData>();                
        }
    }
}
