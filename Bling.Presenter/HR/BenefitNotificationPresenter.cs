using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Bling.Presenter.HR
{
    public interface IBenefitNotificationView
    {
        string From { get; }
        string To { get; }
    }

    public class BenefitNotificationPresenter : Presenter
    {
        private IBenefitNotificationView _view;

        public BenefitNotificationPresenter(IBenefitNotificationView view)
        {
            _view = view;
            m_logger = LogManager.GetLogger(typeof(BenefitNotificationPresenter));
        }

        public void ViewReport(string reportName)
        {
            new Crystal(reportName)
                .ConnectToDataDepot()
                .AddParameter("@start", _view.From)
                .AddParameter("@end", _view.To)
                .SetDestinationToPDF()
                .ViewReport();
            m_logger.DebugFormat("Start: {0}, End: {1}", _view.From, _view.To);

        }
    }
}
