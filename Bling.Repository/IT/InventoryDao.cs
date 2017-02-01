using System;
using System.Collections.Generic;
using Bling.Domain;
using Bling.Domain.IT;
using NHibernate;
using NHibernate.Criterion;
using log4net;

namespace Bling.Repository.IT
{
    public interface IInventoryDao : IDao<Inventory, int>
    {
        int Add(Inventory inventory);
        int GetInventoryCount();
        void Delete(int id);
        IList<Inventory> GetAllInventory(int page);
        IList<LookUp> GetDistictAssignToInInventory();
        IList<LookUp> GetDistinctBranchInInventory();
        IList<Inventory> Search(int page, string searchstring);
        int GetSearchCount(string searchstring);
    }

    public sealed class InventoryDao : AbstractDao<Inventory, int>, IInventoryDao
    {
        private const int ITEMS_PER_PAGE = 10;
        public InventoryDao(ISession session)
            : base(session)
        {
            m_logger = LogManager.GetLogger(typeof(InventoryDao));
        }        

        public int Add(Inventory inventory)
        {
            return Save(inventory).Id;
        }

        public IList<Inventory> GetAllInventory(int page)
        {
            var sql =
" with data as ( " +
" 	Select " +
" 		i.Id, i.IssuedOn, i.Make, i.Model, i.SerialNumber, i.Quantity, i.dtAddedOn, i.BranchName " +
" 		, it.first_name + ' ' + it.last_name as IssuedTo " +
" 		, ab.first_name + ' ' + ab.last_name as AddedBy " +
" 	from " +
" 		xGEM_Inventory i " +
" 		left join userinfo it on i.IssuedTo = it.employ_id " +
" 		left join userinfo ab on i.AddedBy = ab.employ_id " +
" 	where i.Id < 262 " +
" 	union " +
" 	Select " +
" 		i.Id, i.IssuedOn, i.Make, i.Model, i.SerialNumber, i.Quantity, i.dtAddedOn, i.BranchName " +
" 		, it.firstname + ' ' + it.lastname as IssuedTo " +
" 		, ab.firstname + ' ' + ab.lastname as AddedBy " +
" 	from " +
" 		xGEM_Inventory i " +
" 		left join [GEMBP01].[BytePro].[dbo].[User] it on i.IssuedTo = it.username " +
" 		left join [GEMBP01].[BytePro].[dbo].[User] ab on i.AddedBy = ab.username " +
" 	where i.Id > 262 " +
" ), " +
" DataWithRowNumber as ( " +
" 	select  " +
" 		ROW_NUMBER() over (order by d.id desc) as number " +
" 		, d.*  " +
" 	from data d " +
" ) " +
" select * from DataWithRowNumber " +
" WHERE NUMBER BETWEEN ((" + page.ToString() + " - 1) * 10 + 1) AND (" + page.ToString() + " * 10) " ;

            return
                m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(Inventory))
                .List<Inventory>();

                //m_session.CreateCriteria(typeof(Inventory))
                //.AddOrder(Order.Desc("Id"))
                //.SetFirstResult((page * ITEMS_PER_PAGE) - ITEMS_PER_PAGE)
                //.SetMaxResults(ITEMS_PER_PAGE)
                //.List<Inventory>();                
        }

        public IList<Inventory> Search(int page, string searchstring)
        {
            string sql = BuildSearchSQL(searchstring);

            sql = sql +
                " WHERE NUMBER BETWEEN ((" + page.ToString() + " - 1) * 10 + 1) AND (" + page.ToString() + " * 10) ";

            return
                m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(Inventory))
                .List<Inventory>();

            //ICriteria crit = m_session.CreateCriteria(typeof(Inventory))
            //    .AddOrder(Order.Desc("Id"))
            //    .SetFirstResult((page * ITEMS_PER_PAGE) - ITEMS_PER_PAGE)
            //    .SetMaxResults(ITEMS_PER_PAGE)
            //    .CreateAlias("IssuedTo", "it");

            //Disjunction or = Expression.Disjunction();

            //foreach (var s in searches)
            //{
            //    string search = "%" + s + "%";
            //    or.Add(Expression.Like("Make", search));
            //    or.Add(Expression.Like("Model", search));
            //    or.Add(Expression.Like("SerialNumber", search));
            //    or.Add(Expression.Like("BranchName", search));
            //    or.Add(Expression.Like("it.FirstName", search));
            //    or.Add(Expression.Like("it.LastName", search));
            //}

            //crit.Add(or);
            //return crit.List<Inventory>();
        }

        public int GetSearchCount(string searchstring)
        {
            string sql = BuildSearchSQL(searchstring);

            return
                m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(Inventory))
                .List<Inventory>().Count;
            /*
            IList<string> searches = searchstring.Split(' ');

            ICriteria crit = m_session.CreateCriteria(typeof(Inventory))
                .CreateAlias("IssuedTo", "it");

            Disjunction or = Expression.Disjunction();

            foreach (var s in searches)
            {
                string search = "%" + s + "%";
                or.Add(Expression.Like("Make", search));
                or.Add(Expression.Like("Model", search));
                or.Add(Expression.Like("SerialNumber", search));
                or.Add(Expression.Like("BranchName", search));
                or.Add(Expression.Like("it.FirstName", search));
                or.Add(Expression.Like("it.LastName", search));
            }

            crit.Add(or);
            return crit.List<Inventory>().Count;
            */
        }

        private string BuildSearchSQL(string searchstring)
        {
            //IList<string> searches = searchstring.Split(' ');

            string where1 = " and (";
            string where2 = " and (";
            where1 += " make like '%" + searchstring + "%' or " +
                     " model like '%" + searchstring + "%' or " +
                     " serialnumber like'%" + searchstring + "%' or " +
                     " branchname like '%" + searchstring + "%' or " +
                     " it.first_name like '%" + searchstring + "%' or " +
                     " it.last_name like '%" + searchstring + "%' ";

            where2 += " make like '%" + searchstring + "%' or " +
                     " model like '%" + searchstring + "%' or " +
                     " serialnumber like'%" + searchstring + "%' or " +
                     " branchname like '%" + searchstring + "%' or " +
                     " it.firstname like '%" + searchstring + "%' or " +
                     " it.lastname like '%" + searchstring + "%' ";

            /*
            int counter = 0;
            foreach (var s in searches)
            {
                counter++;
                if (counter > 1)
                {
                    where1 += " or ";
                    where2 += " or ";
                }

                where1 += " make like '%" + s + "%' or " +
                         " model like '%" + s + "%' or " +
                         " serialnumber like'%" + s + "%' or " +
                         " branchname like '%" + s + "%' or " +
                         " it.first_name like '%" + s + "%' or " +
                         " it.last_name like '%" + s + "%' ";

                where2 += " make like '%" + s + "%' or " +
                         " model like '%" + s + "%' or " +
                         " serialnumber like'%" + s + "%' or " +
                         " branchname like '%" + s + "%' or " +
                         " it.firstname like '%" + s + "%' or " +
                         " it.lastname like '%" + s + "%' ";
            }
             */

            where1 = where1 + ") ";
            where2 = where2 + ") ";

            var sql =
" with data as ( " +
" 	Select " +
" 		i.Id, i.IssuedOn, i.Make, i.Model, i.SerialNumber, i.Quantity, i.dtAddedOn, i.BranchName " +
" 		, it.first_name + ' ' + it.last_name as IssuedTo " +
" 		, ab.first_name + ' ' + ab.last_name as AddedBy " +
" 	from " +
" 		xGEM_Inventory i " +
" 		left join userinfo it on i.IssuedTo = it.employ_id " +
" 		left join userinfo ab on i.AddedBy = ab.employ_id " +
" 	where i.Id < 262 " + where1 +
" 	union " +
" 	Select " +
" 		i.Id, i.IssuedOn, i.Make, i.Model, i.SerialNumber, i.Quantity, i.dtAddedOn, i.BranchName " +
" 		, it.firstname + ' ' + it.lastname as IssuedTo " +
" 		, ab.firstname + ' ' + ab.lastname as AddedBy " +
" 	from " +
" 		xGEM_Inventory i " +
" 		left join [GEMBP01].[BytePro].[dbo].[User] it on i.IssuedTo = it.username " +
" 		left join [GEMBP01].[BytePro].[dbo].[User] ab on i.AddedBy = ab.username " +
" 	where i.Id > 262 " + where2 +
" ), " +
" DataWithRowNumber as ( " +
" 	select  " +
" 		ROW_NUMBER() over (order by d.id desc) as number " +
" 		, d.*  " +
" 	from data d " +
" ) " +
" select * from DataWithRowNumber ";
            return sql;
        }

        public int GetInventoryCount()
        {
            return m_session.CreateCriteria(typeof(Inventory))
                .List<Inventory>().Count;
        }

        public IList<LookUp> GetDistictAssignToInInventory()
        {
            string sql =
                "Select distinct i.IssuedTo Value, u.First_Name + ' ' + u.Last_Name Name " +
                "From xGEM_Inventory i " +
                   "inner join UserInfo u on i.IssuedTo = u.Employ_id " +
                "Order by u.First_Name + ' ' + u.Last_Name ";

            return m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(LookUp))
                .List<LookUp>();       
        }

        public IList<LookUp> GetDistinctBranchInInventory()
        {
            string sql =
                "Select distinct BranchName Value, BranchName Name " +
                "From xGEM_Inventory  " +
                "Order by BranchName ";

            return m_session.CreateSQLQuery(sql)
                .AddEntity(typeof(LookUp))
                .List<LookUp>();   
        }

       
        public void Delete(int id)
        {
            Inventory inv = GetById(id);

            m_logger.DebugFormat("Deleting Inventory '{0}' '{1}' '{2}' '{3}' '{4}' '{5}' '{6}'",
                inv.Make, inv.Model, inv.SerialNumber, inv.Quantity, inv.IssuedOn, inv.IssuedTo, inv.BranchName);

            m_session.Delete(inv);
        }

    }
}
