using System;
using Bling.Domain.Accounting;
using Bling.Domain.Extension;
using Bling.Repository.Accounting;

namespace Bling.Presenter.Accounting
{
    public interface IDocTrustView
    {
        string TransferDate { get; }
        string AsOfDate { get; }
        string CreatedBy {get; }
        string HistoryTable { set; }
    }

    public class DocTrustPresenter : Presenter
    {
        private IDocTrustView m_View;
        private IDocTrustDao m_DocTrustDao;
        private IDocTrustRunHistoryDao m_DocTrustRunHistoryDao;

        public DocTrustPresenter(IDocTrustView view) : 
            this(view, new DocTrustDao(MWDataStoreSession()), new DocTrustRunHistoryDao(MWDataStoreSession()))        
        {            
        }

        public DocTrustPresenter(IDocTrustView view, IDocTrustDao docTrustDao, IDocTrustRunHistoryDao docTrustRunHistoryDao)
        {
            m_View = view;
            m_DocTrustDao = docTrustDao;
            m_DocTrustRunHistoryDao = docTrustRunHistoryDao;
        }

        public void Transfer ()
        {
            m_DocTrustDao.Transfer(m_View.TransferDate, m_View.AsOfDate);
            
            m_DocTrustRunHistoryDao.Save(new DocTrustRunHistory { TransferDate=m_View.TransferDate, 
                AsOf=m_View.AsOfDate, CreatedBy=m_View.CreatedBy});

            LoadHistory();
        }

        public void LoadHistory()
        {
            m_View.HistoryTable = DocTrustRunHistory.ToHtmlTable(m_DocTrustRunHistoryDao.GetLast10History());
        }

        public void ViewReport(string reportName)        
        {
            new Crystal(reportName)
                .ConnectToSQLBeast()
                .SetDestinationToPDF()                
                .AddParameter("@TransferDate", m_View.TransferDate.ToDateTime())
                .AddParameter("@AsOf", m_View.AsOfDate.ToDateTime())
                .ViewReport();
        }
    }
}
