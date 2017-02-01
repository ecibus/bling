using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.Compliance;

namespace Bling.Presenter.Compliance
{
    public class AjaxChaseFundingFormPresenter : Presenter
    {
        private IAjaxView m_View;
        private IChaseFundingFormDao m_Dao;

        public AjaxChaseFundingFormPresenter(IAjaxView view)
            : this (view, new ChaseFundingFormDao(DMDDataSession()))
        {
        }

        public AjaxChaseFundingFormPresenter(IAjaxView view, IChaseFundingFormDao dao)
        {
            m_View = view;
            m_Dao = dao;
        }

        public void GetLoanInfo(string loanNumber)
        {
            try
            {
                var loanInfo = m_Dao.GetLoanInfo(loanNumber);

                if (loanInfo == null)
                {
                    m_View.ResponseText = String.Format("{{ \"Message\" : \"Could not find Loan Number <b>{0}</b>, please try again.\" }}", loanNumber);
                    return;
                }

                m_View.ResponseText = loanInfo.ToJson();
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveGeneralInfo(string fileId, string purchaseProperty, string item2, string chkOccInvestmentYes, string chkOccInvestmentNo, string chkOccInvestmentNA)
        {
            try
            {
                m_Dao.SaveGeneralInfo(fileId, purchaseProperty, item2, chkOccInvestmentYes, chkOccInvestmentNo, chkOccInvestmentNA);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveATRQM(string fileId, string aporPcnt, string qmSafeHarbor, string qmRebuttablePresumption, string nonQM, string qmNotApplicable, string item7AYes, string item7ANo, string item7BYes, string item7BNo, string item8Yes, string item8No)
        {
            try
            {
                m_Dao.SaveATRQM(fileId, aporPcnt, qmSafeHarbor, qmRebuttablePresumption, nonQM, qmNotApplicable, item7AYes, item7ANo, item7BYes, item7BNo, item8Yes, item8No);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveHighCost(string fileId, string prepaymentPenalty)
        {
            try
            {
                m_Dao.SaveHighCost(fileId, prepaymentPenalty);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveHighCostContinued(string fileId, string pointsExcluded, string feesImposed, string hoepaAPR)
        {
            try
            {
                m_Dao.SaveHighCostContinued(fileId, pointsExcluded, feesImposed, hoepaAPR);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveSpecialFeature(string fileId, string SFFannieMae, string SFFreddieMac, string MIMonthlyPremium, string MISinglePremium)
        {
            try
            {
                m_Dao.SaveSpecialFeature(fileId, SFFannieMae, SFFreddieMac, MIMonthlyPremium, MISinglePremium);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }

        public void SaveExcludedBonafide(string fileId, string item15Percent, string item15Amount, string hoepaQMPcnt, string hoepaQMAmount, string statePcnt, string stateAmount)
        {
            try
            {
                m_Dao.SaveExcludedBonafide(fileId, item15Percent, item15Amount, hoepaQMPcnt, hoepaQMAmount, statePcnt, stateAmount);

                m_View.ResponseText = String.Format("{{ \"Message\" : \"Done saving\" }}");
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
                   .AddParameter("cOption1", loanNumber)
                   .ViewReport();
            }
            catch (Exception ex)
            {
                m_View.ResponseText = String.Format("{{ Message : '{0}' }}", ex.Message.Replace("'", "\\'"));
            }
        }
    }
}
