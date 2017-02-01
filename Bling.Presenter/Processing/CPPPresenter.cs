using System;
using Bling.Domain.Extension;
using log4net;

namespace Bling.Presenter.Processing
{
    public interface IProcessingReport
    {
        string From { get; }
        string To { get; }
        int DateToSearch { get; }
    }

    public class CPPPresenter : Presenter
    {
        private IProcessingReport m_View;

        public CPPPresenter(IProcessingReport view)
        {
            m_View = view;
            m_logger = LogManager.GetLogger(typeof(CPPPresenter));
            
        }

        public void ViewReport(string reportName)
        {            
            new Crystal(reportName)
                .ConnectToDataDepot()
                .AddParameter("@start", m_View.From.ToDateTime())
                .AddParameter("@end", m_View.To.ToDateTime())
                .AddParameter("@dateType", m_View.DateToSearch)                 
                .SetDestinationToPDF()
                .SetPaperToLegal()
                .ViewReport();
            m_logger.DebugFormat("Start: {0}, End: {1}, DateType: {2}", m_View.From, m_View.To, m_View.DateToSearch);

        }
    }
}
