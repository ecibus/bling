using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;
using log4net;
using Bling.Domain.Extension;
using Bling.Domain.Compliance;

namespace Bling.Presenter.Compliance
{
    public class AjaxLPEPresenter : Presenter
    {
        private IAjaxView m_View;
        private ILPELoanInfoDao m_Dao;
        private ILPEReasonDao m_ReasonDao;

        public AjaxLPEPresenter(IAjaxView view)
            : this (view, new LPELoanInfoDao(DMDDataSession()), new LPEReasonDao(DMDDataSession()))
        {
        }

        public AjaxLPEPresenter(IAjaxView view, ILPELoanInfoDao dao, ILPEReasonDao reasonDao)
        {
            m_View = view;
            m_Dao = dao;
            m_ReasonDao = reasonDao;
            m_logger = LogManager.GetLogger(typeof(AjaxLPEPresenter));
        }

        public void LoadLoan(string loanNumber)
        {
            try
            {
                //m_View.ResponseText = m_Dao.GetLoanInfo(loanNumber).ToJson(LPEReason.ToLookUp(m_ReasonDao.GetAll()));
                m_View.ResponseText = m_Dao.GetLoanInfo(loanNumber).ToJson(m_ReasonDao.GetAll());
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void GetLastChanges(string loanNumber)
        {
            try
            {
                //m_View.ResponseText = m_Dao.GetLastChanges(loanNumber).ToJson(LPEReason.ToLookUp(m_ReasonDao.GetAll()));
                m_View.ResponseText = m_Dao.GetLastChanges(loanNumber).ToJson(m_ReasonDao.GetAll());
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void UpdateReadyForDocs(string loanNumber, string value)
        {
            try
            {
                m_Dao.UpdateReadyForDocs(loanNumber, value == "1" ? true : false);
                m_View.ResponseText = " { } ";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void UpdateReasonAndComment(string loanNumber, string reasonId, string comment, string commentedBy)
        {
            try
            {
                m_Dao.UpdateReasonAndComment(loanNumber, reasonId.ToInteger(), comment, commentedBy);
                m_View.ResponseText = " { } ";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void InitialReviewComplete(string loanNumber, string reviewedBy)
        {
            try
            {
                m_Dao.InitialReviewComplete(loanNumber, reviewedBy);
                m_View.ResponseText = " { } ";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        public void AddHistory(string createdBy, string loanNumber, string borrower, string loanType, string loanAmount,
            string gemLoanFeeCharged, string loanOriginationFeeCharged, string loanOfficerPrice, string borrowerPaidDiscount,
            string lenderCredit, string ficoScore, string applicationDate, string lockedDate, string noOfBorrower,
            string programType, string transactionType, string finalNetPrice)
        {
            try
            {
                m_Dao.AddHistory(
                    createdBy, loanNumber, borrower, loanType,
                    GetNumericFromString(loanAmount),
                    GetNumericFromString(gemLoanFeeCharged),
                    GetNumericFromString(loanOriginationFeeCharged),
                    GetNumericFromString(loanOfficerPrice),
                    GetNumericFromString(borrowerPaidDiscount),
                    GetNumericFromString(lenderCredit),
                    ficoScore, applicationDate, lockedDate, noOfBorrower, programType, transactionType,
                    GetNumericFromString(finalNetPrice)
                    );
                m_View.ResponseText = " { } ";
            }
            catch (Exception ex)
            {
                m_logger.DebugFormat("Exception: {0}", ex.Message);
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Escape());
            }
        }

        private double GetNumericFromString(string s)
        {
            return s.Replace("$", "").Replace(",", "").Replace("( ", "-").Replace(")", "").Trim().ToDouble();
        }

    }
}
