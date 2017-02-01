using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;
using Bling.Domain;
using System.Data.SqlClient;
using System.Data;
using Bling.Domain.Extension;

namespace Bling.Repository.Compliance
{
    public interface IDIRWDataDao
    {
        IList<DIRWData> GetData(string fileid);
        IList<DIRWDropDown> GetLookUp(string state);
        void MarkAsYes(string fileId, string fieldId, string oldData, string actorId, string keyId);
        void SaveField(string fileId, string fieldId, string oldData, string newData, string elevated, string actorid, string keyid);
        IList<String> GetFinal1003Id(string fileid);
        IList<DIRWData> GetFinal1003Data(string fileid);
        string GetCountyInfo(string fileid);
        IList<string> GetUntouchFields(string fileid, string reviewType);
        IList<DIRWData> GetDeniedInitial1003Data(string fileid);
        IList<DIRWData> GetCancelledInitial1003Data(string fileid);
    }

    public class DIRWDataDao : AbstractDao<DIRWData, int>, IDIRWDataDao

    {
        public DIRWDataDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(DataIntegrityFieldDao));            
        }

        public IList<DIRWData> GetData(string fileid)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetDIRWData :fileid")
                .AddEntity(typeof(DIRWData))
                .SetString("fileid", fileid)
                .List<DIRWData>();                
        }

        public void MarkAsYes(string fileId, string fieldId, string oldData, string actorId, string keyId)
        {
            m_session.CreateSQLQuery("exec xGEM_DIRWDataIsYes :fileid, :fieldid, :oldData, :actorid, :keyid")
                .SetString("fileid", fileId)
                .SetString("fieldid", fieldId)
                .SetString("actorid", actorId)
                .SetString("oldData", oldData)
                .SetString("keyid", keyId)
                .ExecuteUpdate();
        }

        public IList<DIRWDropDown> GetLookUp(string state)
        {
            IList<DIRWDropDown> list = new List<DIRWDropDown>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetDIRWLookUp";
                    cmd.Parameters.AddWithValue("state", state);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DIRWDropDown
                        {
                            Id = reader["Id"].ToString().ToInteger(),
                            Key = reader["Key"].ToString(),
                            Value = reader["Value"].ToString()
                        }
                        );
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public void SaveField(string fileId, string fieldId, string oldData, string newData, string elevated, string actorid, string keyid)
        {

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_UpdateDIRWData";
                    cmd.Parameters.AddWithValue("fileid", fileId);
                    cmd.Parameters.AddWithValue("fieldId", fieldId);
                    cmd.Parameters.AddWithValue("oldData", oldData);
                    cmd.Parameters.AddWithValue("newData", newData);
                    cmd.Parameters.AddWithValue("elevated", elevated);
                    cmd.Parameters.AddWithValue("actorid", actorid);
                    cmd.Parameters.AddWithValue("keyid", keyid);

                    cmd.ExecuteNonQuery();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    
                }
            }
        }

        public IList<string> GetFinal1003Id(string fileid)
        {
            return m_session.CreateSQLQuery("exec xGEM_GetFinal1003Id :fileid")
                .SetString("fileid", fileid)
                .List<string>(); 
        }

        public string GetCountyInfo(string fileid)
        {
            string json = "";

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetDIRWCountyInfo";
                    cmd.Parameters.AddWithValue("fileid", fileid);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        json = String.Format(", CountyCode : '{0}', MSACode : '{1}', StateCode : '{2}'",
                            reader["CountyCode"], reader["MSACode"], reader["StateCode"]);
                    }
                    reader.Close();
                }
            }

            return json;
        }

        public IList<DIRWData> GetFinal1003Data(string fileid)
        {
            //return m_session.CreateSQLQuery("exec xGEM_GetDIRWFinal1003Data :fileid")
            //    .AddEntity(typeof(DIRWData))
            //    .SetString("fileid", fileid)
            //    .List<DIRWData>();


            IList<DIRWData> list = new List<DIRWData>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetDIRWFinal1003Data";
                    cmd.Parameters.AddWithValue("fileid", fileid);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DIRWData
                        {
                            Id = reader["Id"].ToString().ToInteger(),
                            CurrentData = reader["CurrentData"].ToString(),
                            OldData = reader["OldData"].ToString(),
                            Elevated = reader["Elevated"].ToString() == "1" ? true : false,
                            YN = reader["YN"].ToString(),
                            KeyId = reader["KeyId"].ToString()
                        }
                        );
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public IList<DIRWData> GetDeniedInitial1003Data(string fileid)
        {
            IList<DIRWData> list = new List<DIRWData>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetDIRWDeniedInitial1003Data";
                    cmd.Parameters.AddWithValue("fileid", fileid);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DIRWData
                        {
                            Id = reader["Id"].ToString().ToInteger(),
                            CurrentData = reader["CurrentData"].ToString(),
                            OldData = reader["OldData"].ToString(),
                            Elevated = reader["Elevated"].ToString() == "1" ? true : false,
                            YN = reader["YN"].ToString(),
                            KeyId = reader["KeyId"].ToString()
                        }
                        );
                    }
                    reader.Close();
                }
            }

            return list;
        }

        public IList<DIRWData> GetCancelledInitial1003Data(string fileid)
        {
            IList<DIRWData> list = new List<DIRWData>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_GetDIRWCancelledInitial1003Data";
                    cmd.Parameters.AddWithValue("fileid", fileid);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new DIRWData
                        {
                            Id = reader["Id"].ToString().ToInteger(),
                            CurrentData = reader["CurrentData"].ToString(),
                            OldData = reader["OldData"].ToString(),
                            Elevated = reader["Elevated"].ToString() == "1" ? true : false,
                            YN = reader["YN"].ToString(),
                            KeyId = reader["KeyId"].ToString()
                        }
                        );
                    }
                    reader.Close();
                }
            }

            return list;
        }


        public IList<string> GetUntouchFields(string fileid, string reviewType)
        {
            IList<string> list = new List<string>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_DIRWGetUntouchFields";
                    cmd.Parameters.AddWithValue("fileid", fileid);
                    cmd.Parameters.AddWithValue("reviewtype", reviewType);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(reader["Description"].ToString());
                    }
                    reader.Close();
                }
            }
            return list;
        }
    }
}
