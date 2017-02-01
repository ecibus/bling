using System;
using System.Collections.Generic;
using System.Linq;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;
using Bling.Domain.HR;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository
{
    public interface IUserInfoDao : IDao<UserInfo, string>
    {
        List<UserInfo> GetAllFunder();
        List<UserInfo> GetAllUnderwriter();
        List<UserInfo> GetLicensedUser();
        List<UserInfo> GetAllLO();
        List<UserInfo> GetActiveLO();
        UserInfo GetByActorId(string actorId);
        List<ByteLO> GetAllByteLO();
        string GetEmailByLoginName(string loginName);
        string GetByteUserFullName(string username);
        List<KeyValuePair<string, string>> GetByteAllUserFullName();
    }

    public class UserInfoDao : AbstractDao<UserInfo, string>, IUserInfoDao
    {
        public UserInfoDao(ISession session)
            : base(session)
        {
        }

        public List<UserInfo> GetAllFunder()
        {
            return m_session.CreateCriteria(typeof(UserInfo))
                .Add(Expression.Eq("IsFunder", true))
                .Add(Expression.Eq("Exclude", false))
                .AddOrder(Order.Asc("FirstName"))
                .List<UserInfo>().ToList();                
        }

        public List<UserInfo> GetAllUnderwriter()
        {
            return m_session.CreateCriteria(typeof(UserInfo))
                .Add(Expression.Eq("IsUnderwriter", true))
                .Add(Expression.Eq("Exclude", false))
                .AddOrder(Order.Asc("FirstName"))
                .List<UserInfo>().ToList();
        }

        public List<UserInfo> GetLicensedUser()
        {
            //return m_session.CreateCriteria(typeof(UserInfo))
            //    .Add(Expression.Eq("Exclude", false))
            //    .Add(Expression.Eq("IsLicensedUser", true))
            //    .Add(Expression.IsNotNull("HireDate"))
            //    .AddOrder(Order.Asc("FirstName"))
            //    .List<UserInfo>().ToList();

            return GetByteGEMUser();
        }

        public UserInfo GetByActorId(string actorId)
        {
            return m_session.CreateCriteria(typeof(UserInfo))
                .Add(Expression.Eq("ActorId", actorId))
                .UniqueResult<UserInfo>();  
        }

        public string GetEmailByLoginName(string loginName)
        {
            string email = m_session.CreateSQLQuery("exec xGEM_GetEmailByLoginName :loginName ")
                .SetString("loginName", loginName)
                .UniqueResult<string>();

            return email;
        }

        public List<UserInfo> GetAllLO()
        {
            return m_session.CreateCriteria(typeof(UserInfo))
                .Add(Expression.Not(Expression.Eq("NMLSNo", "")))
                //.Add(Expression.Eq("Exclude", false))
                .AddOrder(Order.Asc("FirstName"))
                .SetFetchMode("Broker", FetchMode.Eager)
                .List<UserInfo>().ToList();
        }

        public List<UserInfo> GetActiveLO()
        {
            return m_session.CreateCriteria(typeof(UserInfo))
                .Add(Expression.Not(Expression.Eq("NMLSNo", "")))
                //.Add(Expression.Eq("Exclude", false))
                .Add(Expression.IsNull("TerminationDate"))
                .AddOrder(Order.Asc("FirstName"))
                .SetFetchMode("Broker", FetchMode.Eager)
                .List<UserInfo>().ToList();
        }

        public List<ByteLO> GetAllByteLO()
        {
            return m_session.CreateSQLQuery("exec xGEM_GetAllByteLO")
                .AddEntity(typeof(ByteLO))
                .List<ByteLO>().ToList();  
        }

        public string GetByteUserFullName(string username)
        {
            string fullname = m_session.CreateSQLQuery("exec xGEM_GetByteUserFullName :username ")
                .SetString("username", username)
                .UniqueResult<string>();

            return fullname;
        }

        public List<KeyValuePair<string, string>> GetByteAllUserFullName()
        {
            var list = new List<KeyValuePair<string, string>>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetByteAllUserFullName";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            list.Add(new KeyValuePair<string, string>(
                                reader["username"].ToString(),
                                reader["fullname"].ToString()
                            ));
                        }
                    }
                }
            }

            return list;
        }

        private List<UserInfo> GetByteGEMUser()
        {
            var list = new List<UserInfo>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetByteGEMUser";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            list.Add(new UserInfo 
                                { 
                                    FirstName = reader["first_name"].ToString(),
                                    LastName = reader["last_name"].ToString(),
                                    Actor = new Actor { LoginName = reader ["employ_id"].ToString() }
                                });
                        }
                    }
                }
            }

            return list;
        }
    }
}
