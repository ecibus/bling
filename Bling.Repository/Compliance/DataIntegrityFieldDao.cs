using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Compliance;
using NHibernate;
using log4net;
using System.Collections;
using Bling.Domain;
using Bling.Domain.Extension;

namespace Bling.Repository.Compliance
{
    public interface IDataIntegrityFieldDao : IDao<DataIntegrityField, int>
    {
        string UpdateField(string fieldId, string fileid, string newData, string oldData, string keyid);
        Translog GetTransLog(string fieldId, string fileId, string actorId, string newValue, string dropdownText);
        //void UpdateAPRFromTILExtra(string fileId, string actorId);
        void ExtraUpdateSP(string storedProc, string fileId, string actorId);
    }

    public class DataIntegrityFieldDao : AbstractDao<DataIntegrityField, int>, IDataIntegrityFieldDao
    {
        public DataIntegrityFieldDao(ISession session)
            : base (session)
        {
            m_logger = LogManager.GetLogger(typeof(DataIntegrityFieldDao));            
        }

        public string UpdateField(string fieldId, string fileid, string newData, string oldData, string keyid)
        {
            if (newData == oldData)
                return "";

            DataIntegrityField field = GetById(fieldId.ToInteger());
            if (field == null)
            {
                m_logger.DebugFormat("Could not find FieldId {0}", fieldId);
                throw new ApplicationException(String.Format("Could not find FieldId {0}", fieldId));
            }

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Set ROWCOUNT 1 ");
            sql.AppendFormat("Update TOP (1) {0} ", String.IsNullOrEmpty(keyid) ? field.TargetTable : field.ExtraTable);
            sql.AppendFormat("Set {0} = '{1}' ", String.IsNullOrEmpty(keyid) ? field.TargetField : field.ExtraField, newData);
            sql.AppendFormat("Where file_id = '{0}' ", fileid);
            if (!String.IsNullOrEmpty(keyid))
            {
                sql.AppendFormat("and {0} and {1}='{2}' ", field.ExtraCriteria, field.ExtraId, keyid);
            }
            sql.AppendFormat("Set ROWCOUNT 0 ");

            m_session.CreateSQLQuery(sql.ToString())
                .ExecuteUpdate();

            return String.IsNullOrEmpty(keyid) ? field.ExtraUpdateSP : "";
        }

        public Translog GetTransLog(string fieldId, string fileId, string actorId, string newValue, string dropdownText)
        {
            DataIntegrityField field = GetById(fieldId.ToInteger());
            if (field == null)
            {
                m_logger.DebugFormat("Could not find FieldId {0}", fieldId);
                throw new ApplicationException(String.Format("Could not find FieldId {0}", fieldId));
            }
            if (String.IsNullOrEmpty(field.Field))
                return null;

            string oldValue = "";

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Select top 1 {0} ", GetSelectField(field));
            sql.AppendFormat("From {0} ", GetFromTable(field));
            sql.AppendFormat("Where ");
            sql.AppendFormat("   {2}.file_id = '{0}' {1} ", fileId, GetWhere(field), field.TargetTable);
            
            oldValue = m_session.CreateSQLQuery(sql.ToString())                
                .UniqueResult<string>();
            oldValue = oldValue ?? "";

            Translog translog = new Translog
            {
                FileId = fileId,
                ActorId = actorId,
                Field = field.Field,
                OldValue = oldValue,
                NewValue = field.DisplayAs.ToLower() == "dropdown" ? dropdownText : newValue,
                ChangeDate = DateTime.Now
            };

            return translog;
        }

        public void ExtraUpdateSP(string storedProc, string fileId, string actorId)
        {
            m_session.CreateSQLQuery("exec " + storedProc + " :fileid, :actorid")
               .SetString("fileid", fileId)
               .SetString("actorid", actorId)
               .ExecuteUpdate();
        }

        private string GetSelectField(DataIntegrityField field)
        {
            string selectField = "";

            switch (field.DisplayAs.ToLower())
            {
                case "calendar":
                    selectField = String.Format("convert(varchar, {0}, 101) ", field.TargetField);
                    break;

                case "dropdown":
                    selectField = String.Format("{0}.{1}", field.LinkTable, field.LinkField);
                    break;

                case "number":
                    selectField = String.Format("CONVERT(varchar(10),{0},101) ", field.TargetField);
                    break;

                default:
                    selectField = String.Format("CAST({0} as varchar)", field.TargetField);
                    break;
            }

            return selectField;
        }

        private string GetFromTable(DataIntegrityField field)
        {
            string table = "";
            switch (field.DisplayAs.ToLower())
            {                
                case "dropdown":
                    table = String.Format("{0} left join {1} on {0}.{2} = {1}.{3}", 
                        field.TargetTable, field.LinkTable, field.TargetField, field.LinkId
                        );
                    break;

                default:
                    table = field.TargetTable;
                    break;
            }
            return table;

        }

        private string GetWhere(DataIntegrityField field)
        {
            string where = "";

            switch (field.DisplayAs.ToLower())
            {
                case "dropdown":
                    where = String.Format("and {0} ", field.LinkCriteria);
                    break;

                default:
                    break;
            }
            return where;
        }



    }
}
