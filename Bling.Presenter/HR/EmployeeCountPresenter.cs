using System;
using log4net;
using Bling.Domain.Extension;

namespace Bling.Presenter.HR
{
    public interface IEmployeeCount
    {
        string From { get; }
        string To { get; }
    }

    public class EmployeeCountPresenter : Presenter
    {
        private IEmployeeCount m_View;

        public EmployeeCountPresenter(IEmployeeCount view)
        {
            m_View = view;
            m_logger = LogManager.GetLogger(typeof(EmployeeCountPresenter));            
        }

        public void ViewReport(string reportName)
        {
            new Crystal(reportName)
                .ConnectToDynamic()
                .AddParameter("@start", m_View.From.ToDateTime())
                .AddParameter("@end", m_View.To.ToDateTime())
                .SetDestinationToPDF()
                .ViewReport();
            m_logger.DebugFormat("Start: {0}, End: {1}", m_View.From, m_View.To);

        }
    }
}
