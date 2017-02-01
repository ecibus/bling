using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;
using Bling.Domain.Compliance;
using Bling.Domain.Extension;
using Bling.Domain;
using Bling.Repository;
using log4net;

namespace Bling.Presenter.Compliance
{
    public class AjaxDIRWPresenter : Presenter
    {
        private IAjaxView m_View;
        private IDIRWLoanInfoDao m_LoanInfoDao;
        private IDataIntegrityGroupDao m_GroupDao;
        private IDIRWDataDao m_DataDao;
        private IDataIntegrityFieldDao m_FieldDao;
        private ITranslogDao m_TransLogDao;
        private IDataIntegrityReviewDao m_ReviewDao;
        

        public AjaxDIRWPresenter(IAjaxView view) :
            this(view, new DIRWLoanInfoDao(DMDDataSession()), new DataIntegrityGroupDao(DMDDataSession()),
                new DIRWDataDao(DMDDataSession()), new DataIntegrityFieldDao(DMDDataSession()),
                new TranslogDao(DMDDataSession()), new DataIntegrityReviewDao(DMDDataSession())
            )
        {
        }

        public AjaxDIRWPresenter(IAjaxView view, IDIRWLoanInfoDao loanInfoDao, IDataIntegrityGroupDao groupDao, 
            IDIRWDataDao dataDao, IDataIntegrityFieldDao fieldDao, ITranslogDao translogDao, IDataIntegrityReviewDao reviewDao)
        {
            m_View = view;
            m_LoanInfoDao = loanInfoDao;
            m_GroupDao = groupDao;
            m_DataDao = dataDao;
            m_FieldDao = fieldDao;
            m_TransLogDao = translogDao;
            m_ReviewDao = reviewDao;

            m_logger = LogManager.GetLogger(typeof (AjaxDIRWPresenter));
        }

        public void Load(string loanNumber)
        {
            m_logger.DebugFormat("Load {0}", loanNumber);

            try
            {
                DIRWLoanInfo loan = m_LoanInfoDao.GetLoanInfo(loanNumber);

                if (loan == null)
                {
                    m_View.ResponseText = String.Format("{{ Message : 'Could not find Loan Number {0}, please try again.' }}", loanNumber);
                    return;
                }

                if (String.IsNullOrEmpty(loan.FundedDate))
                {
                    //m_View.ResponseText = String.Format("{{ Message : 'Loan {0} is not closed yet, please try another loan.' }}", loanNumber);
                    //return;
                }

                m_View.ResponseText = loan.ToJson();
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void LoadGroup(string fileid, string state, string reviewType)
        {
            m_logger.DebugFormat("LoadGroup {0}", fileid);

            try
            {
                IList<DataIntegrityGroup> group = m_GroupDao.GetAllGroupFor(reviewType);
                IList<DIRWData> datum = m_DataDao.GetData(fileid);
                IList<DIRWDropDown> dropdown = m_DataDao.GetLookUp(state);
                DataIntegrityReview review = m_ReviewDao.GetByFileId(fileid);
                
                StringBuilder html = new StringBuilder();
                html.AppendFormat("<b>Yes/No</b><br />");
                //group.ToList()
                //    .ForEach(x => html.Append(x.ToHTML(datum, dropdown, "")));

                foreach (var g in group)
                {
                    html.Append(g.ToHTML(datum, dropdown, ""));
                    if (g.Id == 7)
                    {
                        ;
                        html.Append(LoadFinal1003(fileid));
                    }

                    if (reviewType.ToLower() == "denied loan" && g.Id == 2)
                    {
                        html.Append(LoadDeniedInitial1003(fileid));
                    }

                    if (reviewType.ToLower() == "cancelled loan" && g.Id == 2)
                    {
                        html.Append(LoadCancelledInitial1003(fileid));
                    }
                }

                html.AppendFormat("<h2>Notes:</h2>");
                html.AppendFormat("<textarea id=\"txtNotes\" cols=\"60\" rows=\"4\">{0}</textarea><br />", review == null ? "" : review.Notes);
                html.AppendFormat("<input id=\"btnSave\" type=\"button\" value=\"Save\" />");

                m_View.ResponseText = html.ToString();
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public string LoadFinal1003(string fileid)
        {
            m_logger.DebugFormat("LoadFinal1003 {0}", fileid);
            StringBuilder html = new StringBuilder();
            try
            {
                IList<string> ids = m_DataDao.GetFinal1003Id(fileid);
                IList<DIRWData> datum = m_DataDao.GetFinal1003Data(fileid);
                IList<DIRWDropDown> dropdown = m_DataDao.GetLookUp("zz");
                DataIntegrityGroup final1003 = m_GroupDao.GetById(7);

                //DataIntegrityField occ = final1003.Fields.Where(x => x.Id == 48).Single();
                //final1003.Fields.Remove(occ);

                foreach (var id in ids)
                {
                    html.AppendFormat(final1003.ToHTML(datum, dropdown, id));
                }
                html.Replace("<div id=\"Extra_7\"></div>", "");
                                
                //m_View.ResponseText = html.ToString();

                //final1003.Fields.Add(occ);
                //m_View.ResponseText = "<div id='extrafinal1003'></div>";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
            return html.ToString();
        }

        public string LoadCancelledInitial1003(string fileid)
        {
            m_logger.DebugFormat("LoadCancelledFinal1003 {0}", fileid);
            StringBuilder html = new StringBuilder();
            try
            {
                IList<string> ids = m_DataDao.GetFinal1003Id(fileid);
                IList<DIRWData> datum = m_DataDao.GetCancelledInitial1003Data(fileid);
                IList<DIRWDropDown> dropdown = m_DataDao.GetLookUp("zz");
                DataIntegrityGroup initial1003 = m_GroupDao.GetAllGroupFor("cancelled loan").Where(x => x.Id == 2).FirstOrDefault();
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 40));
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 14));
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 48));
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 20));
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 29));
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 3));
                foreach (var id in ids)
                {
                    html.AppendFormat(initial1003.ToHTML(datum, dropdown, id));
                }

            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
            return html.ToString();
        }

        public string LoadDeniedInitial1003(string fileid)
        {
            m_logger.DebugFormat("LoadDenied1003 {0}", fileid);
            StringBuilder html = new StringBuilder();
            try
            {
                IList<string> ids = m_DataDao.GetFinal1003Id(fileid);
                IList<DIRWData> datum = m_DataDao.GetDeniedInitial1003Data(fileid);
                IList<DIRWDropDown> dropdown = m_DataDao.GetLookUp("zz");
                //DataIntegrityGroup initial1003 = m_GroupDao.GetById(7);
                DataIntegrityGroup initial1003 = m_GroupDao.GetAllGroupFor("denied loan").Where(x => x.Id == 2).FirstOrDefault();
                initial1003.Fields.Remove(initial1003.Fields.First(x => x.Id == 3));
                foreach (var id in ids)
                {
                    html.AppendFormat(initial1003.ToHTML(datum, dropdown, id));
                }
                //html.Replace("<div id=\"Extra_7\"></div>", "");

            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
            return html.ToString();
        }

        public void SaveField(string fileId, string fieldId, string oldData, string newData, string elevated, string dropdownText, string actorid, string keyid)
        {
            m_logger.DebugFormat("SaveField {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                fileId, fieldId, oldData, newData, elevated, dropdownText, actorid, keyid);
            try
            {
                m_DataDao.SaveField(fileId, fieldId, oldData, newData, elevated, actorid, keyid);

                if (newData.ToLower() == "undefined")
                {
                    m_View.ResponseText = String.Format("{{ FieldId : '{0}' }}", fieldId);
                    return;
                }

                if (keyid == String.Empty)
                {
                    Translog translog = m_FieldDao.GetTransLog(fieldId, fileId, actorid, newData, dropdownText);
                    if (translog != null)
                    {
                        if (translog.OldValue != translog.NewValue)
                        {
                            //m_TransLogDao.Save(translog);
                            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["dmddata"].ConnectionString))
                            {
                                cn.Open();

                                using (SqlCommand cmd = new SqlCommand("xGEM_UpdateTranslog", cn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@fileID", translog.FileId);
                                    cmd.Parameters.AddWithValue("@actorID", translog.ActorId);
                                    cmd.Parameters.AddWithValue("@field", translog.Field);
                                    cmd.Parameters.AddWithValue("@oldVal", translog.OldValue);
                                    cmd.Parameters.AddWithValue("@newVal", translog.NewValue);
                                    cmd.Parameters.AddWithValue("@changdate", translog.ChangeDate.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                string extraSPName = m_FieldDao.UpdateField(fieldId, fileId, newData, oldData, keyid);
                if (!String.IsNullOrEmpty(extraSPName))
                    m_FieldDao.ExtraUpdateSP(extraSPName, fileId, actorid);


                string json = String.Format("{{ FieldId : '{0}', NewData : '{1}', OldData : '{2}' {3} }}", 
                    fieldId, String.IsNullOrEmpty(dropdownText) ? newData : dropdownText, oldData, GetCountyInfo(fileId, fieldId));
                m_View.ResponseText = json;
                m_logger.DebugFormat("SaveField Response: {0}", json);
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}', FieldId : '{1}' }}", ex.Message.Escape(), fieldId);
                //throw new ApplicationException(ex.Message.Escape());
            }
        }

        private string GetCountyInfo(string fileId, string fieldId)
        {
            string json = "";
            if (fieldId == "15")
            {
                json = m_DataDao.GetCountyInfo(fileId);
            }
            return json;
        }

        public void MarkAsYes(string fileId, string fieldId, string oldValue, string actorId, string keyId)
        {
            m_logger.DebugFormat("MarkAsYes {0}, {1}, {2}, {3}, {4}", fileId, fieldId, oldValue, actorId, keyId);
            try
            {
                m_DataDao.MarkAsYes(fileId, fieldId, oldValue, actorId, keyId);
                m_View.ResponseText = String.Format("{{ FieldId : '{0}' }}", fieldId);
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void SaveReview(string fileId, string actorId, string notes, string reviewType)
        {
            m_logger.DebugFormat("SaveReview {0}, {1}, {2}", fileId, actorId, notes);
            try
            {

                IList<string> untouched = m_DataDao.GetUntouchFields(fileId, reviewType);
                if (untouched.Count() != 0)
                {
                    StringBuilder message = new StringBuilder();
                    untouched.ToList().ForEach(x => message.AppendFormat("{0}, ", x.Substring(4)));
                    m_View.ResponseText = String.Format("{{ Untouched : 'Please review the following fields: {0}.' }}", 
                        message.ToString().Substring(0, message.Length -2));
                    return;
                }

                DataIntegrityReview review = m_ReviewDao.GetByFileId(fileId);
                if (review == null)
                    m_ReviewDao.Save(new DataIntegrityReview { FileId = fileId, ActorId = actorId, CreatedOn = DateTime.Now, Notes = notes });
                else
                {
                    review.ActorId = actorId;
                    review.CreatedOn = DateTime.Now;
                    review.Notes = notes;
                    m_ReviewDao.Save(review);
                }
                m_View.ResponseText = String.Format("{{ FileId : '{0}' }}", fileId.Escape());
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void SaveTranslog(Translog t)
        {

        }

    }
}
