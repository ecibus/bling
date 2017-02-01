using System;
using log4net;
using Bling.Domain.Extension;

namespace Bling.Presenter.Processing
{
    public class GEMAppraisalPresenter : Presenter
    {
        private IProcessingReport m_View;

        public GEMAppraisalPresenter(IProcessingReport view)
        {
            m_View = view;
            m_logger = LogManager.GetLogger(typeof(GEMAppraisalPresenter));            
        }

        public void ViewReport(string reportName)
        {            
            new Crystal(reportName)
                .ConnectToDataDepot()
                .AddParameter("@start", m_View.From.ToDateTime())
                .AddParameter("@end", m_View.To.ToDateTime())
                .AddParameter("@dateType", m_View.DateToSearch)                 
                .SetDestinationToPDF()
                .ViewReport();
            m_logger.DebugFormat("Start: {0}, End: {1}, DateType: {2}", m_View.From, m_View.To, m_View.DateToSearch);

        }
    }
}
