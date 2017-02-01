using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;
using Bling.Domain;
using Bling.Repository;
using Bling.Domain.Compliance;

namespace Bling.Presenter.Compliance
{
    public class AjaxAuditScoreCardPresenter : Presenter
    {
        private IAjaxView m_View;
        private IAuditScoreCardDao m_Dao;
        private IAuditScoreCardScoreDao m_ScoreDao;
        private ITranslogDao m_TransLogDao;
        private IAuditScoreCardCommentDao m_CommentDao;
        private IAuditScoreCardItemTypeDao m_ItemTypeDao;

        public AjaxAuditScoreCardPresenter(IAjaxView view)
            : this(view, new AuditScoreCardDao(DMDDataSession()), new AuditScoreCardScoreDao(DMDDataSession()), 
              new AuditScoreCardCommentDao(DMDDataSession()), new AuditScoreCardItemTypeDao(DMDDataSession()), new TranslogDao(DMDDataSession()))
        {
        }

        public AjaxAuditScoreCardPresenter(IAjaxView view, IAuditScoreCardDao dao, IAuditScoreCardScoreDao scoreDao, 
            IAuditScoreCardCommentDao commentDao, IAuditScoreCardItemTypeDao itemTypeDao, ITranslogDao translogDao)
        {
            m_View = view;
            m_Dao = dao;
            m_ScoreDao = scoreDao;
            m_CommentDao = commentDao;
            m_ItemTypeDao = itemTypeDao;
            m_TransLogDao = translogDao;
        }

        public void SaveScore(string fileId, int scoreId, double score, string createdBy)
        {
            try
            {
                AuditScoreCardScore scoreCard = new AuditScoreCardScore
                {
                    FileId = fileId.Replace("&lt;", "<").Replace("&gt;", ">"),
                    ScoreId = scoreId,
                    Score = score,
                    CreatedBy = createdBy
                };
                m_ScoreDao.SaveScore(scoreCard);
                m_View.ResponseText = String.Format("{{ SubTotal : {0}}} ", GetSubtotal(fileId));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void RemoveScore(string fileId, int scoreId)
        {
            try
            {
                m_ScoreDao.RemoveByFileIdAndScoreId(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), scoreId);
                m_View.ResponseText = String.Format("{{ SubTotal : {0}}} ", GetSubtotal(fileId));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void GetLoanInfo(string loanNumber)
        {
            try
            {
                var loanInfo = m_Dao.GetLoanInfo(loanNumber);

                if (loanInfo == null)
                {
                    m_View.ResponseText = String.Format("{{ Message : 'Could not find Loan Number <b>{0}</b>, please try again.' }}", loanNumber);
                    return;
                }

                m_View.ResponseText = loanInfo.ToJson(GetSubtotal(loanInfo.FileId), GetNoFindings(loanInfo.FileId),
                    GetOtherScore(loanInfo.FileId), GetComment(loanInfo.FileId), GetItemType(loanInfo.FileId));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void PushNotesToDataTrac(string fileId, string actorId) //, string notes)
        {
            try
            {
                m_Dao.PushNotesInDataTrac(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), actorId);
                m_View.ResponseText = String.Format("{{  }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void AddCategoryWithNoFindings(string fileId, int groupId, string createdBy)
        {
            try
            {
                m_ScoreDao.CategoryHasNoFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), groupId, createdBy);
                m_View.ResponseText = String.Format("{{ 'Id' : '{0}' }}", groupId);
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void RemoveCategoryWithFindings(string fileId, int groupId)
        {
            try
            {
                m_ScoreDao.CategoryHasFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), groupId);
                m_View.ResponseText = String.Format("{{ 'Id' : '{0}' }}", groupId);
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveCommentAndItemType(string fileId, int itemId, string text, string itemType, string createdBy)
        {
            try
            {
                AuditScoreCardComment comment = new AuditScoreCardComment { FileId = fileId.Replace("&lt;", "<").Replace("&gt;", ">"), ItemId = itemId, Comment = text, CreatedBy = createdBy };
                m_CommentDao.SaveComment(comment);

                AuditScoreCardItemType itemtype = new AuditScoreCardItemType { FileId = fileId.Replace("&lt;", "<").Replace("&gt;", ">"), ItemId = itemId, ItemType = itemType, CreatedBy = createdBy };
                m_ItemTypeDao.SaveItemType(itemtype);

                m_View.ResponseText = String.Format("{{ 'Id' : '{0}' }}", itemId);

            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveInitialAuditorAndDate(string loanNumber, string initialAuditor, string initialAuditorValue, string auditDate, string submittedDate, string actorId)
        {
            try
            {
                var loanInfo = m_Dao.GetLoanInfo(loanNumber);

                if (loanInfo == null)
                {
                    m_View.ResponseText = String.Format("{{ Message : 'Could not find Loan Number <b>{0}</b>, please try again.' }}", loanNumber);
                    return;
                }

                UpdateInitialAuditorAndDate(loanInfo, initialAuditor, initialAuditorValue, auditDate, actorId);
                UpdateSubmittedDate(loanInfo, submittedDate, actorId);

                if (loanInfo.LinkedLoanNumber != "&nbsp;")
                {
                    var linkedLoanInfo = m_Dao.GetLoanInfo(loanInfo.LinkedLoanNumber);
                    UpdateInitialAuditorAndDate(linkedLoanInfo, initialAuditor, initialAuditorValue, auditDate, actorId);
                }

                m_View.ResponseText = loanInfo.ToJson(GetSubtotal(loanInfo.FileId), GetNoFindings(loanInfo.FileId), 
                    GetOtherScore(loanInfo.FileId), GetComment(loanInfo.FileId), GetItemType(loanInfo.FileId));
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        private void UpdateSubmittedDate(AuditScoreCardLoanInfo loanInfo, string submittedDate, string actorId)
        {
            if (loanInfo.SubmittedDate != submittedDate)
            {
                Translog sdLog = new Translog
                {
                    FileId = loanInfo.FileId,
                    Field = "Submission Date",
                    ActorId = actorId,
                    OldValue = loanInfo.SubmittedDate,
                    NewValue = submittedDate,
                    ChangeDate = DateTime.Now
                };

                m_TransLogDao.Save(sdLog);

                m_Dao.UpdateSubmittedDate(loanInfo.FileId, loanInfo.SubmittedDate, submittedDate);
            }
        }

        private void UpdateInitialAuditorAndDate(AuditScoreCardLoanInfo loanInfo, string initialAuditor, string initialAuditorValue, string auditDate, string actorId)
        {
            
            if (loanInfo.InitialAuditor != initialAuditor)
            {
                Translog initialAuditorLog = new Translog
                {
                    FileId = loanInfo.FileId,
                    Field = "Initial Auditor",
                    ActorId = actorId,
                    OldValue = loanInfo.InitialAuditorValue.ToUpper(),
                    NewValue = initialAuditorValue.ToUpper(),
                    ChangeDate = DateTime.Now
                };

                m_TransLogDao.Save(initialAuditorLog);

                m_Dao.UpdateCustomData(loanInfo.FileId, "shp_12", initialAuditor);
            }

            if (loanInfo.AuditDate != auditDate)
            {
                Translog auditDateLog = new Translog
                {
                    FileId = loanInfo.FileId,
                    Field = "Audit Date",
                    ActorId = actorId,
                    OldValue = String.IsNullOrEmpty(loanInfo.AuditDate) ? "  /  /  " : loanInfo.AuditDate,
                    NewValue = auditDate == "" ? "  /  /  " : auditDate,
                    ChangeDate = DateTime.Now
                };

                m_TransLogDao.Save(auditDateLog);

                m_Dao.UpdateCustomData(loanInfo.FileId, "shp_11", auditDate == "" ? "  /  /  " : auditDate);
            }

        }


        private string GetSubtotal(string fileId)
        {
            IDictionary<string, double> scores = m_ScoreDao.GetGroupScore(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
            StringBuilder subtotal = new StringBuilder();
            subtotal.AppendFormat("[ ");
            foreach (var score in scores)
            {
                subtotal.AppendFormat("{{ GroupId : {0}, Score : {1:0.00} }},", score.Key, score.Value);
            }
            if (scores.Count > 0)
                subtotal.Remove(subtotal.Length - 1, 1);
            subtotal.AppendFormat(" ]");
            return subtotal.ToString();
        }

        private string GetNoFindings(string fileId)
        {
            IDictionary<string, double> scores = m_ScoreDao.GetNoFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
            StringBuilder subtotal = new StringBuilder();
            subtotal.AppendFormat("[ ");
            foreach (var score in scores)
            {
                //subtotal.AppendFormat("{{ GroupId : {0}, Score : {1:0.00} }},", score.Key, score.Value);
                subtotal.AppendFormat("{0} ,", score.Key);
            }
            if (scores.Count > 0)
                subtotal.Remove(subtotal.Length - 1, 1);
            subtotal.AppendFormat(" ]");
            return subtotal.ToString();
        }

        private string GetOtherScore(string fileId)
        {
            IDictionary<string, double> scores = m_ScoreDao.GetOtherScore(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
            StringBuilder subtotal = new StringBuilder();
            subtotal.AppendFormat("[ ");
            foreach (var score in scores)
            {
                subtotal.AppendFormat("{{ ScoreId : {0}, Score : {1:0.00} }},", score.Key, score.Value);
            }
            if (scores.Count > 0)
                subtotal.Remove(subtotal.Length - 1, 1);
            subtotal.AppendFormat(" ]");
            return subtotal.ToString();
        }

        private string GetComment(string fileId)
        {
            IDictionary<string, string> comments = m_CommentDao.GetComment(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
            StringBuilder json = new StringBuilder();
            json.AppendFormat("[ ");
            foreach (var comment in comments)
            {
                json.AppendFormat("{{ ItemId : {0}, Comment : '{1}' }},", comment.Key, comment.Value.Replace("'", "\\'"));
            }
            if (comments.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.AppendFormat(" ]");
            return json.ToString();
        }

        private string GetItemType(string fileId)
        {
            IDictionary<string, string> itemTypes = m_ItemTypeDao.GetItemType(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
            StringBuilder json = new StringBuilder();
            json.AppendFormat("[ ");
            foreach (var itemtype in itemTypes)
            {
                json.AppendFormat("{{ ItemId : {0}, ItemType : '{1}' }},", itemtype.Key, itemtype.Value.Replace("'", "\\'"));
            }
            if (itemTypes.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.AppendFormat(" ]");
            return json.ToString();
        }

        
    }
}
