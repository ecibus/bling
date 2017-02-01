using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;
using System.Data.SqlClient;
using System.Data;
using Bling.Domain.Extension;

namespace Bling.Repository.Compliance
{
    public interface IDataIntegrityGroupDao : IDao<DataIntegrityGroup, int>
    {
        IList<DataIntegrityGroup> GetAllGroupFor(string reviewType);
        IList<DataIntegrityGroup> GetGroupForClosedLoans();
        IList<DataIntegrityGroup> GetGroupForDeniedInitial1003();
    }

    public class DataIntegrityGroupDao : AbstractDao<DataIntegrityGroup, int>, IDataIntegrityGroupDao
    {
        public DataIntegrityGroupDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(DataIntegrityFieldDao));            
        }

        public IList<DataIntegrityGroup> GetGroupForClosedLoans()
        {
            return m_session.CreateQuery("from DataIntegrityGroup g order by g.OrderBy ")
                .List<DataIntegrityGroup>();
        }

        public IList<DataIntegrityGroup> GetGroupForDeniedInitial1003()
        {
            return m_session.CreateQuery("from DataIntegrityGroup g where order by g.OrderBy ")
                .List<DataIntegrityGroup>();
        }

        public IList<DataIntegrityGroup> GetAllGroupFor(string reviewType)        
        {
            //return m_session.CreateQuery("from DataIntegrityGroup g order by g.OrderBy ")
            //    .List<DataIntegrityGroup>();

            List<DataIntegrityGroup> list = new List<DataIntegrityGroup>();

            using (var cn = new SqlConnection(DMDDataConnectionString))
            {
                using (var cmd = new SqlCommand { Connection = cn })
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "xGEM_DIRWGetFields";
                    cmd.Parameters.AddWithValue("type", reviewType);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int groupId = reader["Id"].ToString().ToInteger();
                        if (!list.Exists(x => x.Id == groupId))
                        {
                            list.Add(new DataIntegrityGroup
                                {
                                    Id = groupId,
                                    GroupName = reader["GroupName"].ToString(),
                                    OrderBy = reader["OrderBy"].ToString().ToInteger(),
                                    Fields = new List<DataIntegrityField>()
                                }
                            );
                        }

                        var group = list.Find(x => x.Id == groupId);
                        group.Fields.Add(new DataIntegrityField
                        {
                            Id = reader["FieldId"].ToString().ToInteger(),
                            Description = reader["Description"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            TargetTable = reader["TargetTable"].ToString(),
                            TargetField = reader["TargetField"].ToString(),
                            DisplayAs = reader["DisplayAs"].ToString(),
                            LinkTable = reader["LinkTable"].ToString(),
                            LinkField = reader["LinkField"].ToString(),
                            LinkId = reader["LinkId"].ToString(),
                            LinkCriteria = reader["LinkCriteria"].ToString(),
                            Field = reader["Field"].ToString(),
                            ExtraUpdateSP = reader["ExtraUpdateSP"].ToString(),
                            ExtraTable = reader["ExtraTable"].ToString(),
                            ExtraField = reader["ExtraField"].ToString(),
                            ExtraCriteria = reader["ExtraCriteria"].ToString(),
                            Include =  reader["Include"].ToString() == "True" ? true : false
                        }
                        );
                    }
                    reader.Close();
                }
            }

            return list;
        }
    }
}
