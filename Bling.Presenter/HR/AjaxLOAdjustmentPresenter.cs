using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Repository.HR;
using Bling.Domain.HR;

namespace Bling.Presenter.HR
{
    public class AjaxLOAdjustmentPresenter : Presenter
    {
        private IAjaxView m_View;
        private ILOAdjustmentDao m_adjDao;

        public AjaxLOAdjustmentPresenter(IAjaxView view)
            : this (view, new LOAdjustmentDao(MWDataStoreSession()))
        {
        }

        public AjaxLOAdjustmentPresenter(IAjaxView view, ILOAdjustmentDao adjDao)
        {
            m_View = view;
            m_adjDao = adjDao;
        }

        public void LoadAdjustment(string lo)
        {
            m_View.ResponseText = LOAdjustment.ToHTMLTable(m_adjDao.GetAllByLOCode(lo));

        }

        public void AddAdjustment(LOAdjustment loa)
        {
            m_adjDao.Save(loa);
        }

        public void RemoveAdjustment(int id)
        {
            m_adjDao.DeleteAdjustment(id);
        }

        public void GenerateReport(string reportName, string pdfName, string lo, string from, string to)
        {
            Crystal crystal = new Crystal(reportName)
               .ConnectToDataDepot()
               .SetDestinationToPDFAndRename(reportName, pdfName)
               .AddParameter("@lo", lo)
               .AddParameter("@start", from)
               .AddParameter("@end", to)
               .ViewReport(true);

            crystal.Dispose();

        }
    
    }
}
