using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using Bling.Domain;
using NHibernate;
using NHibernate.Criterion;
using System.Data.SqlClient;
using System.Data;

namespace Bling.Repository
{
    public interface IGEMUserDao : IDao<GEMUser, int>
    {
        GEMUser GetCurrentUser();
        GEMUser GetUserByLoginName(string loginName);
        List<GEMUser> GetAllUser();
        GEMUser Delete(string loginName);
        void AddGroup(string loginName, GEMGroup group);
        void RemoveGroup(string loginName, GEMGroup group);
    }

    public class GEMUserDao : AbstractDao<GEMUser, int>, IGEMUserDao
    {
        public GEMUserDao(ISession session) : base(session)
        {            
        }

        public GEMUser GetCurrentUser()
        {
            //return GetUserByLoginName("rbird");
            return GetUserByLoginName(WindowsIdentity.GetCurrent().Name.Split('\\')[1]);
        }

        public GEMUser GetUserByLoginName(string loginName)
        {
            loginName = GetUserMap(loginName);

            return GetGEMUser(loginName);
            //return m_session.CreateCriteria(typeof(GEMUser))
            //    .Add(Expression.Eq("UserName", loginName))
            //    .SetFetchMode("Groups", FetchMode.Eager)
            //    .UniqueResult<GEMUser>();
        }

        public GEMUser Delete(string loginName)
        {
            GEMUser user = GetUserByLoginName(loginName);
            m_session.Delete(user);
            return user;
        }

        public List<GEMUser> GetAllUser()
        {
            //return m_session.CreateCriteria(typeof(GEMUser))
            //    .AddOrder(Order.Asc("UserName"))
            //    .List<GEMUser>().ToList();

            return GetGEMAllUser();
        }

        private List<GEMUser> GetGEMAllUser()
        {
            var list = new List<GEMUser>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetGEMAllUser";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new GEMUser
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                UserName = reader["Username"].ToString(),
                                ActorId = reader["ActorId"].ToString(),
                                EmployId = reader["EmployId"].ToString(),
                                UserInfo = new UserInfo
                                {
                                    FirstName = reader["Firstname"].ToString(),
                                    LastName = reader["Lastname"].ToString(),
                                    EMail = reader["Email"].ToString()
                                }
                            });
                        }
                    }
                }
            }

            return list;
        }

        private GEMUser GetGEMUser(string username)
        {
            var gemUser = new GEMUser();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetGEMUser";
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            gemUser = new GEMUser
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                UserName = reader["Username"].ToString(),
                                ActorId = reader["ActorId"].ToString(),
                                EmployId = reader["EmployId"].ToString(),
                                UserInfo = new UserInfo
                                {
                                    FirstName = reader["Firstname"].ToString(),
                                    LastName = reader["Lastname"].ToString(),
                                    EMail = reader["Email"].ToString(),
                                    EmployId = reader["Username"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            gemUser.Groups = GetGEMUserGroup(username);
            return gemUser;
        }

        private List<GEMGroup> GetGEMUserGroup(string username)
        {
            var gemGroups = new List<GEMGroup>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetGEMUserGroup";
                    cmd.Parameters.AddWithValue("@username", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int groupId = Convert.ToInt32(reader["GroupId"].ToString());
                            int applicationId = Convert.ToInt32(reader["ApplicationId"].ToString());
                            string groupName = reader["GroupName"].ToString();

                            var exist = gemGroups.Exists(x => x.Id == groupId);
                            if (exist)
                            {
                                var group = gemGroups.Find(x => x.Id == groupId);
                                group.Applications.Add(new GEMApplication{
                                    Id = applicationId
                                });
                            }
                            else
                            {
                                var gemApplication = new GEMApplication
                                {
                                    Id = applicationId
                                };

                                var gemApplications = new List<GEMApplication>();
                                gemApplications.Add(gemApplication);

                                gemGroups.Add(new GEMGroup 
                                { 
                                    Id = groupId,
                                    GroupName = groupName,
                                    Applications = gemApplications
                                });
                            }
                        }
                    }
                }
            }

            return gemGroups;
        }

        public void AddGroup(string loginName, GEMGroup group)
        {
            GEMUser user = GetUserByLoginName(loginName);
            //user.Groups.Add(group);

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_GEMCentral_AddGroup";
                    cmd.Parameters.AddWithValue("@UserId", user.Id);
                    cmd.Parameters.AddWithValue("@GroupId", group.Id);
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveGroup(string loginName, GEMGroup group)
        {
            GEMUser user = GetUserByLoginName(loginName);
            //List<GEMGroup> groups = new List<GEMGroup>(user.Groups);

            //groups.ForEach(g =>
            //{
            //    if (group.Id == g.Id)
            //        user.Groups.Remove(g);
            //});

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xReport_GEMCentral_RemoveGroup";
                    cmd.Parameters.AddWithValue("@UserId", user.Id);
                    cmd.Parameters.AddWithValue("@GroupId", group.Id);
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private string GetUserMap(string loginName)
        {
            string username = m_session.CreateSQLQuery("exec xGEM_GetUserMap :loginName ")
                .SetString("loginName", loginName)
                .UniqueResult<string>();

            return String.IsNullOrEmpty(username) ? loginName : username;
        }
    }
}
