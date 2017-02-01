using System;
using Bling.Domain.HR;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Bling.Repository.HR
{
    public interface IInsuranceRateDao : IDao<InsuranceRate, int>
    {
        IList<InsuranceRate> GetRatesForType(string type);
        IList<string> GetEEStatus();
        void AddNewRate(string type, decimal newRate);    
    }

    public class InsuranceRateDao : AbstractDao<InsuranceRate, int>, IInsuranceRateDao
    {
        public InsuranceRateDao(ISession session)
            : base(session)
        {            
        }
        
        public IList<InsuranceRate> GetRatesForType(string type)
        {
            return m_session.CreateCriteria(typeof(InsuranceRate))
                .Add(Expression.Eq("Location", "C"))
                .Add(Expression.Eq("InsuranceType", type))
                .Add(Expression.Gt("Rate", 0.00m))
                .AddOrder(Order.Asc("Rate"))
                .List<InsuranceRate>();   
        }

        public void AddNewRate(string type, decimal newRate)
        {
            InsuranceRate rate = new InsuranceRate { Location = "C", InsuranceType = type, Rate = newRate, Data = null };
            m_session.Save(rate);
        }

        public IList<string> GetEEStatus()
        {
            return m_session.CreateSQLQuery("select distinct Ins_Data from hr_insrate where ins_type = 8 and ins_data is not null order by ins_data")
                .List<string>();
        }
    }
}
