using System;
using System.Collections.Generic;
using NHibernate;
using System.Configuration;
using log4net;
using System.Data;
using System.Data.SqlClient;

namespace Bling.Repository
{
    public abstract class AbstractDao<T, IdT> : IDao<T, IdT>
    {
        protected ISession m_session;
        protected static ILog m_logger;
 
        public AbstractDao(ISession session)
        {
            m_session = session;
            m_logger = LogManager.GetLogger(typeof(AbstractDao<T, IdT>));
        }

        public T GetById(IdT id)
        {
            return m_session.Get<T>(id);
        }

        public T Save(T entity)
        {           
            
            m_session.SaveOrUpdate(entity);
            
            return entity;
        }

        public IList<T> GetAll()
        {
            return m_session.CreateCriteria(typeof(T)).List<T>();
        }

        public DataTable GetDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                cmd.Connection = cn;
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public DataTable GetPclDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (var cn = new SqlConnection(PCLConnectionString))
            {
                cmd.Connection = cn;
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public void ExecuteNonQuery(SqlCommand cmd)
        {
            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public void ExecutePclNonQuery(SqlCommand cmd)
        {
            using (var cn = new SqlConnection(PCLConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public void ExecuteNonQueryForMWDataStore(SqlCommand cmd)
        {
            using (var cn = new SqlConnection(MWDataStoreConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public string DMDDataConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["dmddata"].ConnectionString; }
        }

        public string MWDataStoreConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["mwdatastore"].ConnectionString; }
        }

        public string AMBConnectionString
        {
            get { return ConfigurationManager.AppSettings["AMBConnectionString"]; }
        }

        public string PCLConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["pcldata"].ConnectionString; }
        }
    }
}
