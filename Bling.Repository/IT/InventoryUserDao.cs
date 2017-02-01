using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.IT;
using NHibernate;

namespace Bling.Repository.IT
{
    public interface IInventoryUserDao
    {
        List<InventoryUser> GetAllUser();

    }
    public class InventoryUserDao : AbstractDao<InventoryUser, int>, IInventoryUserDao
    {
        public InventoryUserDao(ISession session) : base(session)
        {
        }

        public List<InventoryUser> GetAllUser()
        {
            //string sql =
            //    "select ui.employ_id, " +
            //       "ui.first_name, ui.last_name, " +
            //       "isnull(b.dba, 'CORPORATE') Branch " +
            //    "from userinfo ui " +
            //       "left join brokers b on ui.branch_id = b.brokers_id " +
            //    "where ui.exclude = 0  and last_name <> 'loan officer' " +
            //       "and last_name <> 'SECONDARY MARKETING'  " +
            //    "order by first_name ";

            string sql =
            " select distinct u.username as 'employ_id', u.firstname as 'first_name', " +
            "   u.lastname  as 'last_name', " +
            " 	min(case when o.costcenter = '' then '113 - Corporate' else ab.BranchName end) as 'branch' " +
            " from [GEMBP01].[BytePro].[dbo].[User] u " +
            " 	left join [GEMBP01].[BytePro].[dbo].[UserOrgProfile] uop on uop.UserID = u.UserID " +
            " 	left join [GEMBP01].[BytePro].[dbo].[Organization] o on uop.OrganizationID = o.OrganizationID " +
            " 	left join xGEM_AMBBranch ab on ab.BranchCode = o.costcenter " +
            " where u.username not in ('admin', 'test', '106manager') " +
            " 	and u.disabled = 0 " +
            " 	and o.costcenter not in (1000) " +
            " 	and u.firstname not in ('byteprop', 'corporate') " +
            " group by u.username, u.firstname, u.lastname " +
            " order by u.firstname ";

            var result =  m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(InventoryUser))
                .List<InventoryUser>().ToList();

            return result;
        }
    }
}
