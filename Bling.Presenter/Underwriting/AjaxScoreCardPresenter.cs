using System;
using System.Linq;
using Bling.Repository.Underwriting;
using System.Text;
using Bling.Domain.Underwriting;
using System.Collections.Generic;
using Bling.Domain;

namespace Bling.Presenter.Underwriting
{
    public class AjaxScoreCardPresenter : Presenter
    {
        private IAjaxView m_View;
        private IScoreCardLoanInfoDao m_LoanInfoDao;
        private IScoreCardDao m_ScoreCardDao;
        private IScoreCardCommentDao m_ScoreCardCommentDao;

        public AjaxScoreCardPresenter(IAjaxView view) : 
            this (view, new ScoreCardLoanInfoDao(DMDDataSession()), new ScoreCardDao(DMDDataSession()), new ScoreCardCommentDao(DMDDataSession()))        
        {            
        }

        public AjaxScoreCardPresenter(IAjaxView view, IScoreCardLoanInfoDao loanInfoDao, IScoreCardDao scoreCardDao, IScoreCardCommentDao commentDao)
        {
            m_View = view;
            m_LoanInfoDao = loanInfoDao;
            m_ScoreCardDao = scoreCardDao;
            m_ScoreCardCommentDao = commentDao;
        }

        public void SaveComment(string fileId, int groupId, string text, string createdBy)
        {
            try
            {
                ScoreCardComment comment = new ScoreCardComment { FileId = fileId.Replace("&lt;", "<").Replace("&gt;", ">"), GroupId = groupId, Comment = text, CreatedBy = createdBy };
                m_ScoreCardCommentDao.SaveComment(comment);                
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveScore(string fileId, int scoreId, double score, string createdBy)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard { FileId = fileId.Replace("&lt;", "<").Replace("&gt;", ">"), 
                    ScoreId = scoreId, Score = score, CreatedBy = createdBy };
                m_ScoreCardDao.SaveScore(scoreCard);
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
                m_ScoreCardDao.RemoveByFileIdAndScoreId(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), scoreId);
                m_View.ResponseText = String.Format("{{ SubTotal : {0}}} ", GetSubtotal(fileId));
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
                m_LoanInfoDao.CategoryHasNoFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), groupId, createdBy);
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
                m_LoanInfoDao.CategoryHasFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), groupId);
                m_View.ResponseText = String.Format("{{ 'Id' : '{0}' }}", groupId);
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void AddPerfectLoan(string fileId, string createdBy)
        {
            try
            {
                m_LoanInfoDao.MakeALoanPerfect(fileId.Replace("&lt;", "<").Replace("&gt;", ">"), createdBy);
                m_View.ResponseText = String.Format("{{  }}", "");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void RemovePerfectLoan(string fileId)
        {
            try
            {
                m_LoanInfoDao.MakeALoanNotPerfect(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
                m_View.ResponseText = String.Format("{{  }}", "");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void Load(string loanNumber)
        {
            try
            {                
                ScoreCardLoanInfo loan = m_LoanInfoDao.GetByLoanNumber(loanNumber);

                if (loan == null)
                {
                    m_View.ResponseText = String.Format("{{ Message : 'Could not find Loan Number {0}, please try again.' }}", loanNumber);
                    return;
                }
                                
                StringBuilder scoreIds = new StringBuilder();
                scoreIds.Append("[ ");
                loan.ScoreIds.ToList()
                    .ForEach(i => scoreIds.AppendFormat("'{0}',", i));
                if (loan.ScoreIds.Count > 0)
                    scoreIds.Remove(scoreIds.Length - 1, 1);                
                scoreIds.Append(" ]");

                StringBuilder json = new StringBuilder();
                json.Append("{ ");
                json.AppendFormat("LoanNumber : '{0}', ", loan.LoanNumber);
                json.AppendFormat("Borrower : \"{0}\", ", loan.Borrower.Replace("'", "\'"));
                json.AppendFormat("Underwriter : \"{0}\", ", loan.Underwriter.Replace("'", "\'"));
                json.AppendFormat("LoanOfficer : \"{0}\", ", loan.LoanOfficer.Replace("'", "\'"));
                json.AppendFormat("Processor : \"{0}\", ", loan.Processor.Replace("'", "\'"));
                json.AppendFormat("Is203K : {0}, ", loan.Is203K ? "true" : "false");
                json.AppendFormat("IsPerfect : {0}, ", loan.IsPerfect ? "true" : "false");
                json.AppendFormat("FileId : '{0}', ", loan.FileId).Replace("\\", "\\\\").Replace("<", "&lt;").Replace(">", "&gt;");
                json.AppendFormat("ScoreIds : {0}, ", scoreIds.ToString());
                json.AppendFormat("SubTotal : {0}, ", GetSubtotal(loan.FileId));
                json.AppendFormat("NoFindings : {0}, ", GetNoFindings(loan.FileId));
                json.AppendFormat("Comments : {0}, ", GetComment(loan.FileId));
                json.AppendFormat("Other : {0} ", GetOtherScore(loan.FileId));
                json.Append(" }");

                m_View.ResponseText = json.ToString();
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }


        public void PrintPreview(string reportName, string pdfName, string loanNumber)
        {
            try
            {
                new Crystal(reportName)
                   .ConnectToDataDepot()
                   .SetDestinationToPDFAndRename(reportName, pdfName)
                   .AddParameter("@loan_num", loanNumber)
                   .ViewReport();
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        private string GetOtherScore(string fileId)
        {
            IDictionary<string, double> scores = m_ScoreCardDao.GetOtherScore(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
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

        private string GetSubtotal(string fileId)
        {
            IDictionary<string, double> scores = m_ScoreCardDao.GetGroupScore(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
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
            IDictionary<string, double> scores = m_ScoreCardDao.GetNoFindings(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));
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

        private string GetComment(string fileId)
        {
            IDictionary<string, string> comments = m_ScoreCardCommentDao.GetGroupComment(fileId.Replace("&lt;", "<").Replace("&gt;", ">"));            
            StringBuilder json = new StringBuilder();
            json.AppendFormat("[ ");
            foreach (var comment in comments)
            {
                json.AppendFormat("{{ GroupId : {0}, Comment : '{1}' }},", comment.Key, comment.Value.Replace("'", "\\'"));
            }
            if (comments.Count > 0)
                json.Remove(json.Length - 1, 1);
            json.AppendFormat(" ]");
            return json.ToString();
        }
    }
}
